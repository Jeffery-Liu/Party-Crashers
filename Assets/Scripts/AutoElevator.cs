using UnityEngine;
using System.Collections;

public class AutoElevator : MonoBehaviour {

    private Vector3 startPosition;
    public Transform endPosition;
    public Transform elevatorPosition;
    private Vector3 newPosition;

    private bool movingtoStart;
    public float resetTime;
    public float smooth;
    // Use this for initialization
    void Start ()
    {
        startPosition = elevatorPosition.position;
        movingtoStart = false;
        newPosition = endPosition.position;
        changeTarget();
    }
	
	// Update is called once per frame
	void Update ()
    {
        elevatorPosition.position = Vector3.Lerp(elevatorPosition.position, newPosition, smooth * Time.deltaTime);
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
                newPosition = startPosition;
            }
            else
            {
                movingtoStart = false;
                newPosition = endPosition.position;
            }


        Invoke("changeTarget", resetTime);

    }
}
