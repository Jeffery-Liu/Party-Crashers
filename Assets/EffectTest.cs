using UnityEngine;
using System.Collections;

public class EffectTest : MonoBehaviour {

    public GameObject explosionPickupFX;


    void OnTriggerEnter(Collider other)
    {
        //VfX
        if (explosionPickupFX != null)
        {
            GameObject pickUpVFX;
            pickUpVFX = (GameObject)Instantiate(explosionPickupFX, other.transform.position, transform.rotation);
            Destroy(pickUpVFX, 1.0f);
        }
    }
}

    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Players")
        {
            Debug.Log("Die ball!");
            pickUpVFX = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject, 0.1f); // also sometimes destroying right away causes problems
        }
    }
}
*/