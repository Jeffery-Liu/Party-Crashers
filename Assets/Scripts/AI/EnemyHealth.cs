using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    //sound
    public GameObject SFXPlayer;
    public GameObject SFXHit;
    public AudioClip[] hurtSFX;
    public AudioClip[] deathSFX;
    private AudioClip SFXtoPlay;
    public float maxRandomPitch;
    public float minRandomPitch;
    private float randomPitch;

    //Kavells VFX code
    public GameObject deathVFX;
    public GameObject hurtVFX;
    //Kavells VFX code

    public float m_MaxHealth = 100f;
    public float m_EnemyHealth;
    public float m_EnemyInvincibleTime = 0.5f;
    public GameObject m_Drop;
    //[HideInInspector]
    public bool isInvincible = false;
    private bool isDamage = false;


    void Awake()
    {
        m_EnemyHealth = m_MaxHealth;
    }

    void Update()
    {
        if (this.gameObject.name != "Boss")
        {
            if (isDamage == true)
            {
                isInvincible = true;
                StartCoroutine(StopDamagefor(m_EnemyInvincibleTime));
            }
            if (isDamage == false)
            {
                isInvincible = false;
            }
        }
    }


    public void Kill()
    {
        Destroy(gameObject);

        if (m_Drop != null)
            Instantiate(m_Drop, gameObject.transform.position, gameObject.transform.rotation);

        for (int i = 0; i < GameManager.m_Instance.m_Players.Length; ++i)
        {
            Player player = GameManager.m_Instance.m_Players[i].GetComponent<Player>();
            player.m_Score += 100;
        }
    }

    public float GetEnemyHealth()
    {
        return m_EnemyHealth;
    }

    public void Damage(float health)
    {
        isDamage = true;
        //Debug.Log("Damaged");
        if (isInvincible == true)
        {
        }
        if (isInvincible == false)
        {
            m_EnemyHealth -= health;
            //James Sound Code
           
            randomPitch = Random.Range(minRandomPitch, maxRandomPitch);
            if (randomPitch > 3)
            {
                randomPitch = 3;
            }


            if (m_EnemyHealth > 0)
            {
                SFXtoPlay = hurtSFX[Random.Range(0, hurtSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay);
            }

            else
            {
                SFXtoPlay = deathSFX[Random.Range(0, deathSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay);
            }

            //if (SFXPlayer != null)
            //{
            //    AudioSource source = SFXPlayer.GetComponent<AudioSource>();
            //    source.pitch = randomPitch;
            //    source.clip = SFXtoPlay;
            //    //source.Play();
            //    //SFXManager.GetPitch(randomPitch);
            //    //SFXManager.PushMusic(SFXtoPlay);
            //    //SFXManager.PushMusic(SFXtoPlay);
            //}

            //GameObject SFXtest = Instantiate(SFXPlayer, transform.position, transform.rotation) as GameObject;
            //GameObject SFXtest2 = Instantiate(SFXHit, transform.position, transform.rotation) as GameObject;

            //James Sound Code
        }
        if (m_EnemyHealth <= 0)
        {
			//Kavells VFX code
			if (deathVFX != null) 
			{
				GameObject takeDamage;
				takeDamage = (GameObject)Instantiate (deathVFX, transform.position, transform.rotation);
				Destroy (takeDamage, 1f);
			}
			//Kavells VFX code
            if(gameObject.GetComponent<AdvancedBossAi>() == null)
            {
                Kill();
            }
            
        }
        else
        {
            //Kavells VFX code
            if (hurtVFX != null)
            {
                GameObject takeDamage;
                takeDamage = (GameObject)Instantiate(hurtVFX, transform.position, transform.rotation);
                Destroy(takeDamage, 1f);
            }
            //Kavells VFX code
        }
    }
    IEnumerator StopDamagefor(float wait)
    {
        //this is the amount of time i want it to wait
        yield return new WaitForSeconds(wait);
        //this is what it will do when the timehas passed
        isDamage = false;
    }
}
