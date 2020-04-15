using UnityEngine;
using System.Collections;

public class HealthBarOrientation : MonoBehaviour {

	[SerializeField]private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (transform.position + mainCamera.transform.rotation * Vector3.back, 
			mainCamera.transform.rotation * Vector3.up);
	}
}
