using UnityEngine;
using System.Collections;

public class KickMeTrap : Trap {

    public string[] m_EnemiesToAffect;

    public float m_Radius;
    public float chaseIncrease;
    public float m_ChasingTime;
	public bool m_used = false;
    public GameObject m_effect;
    private GameObject m_Player;
    private GameObject m_CurEffect;
    // Use this for initialization

    void Update()
    {
        if(m_Player != null)
        {
            transform.position = m_Player.gameObject.transform.position;
            if (m_effect != null)
            {
                m_CurEffect.transform.position = m_Player.gameObject.transform.position;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && m_used == false)
        {
            m_Player = other.gameObject;
            m_CurEffect = (GameObject)Instantiate(m_effect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(m_CurEffect, m_ChasingTime);
            Destroy(gameObject, m_ChasingTime);
        }

        if (other.tag == "Player" && m_used == false)
        {
            //EnemyAI.Instance.getDistance(50);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_Radius);
            for (int i = 0; i < hitColliders.Length; ++i)
            {
                for (int j = 0; j < m_EnemiesToAffect.Length; j++)
                {
                    if (hitColliders[i].gameObject.CompareTag(m_EnemiesToAffect[j]))
                    {
                        if (hitColliders[i].GetComponent<ChaserEnemyAi>() != null)
                        {
                            //Transform enemy = hitColliders[i].transform;
                            ChaserEnemyAi ai = hitColliders[i].GetComponent<ChaserEnemyAi>();

                            ai.disableGetClosestPlayer = true;
                            ai.m_ChaseDist += chaseIncrease;
                            ai.target = other.GetComponent<Player>().gameObject;
                            StartCoroutine(setToDefault(ai));
                        }
                    }
                }
            }

        }
    }
  //  public void OnTriggerStay(Collider other)
  //  {
		////Debug.Log("RUNING");
		
  //  }

	public void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
            m_used = true;
		}
	}

    IEnumerator setToDefault(ChaserEnemyAi ai)
    {
        yield return new WaitForSeconds(3);
        ai.disableGetClosestPlayer = false;
        ai.m_ChaseDist -= chaseIncrease;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        timer += Time.deltaTime;

    //        if (timer > chasingtime)
    //        {
    //            EnemyAI.Instance.getDistance(10);
    //        }
    //    }
    //}

}
