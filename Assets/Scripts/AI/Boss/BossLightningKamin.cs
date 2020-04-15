using UnityEngine;
using System.Collections;

public class BossLightningKamin : MonoBehaviour
{

    [HideInInspector]
    public Vector3 m_ProjectileVelocity;
    public float m_Life;


    private int frame;
    private Rigidbody m_Body;

    // Use this for initialization
    void Start()
    {
        m_Body = GetComponent<Rigidbody>();
        m_Body.velocity = m_ProjectileVelocity;
        frame = 0;
    }

    //Called once object is activated
    void OnEnable()
    {
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_Body.velocity = m_ProjectileVelocity;
        frame++;

        if (frame > m_Life)
        {
            gameObject.SetActive(false);
        }

        transform.position = new Vector3(transform.position.x, 5.4f, transform.position.z);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
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
