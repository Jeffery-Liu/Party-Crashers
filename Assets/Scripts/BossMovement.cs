using UnityEngine;
using System.Collections;

public class BossMovement : EnemyAI
{
    //GameObject[] players;
    //public float StartPosX = 0;
    //public float StartPosY = 1;
    //public float StartPosZ = 100;
    public float RunAwayDistance = 5f;
    public float ChaseDistance = 10f;
    public float StayDistance = 15f;
    public float AttackDistance = 20f;
    public float KnockBackDis = 40f;
    //public float m_Distance;
    public float BossRunAwaySpeed = 0.01f;
    //public float BossChaseSpeed = 0.005f;

    //public Vector3 StartPos;

    Vector3 MoveBackward;
    Vector3 Flee;

    //PlayerController playercontroller;
    EnemyEffect enemyEffect;
    // Use this for initialization
    void Start()
    {
        initializeVariables();
        enemyEffect = gameObject.GetComponent<EnemyEffect>();
        //StartPos = new Vector3(StartPosX, StartPosY, StartPosZ);
        //players = GameManager.m_Instance.m_Players;
        //transform.position = new Vector3(StartPosX, StartPosY, StartPosZ);
        //players = GameObject.FindGameObjectsWithTag("Player");
        //playercontroller = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //float NewX = transform.position.x;
        //float NewY = transform.position.y;
        //float NewZ = transform.position.z;
        //?
        //Vector3 MoveToward;

        // Get closest player

        getClosestPlayer();
        look(target.transform);
        MoveBackward = transform.position - target.transform.position;
        Flee = transform.position + MoveBackward;
        if (!enemyEffect.isStun)
        {
            //m_Distance = Vector3.Distance(players[i].transform.position, transform.position);
            //MoveToward = players[i].transform.position - transform.position;
            if (m_Distance <= RunAwayDistance)
            {
                transform.position = Vector3.Lerp(transform.position, Flee, BossRunAwaySpeed);
                Debug.Log("Running away!");
                //transform.position += MoveBackward * BossMoveSpeed * Time.deltaTime;
            }
            else if (m_Distance > ChaseDistance && m_Distance < StayDistance)
            {
                chase();
                //transform.position = Vector3.Lerp(transform.position, players[i].transform.position, BossChaseSpeed);
                //transform.position += MoveToward * BossMoveSpeed * Time.deltaTime;
            }
            else if (m_Distance >= StayDistance)
            {
                returnToOrigin();
                //transform.position = Vector3.Lerp(transform.position, StartPos, BossRunAwaySpeed);
            }
        }
        else
        {
            agent.Stop();
        }

    }
}
