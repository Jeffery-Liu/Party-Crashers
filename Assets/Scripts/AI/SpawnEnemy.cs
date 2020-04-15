using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnEnemy : EnemyAI // Inherits from EnemyAI now instead of Monobehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    public int limitedSpawnTotalNum = 4;
    public int limitedSpawnCurrentNum = 0;
    public bool infiniteSpawn;
    public int infiniteSpawnInMapNum = 5;
    public int infiniteSpawnCurrentNum;
    float x;
    float y;
    float z;
    Vector3 RandomLocation;
    public float SpawnRange = 5f;
    public float GodRadius = 3f;
    //---------------------------------------------------
    public GameObject enemyPrefab;
    public float timer;
    public float range;
    public float spawnTime = 5.0f;
    public float activedRange = 10f;
    // run away
    Vector3 MoveDirection;
    Vector3 RunAwayDirection;
    public float RunAwayRange = 5f;
    public float RunSpeed = 0.01f;

	//VFX
	public GameObject spawningEffect;
	public GameObject spawnedEffect;
	//VFX

    //EnemyAI enemyAi;
    EnemyEffect enemyEffect;

    //SFX
    public AudioSource audioSource;
    public AudioClip[] SFX;
    private AudioClip SFXtoPlay;

    public float maxRandomPitch;
    public float minRandomPitch;
    private float randomPitch;
    //SFX End

    Animator m_Animator;

    void Start()
    {
        initializeVariables();
        timer = spawnTime;
        enemyEffect = gameObject.GetComponent<EnemyEffect>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
                infiniteSpawnCurrentNum--;
            }
        }
        // run away ----------------------------------------------------------
        for (int i = 0; i < players.Length; i++)
        {
            if (!enemyEffect.isStun)
            {
                m_Distance = Vector3.Distance(players[i].transform.position, transform.position);
                target = players[i];
                look(target.transform);
                MoveDirection = transform.position - players[i].transform.position;
                RunAwayDirection = transform.position + MoveDirection;
                if (m_Distance <= RunAwayRange)
                {
                    transform.position = Vector3.Lerp(transform.position, RunAwayDirection, RunSpeed);
                    isArrived = false;
                }
                if(m_Distance > RunAwayRange)
                {
                    isArrived = true;
                }
                if (isArrived == true)
                {
                    if (m_Animator != null)
                    {
                        m_Animator.SetBool("isChasing", false);
                    }
                }
                if (isArrived == false)
                {
                    if (m_Animator != null)
                    {
                        m_Animator.SetBool("isChasing", true);
                    }
                }
            }
            else
            {
                agent.Stop();
                isArrived = true;
            }
        }
        // run away finish ---------------------------------------------------

        timer -= Time.deltaTime;
        range = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);
        if (infiniteSpawn == true)
        {
            if (infiniteSpawnCurrentNum < infiniteSpawnInMapNum)
            {
                if (timer <= 0 && range <= activedRange)
                {
                    GameObject enemy = EnemySpawner();

                    enemies.Add(enemy);
                    infiniteSpawnCurrentNum++;
                    timer = spawnTime;
                }
            }
        }
        else
        {
            if (limitedSpawnCurrentNum < limitedSpawnTotalNum)
            {
                if (timer <= 0 && range <= activedRange)
                {
                    Instantiate(enemyPrefab, GetRandomLocationForEnemy(), transform.rotation);
                    limitedSpawnCurrentNum++;
                    timer = spawnTime;
                }
            }
        }
    }
    public Vector3 GetRandomLocationForEnemy()
    {
        x = Random.Range(transform.position.x - SpawnRange, transform.position.x + SpawnRange);
        y = 1;
        z = Random.Range(transform.position.z - SpawnRange, transform.position.z + SpawnRange);

        for (int i = 0; i < players.Length; i++)
        {
            while (x <= players[i].transform.position.x + GodRadius && x >= players[i].transform.position.x - GodRadius && z <= players[i].transform.position.z + GodRadius && z >= players[i].transform.position.z - GodRadius)
            {
                x = Random.Range(transform.position.x - SpawnRange, transform.position.x + SpawnRange);
                z = Random.Range(transform.position.z - SpawnRange, transform.position.z + SpawnRange);
            }
        }

        RandomLocation = new Vector3(x, y, z);
        return RandomLocation;
    }
    GameObject EnemySpawner()
    {
        GameObject enemy = (GameObject)Instantiate(enemyPrefab, GetRandomLocationForEnemy(), transform.rotation);
		//VFX
		if (spawningEffect != null && spawnedEffect != null) 
		{
			GameObject spawningvfx;
			GameObject spawnedvfx;
			spawningvfx = (GameObject)Instantiate (spawningEffect, transform.position, transform.rotation);
			spawnedvfx = (GameObject)Instantiate (spawnedEffect, enemy.transform.position, transform.rotation);
			Destroy (spawningvfx, 2f);
			Destroy (spawnedvfx, 2f);
		}
        //VFXend
        //SFX
        if (audioSource != null)
        {
            randomPitch = Random.RandomRange(maxRandomPitch, minRandomPitch);
            SFXtoPlay = SFX[Random.Range(0, SFX.Length)];
            audioSource.clip = SFXtoPlay;
            audioSource.pitch = randomPitch;
            audioSource.Play();
        }
        //SFX END
        return enemy;
    }
}
