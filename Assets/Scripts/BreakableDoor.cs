using UnityEngine;
using System.Collections;

public class BreakableDoor : MonoBehaviour {

    public float explosionRadius;
    public GameObject explosionObject;
    public GameObject particleLocation;

    public AudioSource audioSource;
    public AudioClip[] DoorSFX;
    private AudioClip SFXtoPlay;
    public float maxRandomPitch;
    public float minRandomPitch;
    private float randomPitch;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Projectile")
        {
            GameObject explosion;
            explosion = (GameObject)Instantiate(explosionObject, particleLocation.gameObject.transform.position, particleLocation.gameObject.transform.rotation);
            explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
            Destroy(gameObject);
        }

        if (other.tag == "Physical")
        {
            GameObject explosion;
            explosion = (GameObject)Instantiate(explosionObject, particleLocation.gameObject.transform.position, particleLocation.gameObject.transform.rotation);
            explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
            Destroy(gameObject);
        }

        randomPitch = Random.RandomRange(maxRandomPitch, minRandomPitch);
        SFXtoPlay = DoorSFX[Random.Range(0, DoorSFX.Length)];
        audioSource.clip = SFXtoPlay;
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }

}
