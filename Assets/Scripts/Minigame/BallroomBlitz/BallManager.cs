using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
    public enum EBallType { Basic, Stun, Bomb };           // Types of balls: Basic(pink), Stun(yellow), and Bomb(black).

    [HideInInspector]
    public float m_StunTime;

    public float m_KnockBackIntensity;
    public float m_BombKnockBackIntensity;

    private Vector3 m_KnockBackDirection;
    private EBallType m_BallType;
    private BallPoolManager m_BallPoolManager;
    private bool m_IsCoroutineExecuting;

    //VFX James
    public GameObject m_explosion;
   
    //VFX James

    void Start()
    {
        m_IsCoroutineExecuting = false;
    }

    void Update()
    {
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the collision happened to a player
        if (other.tag == "Player")
        {
            //If it hits a player, we have to check the type of ball that we have
            switch (m_BallType)
            {
                case EBallType.Basic:
                    if(!other.GetComponent<Player>().m_IsDead)
                    {
                        var playerController = other.GetComponent<PlayerController>();
                        m_KnockBackDirection = (other.transform.position - transform.position).normalized;

                        // Check if player is stunned
                        if (playerController.m_CantMove)
                            playerController.m_CantMove = false;

                        playerController.m_Velocity = m_KnockBackDirection * m_KnockBackIntensity;
                    }
                    break;
                case EBallType.Stun:
                    if (!other.GetComponent<Player>().m_IsDead)
                    {
                        other.GetComponent<Player>().stun(m_StunTime);
                    }
                    break;
                case EBallType.Bomb:
                    BombBallExplosion();
                    break;
                default:
                    Debug.Log("Ball type not set!");
                    break;
            }
        }
    }

    // When ball leaves playing area (defined by a collision box set as trigger),
    // or hits a player, this ball should go back to the ball pool
    void OnTriggerExit(Collider other)
    {
        if (gameObject.activeInHierarchy && (other.tag == "Player" ||  other.tag == "PlayingVolume"))
        {
            m_BallPoolManager.PutBallBackIntoPool(gameObject);
        }
    }

    // Method used by the BallLauncherManager to set the ball type when it is created
    public void SetBallType(EBallType type)
    {
        m_BallType = type;
    }

    // Method used by the BallLauncherManager to set itself as the ball manager
    // It's necessary, since the BallLauncherManager controls the ball pool.
    public void SetBallPoolManager(BallPoolManager manager)
    {
        m_BallPoolManager = manager;
    }

    public void BombBallExplosion()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //vfx Explosion
        if(m_explosion != null)
        {
            GameObject explosion;
            explosion = (GameObject)Instantiate(m_explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        //vfx Explosion

        foreach (var player in players)
        {
            // check if player is not a pinata
            if(!player.GetComponent<Player>().m_IsDead)
            {
                var playerController = player.GetComponent<PlayerController>();
                m_KnockBackDirection = (player.transform.position - transform.position).normalized;

                // Check if player is stunned
                if (playerController.m_CantMove)
                    playerController.m_CantMove = false;

                playerController.m_Velocity = m_KnockBackDirection * m_BombKnockBackIntensity;
            }
        }

        if (gameObject.activeInHierarchy)
        {
            m_BallPoolManager.PutBallBackIntoPool(gameObject);
        }
    }
}
