using UnityEngine;
using System.Collections;

public class RotatingLight : MonoBehaviour {

    // Use this for initialization
    public float degreesPerSecond;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * degreesPerSecond * Time.deltaTime);
    }
}