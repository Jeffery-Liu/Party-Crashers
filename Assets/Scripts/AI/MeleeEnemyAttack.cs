using UnityEngine;
using System.Collections;

public class MeleeEnemyAttack : MonoBehaviour {

    GameObject[] players;
    int PlayerNumbers;
    float x;
    float y;
    float z;

    public Vector3 StartPos;
    public float StartPosX = 0.5f;
    public float StartPosY = 1f;
    public float StartPosZ = 0f;

    public Vector3 AttackPos;
    public float AttackPosX = 0f;
    public float AttackPosY = -0.16f;
    public float AttackPosZ = 8f;

    public float m_Distance;
    public float AttackDis = 2f;

    private float m_LastShotTime;
    private float m_LastAttackTime;
    private float m_LastAttackTime2;
    public float timer;
    
    public float AttackCoolDown = 5f;
    public float AttackTime = 10f;
    
    void Start()
    {
        StartPos = new Vector3(StartPosX, StartPosY, StartPosZ);
        AttackPos = new Vector3(AttackPosX, AttackPosY, AttackPosZ);
        PlayerNumbers = GameObject.FindGameObjectsWithTag("Player").Length;
        players = GameManager.m_Instance.m_Players;
        m_LastShotTime = Time.time;
        m_LastAttackTime = Time.time;
        m_LastAttackTime2 = Time.time;
    }
    
    void Update()
    {
        bool CoolDown = (m_LastAttackTime + AttackCoolDown) < Time.time;
        bool Attack = (m_LastAttackTime2 + AttackTime) < Time.time;
        if (m_Distance <= AttackDis)
        {
            //transform.position = Vector3.Lerp(StartPos, KnockBack, KnockBackSpeed);

        }
    }
    
}
