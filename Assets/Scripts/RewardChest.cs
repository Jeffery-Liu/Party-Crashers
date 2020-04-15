using UnityEngine;
using System.Collections;
using System;

using Random = UnityEngine.Random;

public class RewardChest : MonoBehaviour
{
    public float DestructionDelay;

    public GameObject[] items;

    private bool Activated = false;
    private bool mActivated;

    GameObject rewardChest;
    GameObject itemPrefab;

    void Start ()
    {
        rewardChest = this.gameObject;
        mActivated = false;
    }
	
	void Update ()
    {
        // Comment this to desactivate the GameObject destruction.
        DestroyAfterActivation();
    }

    private GameObject RandomItem()
    {
        return items[Random.Range(0, items.Length/*-1*/)]; 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Reward Chest Activated.");

            Vector3 itemPos = rewardChest.transform.position + rewardChest.transform.up;    // Will Spawn on top of chest. Change ".up" to ".forward", ".back", ".right" or ".left" for different spawn locations.
            // Change for " = new Vector3(0, 0, 0);" For a specific location.

            itemPrefab = RandomItem();
            GameObject spawnedItem = Instantiate(itemPrefab, itemPos, itemPrefab.transform.rotation) as GameObject;

            //This line will prevent the chest from activating more than once. Comment to Use the chest multiple times.
            this.GetComponent<SphereCollider>().enabled = false;

            mActivated = true;
        }
    }

    void DestroyAfterActivation()
    {
        if (mActivated == true)
        {
            Destroy(rewardChest, DestructionDelay);
        }
    }
}
