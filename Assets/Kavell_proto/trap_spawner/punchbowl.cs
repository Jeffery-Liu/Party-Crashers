using UnityEngine;
using System.Collections;

public class punchbowl : MonoBehaviour {

	[SerializeField]GameObject activeZone;
	[SerializeField]GameObject pickupZone;
	// Use this for initialization
	void Start () {
		pushrug pushCheck = activeZone.GetComponent<pushrug> ();
		pickuprug pickupCheck = activeZone.GetComponent<pickuprug> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void pickup()
	{
		pushrug pushCheck = activeZone.GetComponent<pushrug> ();
		pickuprug pickupCheck = activeZone.GetComponent<pickuprug> ();
		if (pushCheck.active == true && pickupCheck.active == true) 
		{
			
		}
	}
}
