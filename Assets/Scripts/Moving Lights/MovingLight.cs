using UnityEngine;
using System.Collections;

public class MovingLight : MonoBehaviour {

    public Transform startPosition;
    public Transform endPosition;
    public Transform lightPosition;
    private Vector3 newPosition;

    public bool movingtoStart;
    public float smooth;
    public float resetTime;
	// Use this for initialization
	void Start ()
    {
       changeTarget();
    }
	
	// Update is called once per frame
	void Update ()
    {
        lightPosition.position = Vector3.Lerp(lightPosition.position, newPosition, smooth * Time.deltaTime);


    }

    void changeTarget()
    {
        if (movingtoStart == true)
        {
            movingtoStart = false;
            newPosition = endPosition.position;
        }
        else if (movingtoStart == false)
        {
            movingtoStart = true;
            newPosition = startPosition.position;
        }
        else
        {
            movingtoStart = false;
            newPosition = endPosition.position;
        }
     
        
        Invoke("changeTarget", resetTime);

    }
}
