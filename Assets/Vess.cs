using UnityEngine;
using System.Collections;

public class Vess : MonoBehaviour
{
    public float m_StunTime;
    public GameObject m_explosion;
    public GameObject m_BoomEffect;

    //SFX
    public GameObject SFXPlayer;
    public AudioClip[] SFX;
    private AudioClip SFXtoPlay;
    public float maxRandomPitch;
    public float minRandomPitch;
    private float randomPitch;
    //SFX End

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject Boom;
            Boom = (GameObject)Instantiate(m_BoomEffect, gameObject.transform.position, gameObject.transform.rotation);
            //StartCoroutine(Test());
            Destroy(gameObject);
            Destroy(Boom, 4f);
            Player p = collision.gameObject.GetComponent<Player>();
            p.stun(m_StunTime);
            //Debug.Log("Hit Player");
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            //Debug.Log("Hit Wall");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<StateEffect>() != null)
        {
            GameObject explosion;
            explosion = (GameObject)Instantiate(m_explosion, gameObject.transform.position, gameObject.transform.rotation);
            //StartCoroutine(Test());
            Destroy(gameObject);
            Destroy(explosion, 4f);
            //Debug.Log("Hit Weapon");
            //SFX
            //James Sound Code

            randomPitch = Random.RandomRange(maxRandomPitch, minRandomPitch);
            if (randomPitch > 3)
            {
                randomPitch = 3;
            }

            if (SFXPlayer != null)
            {
                SFXtoPlay = SFX[Random.Range(0, SFX.Length)];
                AudioSource source = SFXPlayer.GetComponent<AudioSource>();
                source.pitch = randomPitch;
                source.clip = SFXtoPlay;
            }

            GameObject SFXtest = Instantiate(SFXPlayer, transform.position, transform.rotation) as GameObject;

            //James Shound Code
            //SFX END
        }

    }
}
