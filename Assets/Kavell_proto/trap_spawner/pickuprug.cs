using UnityEngine;
using System.Collections;

public class pickuprug : MonoBehaviour {
	public bool active = false;

	void OnTriggerEnter(Collider other)
	{ 
		if (other.tag == "Player") 
		{	
			Debug.Log ("Someone is here");
			active = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{	
			Debug.Log ("Someone is here");
			active = true;
		}
	}
}
