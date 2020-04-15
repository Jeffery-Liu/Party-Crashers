using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{

    public Transform startPosition;
    public Transform endPosition;
    public Transform ElevatorPosition;
    private Vector3 newPosition;
    // Use this for initialization
    public int requiredCarryAmmount;
    public bool AllowEnemies;
    public bool AllowPlayers;
    private int carryAmmount = 0;

    private bool movingtoStart;
    public bool resetToStart;
    public float resetTime;
    public float smooth;


    //sound
    public AudioSource audioSource;
    private bool soundMuted = false;
    //sound end

    void Start()
    {
        movingtoStart = false;
        newPosition = endPosition.position;
        changeTarget();

    }


    // Update is called once per frame
    void Update()
    {
        if (carryAmmount >= requiredCarryAmmount)
        {
            if (resetToStart == false)
            {
                Invoke("muteSound", resetTime);
            }
            ElevatorPosition.position = Vector3.Lerp(ElevatorPosition.position, newPosition, smooth * Time.deltaTime);
            if (soundMuted == false)
            {
                audioSource.volume = 1;
            }

        }
        else
        {
            audioSource.volume = 0;
        }

    }

    void changeTarget()
    {
        if (carryAmmount >= requiredCarryAmmount && resetToStart == true)
        {
            if (movingtoStart == true)
            {
                movingtoStart = false;
                newPosition = endPosition.position;
            }
            else if (movingtoStart == false)
            {
                movingtoStart = true;
                newPosition = startPosition.position;
            }
            else
            {
                movingtoStart = false;
                newPosition = endPosition.position;
            }
        }
        else
        {

            audioSource.volume = 0;
        }


        Invoke("changeTarget", resetTime);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && AllowPlayers == true)
        {
            carryAmmount++;
        }
        if ((other.gameObject.tag == "ChaserEnemy" || other.gameObject.tag == "MeleeEnemy" || other.gameObject.tag == "HeavyEnemy") && AllowEnemies == true)
        {
            carryAmmount++;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && AllowPlayers == true)
        {
            carryAmmount--;
        }
        if ((other.gameObject.tag == "ChaserEnemy" || other.gameObject.tag == "MeleeEnemy" || other.gameObject.tag == "HeavyEnemy") && AllowEnemies == true)
        {
            carryAmmount--;
        }
    }

    void muteSound()
    {
        soundMuted = true;
        audioSource.volume = 0;
    }

}
