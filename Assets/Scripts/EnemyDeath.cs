using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    public int m_EnemyHealth = 100;
    public int m_PartyBarAmount = 2;
    public GameObject coin;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            if (m_EnemyHealth > 0)
            {
                m_EnemyHealth = m_EnemyHealth - 5;
            }
            else
            {
                
                Destroy(gameObject);

                Instantiate(coin, this.gameObject.transform.position, this.gameObject.transform.rotation);

                for (int i = 0; i < GameManager.m_Instance.m_Players.Length; ++i)
                {
                    Player player = GameManager.m_Instance.m_Players[i].GetComponent<Player>();
                            player.m_Score += 100;
                }

            }
        }
        if (col.gameObject.CompareTag("Physical"))
        {
            if (m_EnemyHealth > 0)
            {
                m_EnemyHealth--;
            }
            else
            {
                
                Destroy(gameObject);

                Instantiate(coin, this.gameObject.transform.position, this.gameObject.transform.rotation);

                for (int i = 0; i < GameManager.m_Instance.m_Players.Length; ++i)
                {
                    Player player = GameManager.m_Instance.m_Players[i].GetComponent<Player>();
                    player.m_Score += 100;
                }

            }
        }
    }
}