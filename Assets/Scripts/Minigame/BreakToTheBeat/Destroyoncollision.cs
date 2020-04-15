using UnityEngine;
using System.Collections;

public class Destroyoncollision : MonoBehaviour {

	// Use this for initialization

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }


}
