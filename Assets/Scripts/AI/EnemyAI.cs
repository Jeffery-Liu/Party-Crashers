using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    protected GameObject[] players;
    public GameObject target;
    public NavMeshAgent agent;
    protected float m_Distance;
    protected Vector3 m_Origin;
    public float m_RotationSpeed = 2f;
    [HideInInspector]
    public float m_Rtts;
    public Rigidbody m_RigidBody;
    public bool isArrived = false;

    private float m_KnockBackCounter;
 private Vector3 m_KnockBackPosition;
 private float m_KnockBackTime;
 private float m_KnockBackSpeed;
    public float m_LookUpDis = 5f;

    void Start()
    {
        m_Rtts = m_RotationSpeed;
    }

    void FixedUpdate()
    {
        m_KnockBackCounter -= Time.deltaTime;
        if (m_KnockBackCounter > 0)
        {
            Vector3 newPosition = Vector3.Slerp(m_KnockBackPosition.normalized * m_KnockBackSpeed, new Vector3(0, 0, 0), 1f - m_KnockBackCounter / m_KnockBackTime);
            agent.Move(newPosition * Time.deltaTime);
        }
      }

    public void chase()
    {
        if (target != null)
        {
            look(target.transform);
            agent.SetDestination(target.transform.position);
            agent.Resume();
        }
    }

    public void returnToOrigin()
    {
        agent.SetDestination(m_Origin);
        agent.Resume();
        if(transform.position.x == m_Origin.x && transform.position.z == m_Origin.z)
        {
            isArrived = true;
        }
        else
        {
            isArrived = false;
        }
    }

    public void aim(Transform other)
    {
        if (target != null)
        {
            //if(other.transform.position.y < (transform.position.y + m_LookUpDis) && other.transform.position.y > (transform.position.y - m_LookUpDis))
            //{
                transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            //}
        }
    }

    public void look(Transform other)
    {
        Vector3 lookPosition = other.position - transform.position;
        lookPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * m_Rtts);
    }

    public void Knockback(Vector3 position, float KB)
    {
        m_KnockBackPosition = position;
        m_KnockBackCounter = m_KnockBackTime;
        m_KnockBackSpeed = KB;
    }

    public void disableAgent()
    {
        agent.enabled = false;
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        m_RigidBody = gameObject.GetComponent<Rigidbody>();
        m_RigidBody.freezeRotation = true;
    }

    public void reActivateAgent(float time)
    {
        StopCoroutine("reActivateAgentCoroutine");
        StartCoroutine(reActivateAgentCoroutine(time));
    }

    IEnumerator reActivateAgentCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(m_RigidBody);
        agent.enabled = true;
    }

    public void getClosestPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i == 0)
            {
                if(players[i].GetComponent<Player>().m_State == Player.State.Alive)
                {
                    m_Distance = Vector3.Distance(players[i].transform.position, transform.position);
                    target = players[i];
                }
                else
                {
                    m_Distance = 10000;
                }
            }
            else
            {
                if (Vector3.Distance(players[i].transform.position, transform.position) < m_Distance)
                {
                    //m_Distance = Vector3.Distance(players[i].transform.position, transform.position);
                    if (players[i].GetComponent<Player>().m_State == Player.State.Alive)
                    {
                        m_Distance = Vector3.Distance(players[i].transform.position, transform.position);
                        target = players[i];
                    }
                    else
                    {
                        m_Distance = 10000;
                    }
                }
            }
        }
        if(m_Distance == 10000)
        {
            target = null;
        }
    }

    public void initializeVariables()
    {
        players = GameManager.m_Instance.m_Players;
        agent = gameObject.GetComponent<NavMeshAgent>();
        m_Origin = gameObject.transform.position;
        m_KnockBackTime = 0.5f;
        m_KnockBackSpeed = 30f;
    }

}
