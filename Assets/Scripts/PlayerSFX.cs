using UnityEngine;
using System.Collections;

public class PlayerSFX : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip[] ChewSFX;
    private AudioClip SFXtoPlay;

    public float maxRandomPitch;
    public float minRandomPitch;
    private float randomPitch;
    private Player m_Player;
    // Use this for initialization

    void Start()
    {
        m_Player = gameObject.GetComponent<Player>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectible>() && m_Player.m_State == Player.State.Alive) //chew
        {
            randomPitch = Random.RandomRange(maxRandomPitch, minRandomPitch);
            SFXtoPlay = ChewSFX[Random.Range(0, ChewSFX.Length)];
            audioSource.clip = SFXtoPlay;
            audioSource.pitch = randomPitch;
            audioSource.Play();
        }

    }
}
