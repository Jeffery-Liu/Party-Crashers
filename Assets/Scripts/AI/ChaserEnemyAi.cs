using UnityEngine;
using System.Collections;

public class ChaserEnemyAi : EnemyAI // Used to inherit from monobehaviour
{
    public float m_ChaseDist = 50;
    public float m_StopDistance = 5;
    public float KnockBackDis = 40f;
    public int m_Damage = 1;

    public bool disableGetClosestPlayer;
    EnemyEffect enemyEffect;
    private HeartSystem m_HeartSystem;
    private bool m_CanDamage = true;

    Animator m_Animator;

    //sound
    public AudioClip[] AttentionSFX;
    public AudioClip SFXtoPlay;
    static private int Chance = 1;
    public int maxChance;
    public int ChanceNumber;
    public bool m_IsPlayed;
    // Use this for initialization

    public int hitmaxChance;
    public int hitChanceNumber;
    public AudioClip[] DamageSFX;
    public AudioClip SFXtoPlay2;
    void Start()
    {
        initializeVariables();
        enemyEffect = gameObject.GetComponent<EnemyEffect>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!disableGetClosestPlayer)
        {
            getClosestPlayer();
        }

        if (!enemyEffect.isStun)
        {
            chase();

            if (m_Distance < m_ChaseDist)
            {
                chase();
                isArrived = false;

                if(!m_IsPlayed)
                {
                    ChanceNumber = Random.Range(0, maxChance);
                    if (ChanceNumber == Chance)
                    {
                        SFXtoPlay = AttentionSFX[Random.Range(0, AttentionSFX.Length)];
                        AudioManager.m_Instance.PushMusic(SFXtoPlay);
                    }
                    m_IsPlayed = true;
                }
                
            }
            if (m_Distance < m_StopDistance)
            {
                agent.Stop();
            }
            if (m_Distance > m_ChaseDist)
            {
                returnToOrigin();
            }
            if (isArrived == true)
            {
                if(m_Animator != null)
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
            if (m_Animator != null)
            {
                m_Animator.SetBool("isChasing", false);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<HeartSystem>() != null)
        {
            m_HeartSystem = other.GetComponent<HeartSystem>();
            if (other.tag == "Player")
            {
                if(m_CanDamage)
                {
                    hitChanceNumber = Random.Range(0, hitmaxChance);
                    if (hitChanceNumber == Chance)
                    {
                        SFXtoPlay2 = DamageSFX[Random.Range(0, DamageSFX.Length)];
                        AudioManager.m_Instance.PushMusic(SFXtoPlay2);
                    }

                    m_HeartSystem.TakeDamage(m_Damage);
                    m_CanDamage = false;
                    StartCoroutine(WaitForSec(2));

                    
                }
                m_HeartSystem.UpdateHearts();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //if (other.GetComponent<HeartSystem>() != null)
        //{
        //    m_CanDamage = true;
        //}
    }
    
    IEnumerator WaitForSec(float s)
    {
        yield return new WaitForSeconds(s);
        m_CanDamage = true;
    }

}


