using UnityEngine;
using System.Collections;

public class pushrug : MonoBehaviour {

	public bool active = false;
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Somthing is here");
		active = true;
	}
	void OnTriggerExit(Collider other)
	{
		Debug.Log("Somthing left here");
		active = false;
	}

}
