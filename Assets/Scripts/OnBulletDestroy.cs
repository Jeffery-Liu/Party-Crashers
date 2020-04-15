using UnityEngine;
using System.Collections;

public class OnBulletDestroy : MonoBehaviour {

    public GameObject deathEffect;

    void OnDestroy()
    {
        GameObject deathPrefab = Instantiate(deathEffect, transform.position, transform.rotation) as GameObject;
    }
}
