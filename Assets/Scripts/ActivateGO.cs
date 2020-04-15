using UnityEngine;
using System.Collections;

public class ActivateGO : MonoBehaviour
{

    public GameObject gameobject;


    void Start()
    {
        gameobject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                gameobject.SetActive(true);
            }
        }
    }
}
