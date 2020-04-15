using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Transform target;
    private Vector3 offset;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("Player").transform;
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = target.position + offset;
    }
}

