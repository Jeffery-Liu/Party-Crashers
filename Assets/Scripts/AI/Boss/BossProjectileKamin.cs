using UnityEngine;
using System.Collections;

public class BossProjectileKamin : MonoBehaviour
{

    [HideInInspector]
    public Vector3 m_ProjectileVelocity;

    public int m_OutTime;
    public int m_SlowTime;
    public int m_BackTime;

    private Transform m_FirstPosition;
    private Vector3 m_StartingVelocity;
    private Vector3 m_Velocity;
    private int frame;
    public Rigidbody m_Body;
    private Collider col;


    AdvancedBossAi boss;
    Transform[] m_Torches;

    // Use this for initialization
    void Start()
    {
        boss = boss = GameObject.Find("Boss").GetComponent<AdvancedBossAi>();
        m_Velocity = m_ProjectileVelocity;
        m_Torches = boss.torches;
        frame = 0;
        col = GetComponent<Collider>();
    }

    //Called once object is activated
    void OnEnable()
    {
        frame = 0;
        m_FirstPosition = gameObject.transform;
        m_Velocity = m_ProjectileVelocity;
    }

    // Update is called once per frame
    void Update()
    {

        //Manage frame
        frame++;

        //Update velocity
        m_Body.velocity = m_Velocity;

        //Begining
        if (frame <= m_OutTime)
        {
            m_Velocity = m_ProjectileVelocity;
            m_StartingVelocity = m_Velocity;
        }
        //Slow down
        if (frame > m_OutTime && frame <= m_OutTime + m_SlowTime)
        {
            m_Velocity.x -= m_StartingVelocity.normalized.x * 2f;
            m_Velocity.z -= m_StartingVelocity.normalized.z * 2f;
        }

        //Die
        if (frame > m_OutTime + m_SlowTime + m_BackTime)
        {
            gameObject.SetActive(false);
        }

        //Update collision

        if (transform.position.x > m_Torches[0].position.x && transform.position.x < m_Torches[1].position.x && transform.position.z < m_Torches[0].position.z && transform.position.z > m_Torches[2].position.z)
        {
            col.isTrigger = false;

        }
        else
        {
            col.isTrigger = true;

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            PlayerController playerScript = other.gameObject.GetComponent<PlayerController>();
            HeartSystem health = other.gameObject.GetComponent<HeartSystem>();

            float knockback = 10f;
            playerScript.m_Velocity = Vector3.Normalize(transform.position - other.gameObject.transform.position) * knockback;
            //Deal damage
            health.TakeDamage(1);
            gameObject.SetActive(false);

        }

    }
}
