using UnityEngine;
using System.Collections;

public class SimplePlayerController : MonoBehaviour
{
    public float speed;

    GameObject m_player;
    Rigidbody rb;

    void Start()
    {
        m_player = this.gameObject;
        rb = m_player.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector3.right * speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector3.forward * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Vector3.back * speed;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
