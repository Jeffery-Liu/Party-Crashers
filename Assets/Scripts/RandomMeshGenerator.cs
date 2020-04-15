using UnityEngine;
using System.Collections;

public class RandomMeshGenerator : MonoBehaviour {

    //Cooper Klassen
    //Tuesday Januray 17, 2017
    //Script that makes the coin a random pickup

    public float scaleValue;
    public GameObject[] pickupGameObject;
    private int randomNum;

    void Start()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;

        randomNum = Random.Range(0,pickupGameObject.Length);
        GameObject pickup = GameObject.Instantiate(pickupGameObject[randomNum],transform.position,transform.rotation) as GameObject;
        pickup.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        pickup.transform.parent = transform;

    }

}
