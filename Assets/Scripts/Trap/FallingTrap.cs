using UnityEngine;
using System.Collections;

public class FallingTrap : MonoBehaviour {

    public float FallingSpeed;
    //public float DestructionDelay;
    public int m_Damage;

    private float mMass;
    private bool mActivated;
    private HeartSystem m_Heart;
    GameObject mTrap;
    private float m_DeleteCounter = 1.5f;
    private bool m_IsUsed;
    //SFX
    public AudioSource audioSource;
    public AudioClip[] FallSFX;
    public AudioClip[] hitPlayerSFX;
    private AudioClip SFXtoPlay;
    
    //SFX
    // 

    void Start ()
    {
        mTrap = this.gameObject;
        mMass = 1000;
        mActivated = false;
	}
	
	void Update ()
    {
        DestroyAfterActivation();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HeartSystem>() != null || other.GetComponent<EnemyHealth>() != null)
        {

            //Debug.Log("Falling trap Activated.");
            //SFX
            if (audioSource != null)
            {
                SFXtoPlay = FallSFX[Random.Range(0, FallSFX.Length)];
                audioSource.clip = SFXtoPlay;
                audioSource.Play();
            }
            //SFX End
            m_IsUsed = true;
            Rigidbody rb = mTrap.GetComponent<Rigidbody>();
            rb.mass = mMass;
            if(FallingSpeed >= 1000)
            {
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            rb.AddForce(Physics.gravity * (FallingSpeed * 100));
            rb.useGravity = true;
            mActivated = true;
        }
    }

    void DestroyAfterActivation()
    {
        if(m_IsUsed == true)
        {
            m_DeleteCounter -= Time.deltaTime;

            if (m_DeleteCounter <= 0)
            {
                Destroy(mTrap);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Player>() != null && !other.gameObject.GetComponent<Player>().m_IsDead)
        {
            //SFX
            if (audioSource != null)
            {
                SFXtoPlay = hitPlayerSFX[Random.Range(0, hitPlayerSFX.Length)];
                audioSource.clip = SFXtoPlay;
                audioSource.Play();
            }
            //SFX End

            m_Heart = other.gameObject.GetComponent<HeartSystem>();
            m_Heart.TakeDamage(m_Damage);
            m_Heart.UpdateHearts();
        }
    }
}
