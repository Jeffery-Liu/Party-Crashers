using UnityEngine;
using System.Collections;

public class kavell_destroy_on_collision : MonoBehaviour {


	public void OnCollisionEnter(Collision coll)
	{
		Destroy(gameObject);
	}
}
