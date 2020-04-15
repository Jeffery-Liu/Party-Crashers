using UnityEngine;
using System.Collections;

public class TrapAnimation : MonoBehaviour {

    public Animator ani;
    public GameObject m_effect;

    public AudioSource audioSource;
    public AudioClip[] DoorSFX;
    private AudioClip SFXtoPlay;
    public float maxRandomPitch;
    public float minRandomPitch;
    private float randomPitch;
    // Use this for initialization
    void Start () {
        ani.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && ani.enabled == false && other.GetComponent<Player>().m_State == Player.State.Alive)
        {
            ani.enabled = true;
            if (m_effect != null)
            {
                GameObject effect;
                effect = (GameObject)Instantiate(m_effect, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(effect, 3f);
            }

            randomPitch = Random.RandomRange(maxRandomPitch, minRandomPitch);
            if (DoorSFX.Length > 0)
            {
                SFXtoPlay = DoorSFX[Random.Range(0, DoorSFX.Length)];
                audioSource.clip = SFXtoPlay;
                audioSource.pitch = randomPitch;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Audio not set for TrapAnimation under: " + gameObject.name);
            }
        }


    }
}
