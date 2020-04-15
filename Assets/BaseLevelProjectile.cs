using UnityEngine;
using System.Collections;

public class BaseLevelProjectile : MonoBehaviour {

    public float m_KnockBack = 30f;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Weapon>() != null)
        {
            GameObject player = other.gameObject.GetComponent<Weapon>().transform.GetComponentInParent<PlayerController>().gameObject;
            Vector3 KnockBackDirection = player.transform.position - transform.position;
            player.GetComponent<PlayerController>().m_Velocity = KnockBackDirection.normalized * m_KnockBack;
        }

    }
}
