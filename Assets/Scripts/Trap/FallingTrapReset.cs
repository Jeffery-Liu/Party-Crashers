using UnityEngine;
using System.Collections;

public class FallingTrapReset : MonoBehaviour
{
    
    public float FallingSpeed;

    private float mMass;
    private bool mActivated;

    GameObject mTrap;

    //reset
    public Transform startPosition;
    public Transform trapPosition;
    public float smooth;
    public float resetTime;
    private Vector3 newPosition;

    void Start()
    {
        mTrap = this.gameObject;
        mMass = 1000;
        mActivated = false;
    }

    void Update()
    {
        ResetAfterActivation();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            //Debug.Log("Falling trap Activated.");

            Rigidbody rb = mTrap.GetComponent<Rigidbody>();
            rb.mass = mMass;
            if (FallingSpeed >= 1000)
            {
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            rb.AddForce(Physics.gravity * (FallingSpeed * 100));
            rb.useGravity = true;
            mActivated = true;
        }
    }

    void ResetAfterActivation()
    {
        if (mActivated)
        {

            resetPosition();
        }
    }
    void resetPosition()
    {
        Rigidbody rb = mTrap.GetComponent<Rigidbody>();
        rb.useGravity =false;
        //mActivated = false;
        newPosition = startPosition.position;
        trapPosition.position = Vector3.Lerp(trapPosition.position, newPosition, smooth * Time.deltaTime);
        trapPosition.position = newPosition;
    }

}