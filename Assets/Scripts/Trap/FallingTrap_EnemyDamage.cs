using UnityEngine;
using System.Collections;

public class FallingTrap_EnemyDamage : MonoBehaviour {

    public int m_Damage;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>() != null)
        {
            EnemyHealth m_EnemyHealth = other.GetComponent<EnemyHealth>();
            m_EnemyHealth.Damage(m_Damage);
        }
    }
}
