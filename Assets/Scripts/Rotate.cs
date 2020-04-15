using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	void FixedUpdate () {
        transform.Rotate(new Vector3(0, Random.Range(1, 360), 0) * Time.deltaTime);
	}
}
