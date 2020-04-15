using UnityEngine;
using System.Collections;

public class RespawnHealth : MonoBehaviour
{

    public int m_RespawnCount = 1;
    public int m_MaxHealth = 5;
    public int m_CurrentHealth = 0;

    private Player m_Player;
    private HeartSystem m_PlayerHearts;
    private WeaponManager m_WeaponManager;
    private float m_Counter;
    private float m_RespawnTime;

    //SFX
    public GameObject SFXPlayer;
    public AudioClip pinataTear;
    private AudioClip SFXtoPlay;
    //SFX END

    void Start()
    {
        m_Player = GetComponent<Player>();
        m_PlayerHearts = GetComponent<HeartSystem>();
        m_WeaponManager = GetComponent<WeaponManager>();

        m_CurrentHealth = m_MaxHealth * m_RespawnCount;
        m_Counter = -1;
    }

    void Update()
    {
        if(m_Counter < Time.time - m_RespawnTime && m_Counter != -1)
        {
            revive();
            m_Counter = -1;
        }
    }

    public void damage(int damage)
    {
        m_CurrentHealth -= damage;

        Debug.Log("Current health for pinata: " + m_CurrentHealth + "/" + m_MaxHealth);
        if(m_CurrentHealth <= 0)
        {
            revive();
        }
    }

    public void initialize(float respawnTime)
    {
        m_CurrentHealth = m_MaxHealth;
        m_RespawnTime = respawnTime;
        m_Counter = Time.time;
    }

    void revive()
    {
        if (m_Player.m_State == Player.State.Dead)
        {
            m_PlayerHearts.curHealth = m_PlayerHearts.maxHealth;
            m_PlayerHearts.UpdateHearts();
            gameObject.layer = 8;
            m_Player.m_State = Player.State.Alive;
            m_Player.m_IsDead = false;
            m_Player.updateModel();
            //Debug.Log("Updated Model");
            //SFX Start
            if (SFXPlayer != null)
            {
                AudioSource source = SFXPlayer.GetComponent<AudioSource>();
                SFXtoPlay = pinataTear;
                source.clip = SFXtoPlay;
            }

            GameObject SFXtest = Instantiate(SFXPlayer, transform.position, transform.rotation) as GameObject;
            //SFX End
            StartCoroutine(waitBeforeInitializeWeapon());
            m_RespawnCount++;
        }
    }

    public void reviveWeapon()
    {
        StartCoroutine(waitBeforeInitializeWeapon());
    }

    IEnumerator waitBeforeInitializeWeapon()
    {
        yield return new WaitForSeconds(.05f);
        m_WeaponManager.initialize();
    }
}
