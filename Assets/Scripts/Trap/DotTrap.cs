using UnityEngine;
using System.Collections;

public class DotTrap : MonoBehaviour
{

    public int m_DotDamage;
    public float m_CoolDown = 2;
    private HeartSystem m_HeartSystem;
    private bool m_CanDamagePlayer = true;
    private bool m_CanDamageEnemy = true;
    public GameObject m_effect;

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<HeartSystem>() != null)
        {
            m_HeartSystem = other.GetComponent<HeartSystem>();
            if (m_CanDamagePlayer)
            {
                if (m_effect != null)
                {
                    GameObject effect;
                    effect = (GameObject)Instantiate(m_effect, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(effect, 3f);
                }
                m_HeartSystem.TakeDamage(m_DotDamage);
                m_CanDamagePlayer = false;
                StartCoroutine(WaitForSec(m_CoolDown));
            }
            m_HeartSystem.UpdateHearts();
        }
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            EnemyHealth m_EnemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (m_CanDamageEnemy)
            {
                if (m_effect != null)
                {
                    GameObject effect;
                    effect = (GameObject)Instantiate(m_effect, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(effect, 3f);
                }
                m_EnemyHealth.Damage(m_DotDamage);
                m_CanDamageEnemy = false;
                Debug.Log(m_CanDamageEnemy);
                StartCoroutine(WaitForSec2(m_CoolDown));
            }
        }
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.GetComponent<HeartSystem>() != null)
    //    {
    //        m_CanDamagePlayer = true;
    //    }
    //    if (other.GetComponent<EnemyHealth>() != null)
    //    {
    //        m_CanDamageEnemy = true;
    //        Debug.Log("Exit");
    //    }
    //}


    IEnumerator WaitForSec(float s)
    {
        yield return new WaitForSeconds(s);
        m_CanDamagePlayer = true;
    }

    IEnumerator WaitForSec2(float s)
    {
        m_CanDamageEnemy = true;
        yield return new WaitForSeconds(s);
        Debug.Log("Wait sec");
    }
}
