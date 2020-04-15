using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{

    public float m_Damage;
    private Transform m_WeaponTransform;
    //kavells new code for feedback effects
    public GameObject landHitEffect;
    //kavells new code for feedback effects
    void Start()
    {
        m_WeaponTransform = transform;
        for (int i = 0; i < 10; ++i)
        {
            if (m_WeaponTransform.GetComponent<Weapon>() != null)
            {
                m_Damage = m_WeaponTransform.GetComponent<Weapon>().m_Damage;
                //Debug.Log(m_Damage + " set from weapon " + m_WeaponTransform.name);
                break;
            }
            else
            {
                if (m_WeaponTransform.parent != null)
                {
                    m_WeaponTransform = m_WeaponTransform.parent;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            //IF HIT BOSS
            if (other.GetComponent<AdvancedBossAi>() != null)
            {
                GameManager.m_Instance.m_PartyBar.transform.parent.GetComponentInChildren<Animator>().SetBool("Gain", true);
            }
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Damage(m_Damage);
            //kavells new code for feedback effects
            if (landHitEffect != null)
            {
                GameObject takeDamage;
                takeDamage = (GameObject)Instantiate(landHitEffect, other.transform.position, Random.rotation);
                Destroy(takeDamage, 1f);
            }
            //kavells new code for feedback effects
        }

        if (other.gameObject.GetComponent<HeartSystem>() != null)
        {
            if (other.gameObject.GetComponent<RespawnHealth>() != null)
            {
                RespawnHealth playerRespawnHealth = other.gameObject.GetComponent<RespawnHealth>();
                HeartSystem playerHeartSystem = other.gameObject.GetComponent<HeartSystem>();
                Player player = playerRespawnHealth.GetComponent<Player>();

                if (player.m_State == Player.State.Dead)
                {
                    playerRespawnHealth.damage(1);
                    Debug.Log("Pinata hit");
                }
                else if (gameObject.CompareTag("Dodgeball"))
                {
                    playerHeartSystem.TakeDamage((int)m_Damage);
                }
            }
        }
    }
}
