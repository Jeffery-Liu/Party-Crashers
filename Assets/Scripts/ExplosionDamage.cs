using UnityEngine;
using System.Collections;

public class ExplosionDamage : MonoBehaviour {

    public float damage;

    public float destroyDelay;

    void Start()
    {

        Destroy(this.gameObject, destroyDelay);

    }

}
