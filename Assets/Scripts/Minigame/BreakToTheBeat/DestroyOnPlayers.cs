using UnityEngine;
using System.Collections;

public class DestroyOnPlayers : MonoBehaviour {
    private Collider col;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        //------------ Sword -----------------
        //if (collision.gameObject.tag == "Sword")
        //{
        //    Destroy(gameObject);
        //}
    }
}
