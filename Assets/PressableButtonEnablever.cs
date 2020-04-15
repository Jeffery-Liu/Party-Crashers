using UnityEngine;
using System.Collections;

public class PressableButtonEnablever : MonoBehaviour
{

    private Vector3 buttonstartPosition;
    public Transform buttondownPosition;
    public Transform ButtonPosition;
    public bool buttonDownPermanent;
    public int requiredCarryAmmount;
    public bool AllowPlayers;
    public bool AllowEnemies;
    private int carryAmmount = 0;
    public float smooth;
    public bool EnableObject;
    private bool isDown = false;
 
     //sounds
     public AudioSource audioSource;
     public AudioClip[] SFXDown;
     public AudioClip[] SFXUp;
     private AudioClip SFXtoPlay;
     //sound end

    public GameObject[] objectToEnable;
    private int I;


    // Use this for initialization
    void Start()
    {
        buttonstartPosition = ButtonPosition.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        I = 0;
        if (carryAmmount >= requiredCarryAmmount)
        {
            
            ButtonPosition.position = Vector3.Lerp(ButtonPosition.position, buttondownPosition.position, smooth * Time.deltaTime);
            
            while (objectToEnable.Length > (I))
            {
                objectToEnable[I].SetActive(EnableObject);
                I++;
            }
            //sound
            if (!isDown)
            {
               SFXtoPlay = SFXDown[Random.Range(0, SFXDown.Length)];
               audioSource.clip = SFXtoPlay;
               audioSource.Play();
            }
            //sound end
            isDown = true;

        }
        else if (buttonDownPermanent == false)
        {
            ButtonPosition.position = Vector3.Lerp(ButtonPosition.position, buttonstartPosition, smooth * Time.deltaTime);
            while (objectToEnable.Length > (I))
            {
                objectToEnable[I].SetActive(!EnableObject);
                I++;
            }
            
            //sound
            if (isDown)
            {
               SFXtoPlay = SFXUp[Random.Range(0, SFXUp.Length)];
               audioSource.clip = SFXtoPlay;
               audioSource.Play();
            }
            //sound end
            isDown = false;
        }

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
}
