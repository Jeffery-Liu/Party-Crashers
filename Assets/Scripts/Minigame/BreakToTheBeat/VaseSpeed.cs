using UnityEngine;
using System.Collections;

public class VaseSpeed : MonoBehaviour
{
    public float velocity;
    private Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 forward = transform.forward;
        rb.velocity = forward * velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
