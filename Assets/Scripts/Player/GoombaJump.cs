using UnityEngine;
using System.Collections;

public class GoombaJump : MonoBehaviour {

    CharacterController controller;
    public float m_StunTime = 5f;
    public float m_Damage = 2f;

    void Start()
    {
        // Cannot access to CharacterController for pickup.
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(controller.isGrounded);
        if (!controller.isGrounded)
        {
            if (other.gameObject.GetComponent<EnemyEffect>() != null)
            {
                EnemyEffect enemyEffect = other.gameObject.GetComponent<EnemyEffect>();
                enemyEffect.Stun(m_StunTime);
            }
            if (other.gameObject.GetComponent<EnemyHealth>() != null)
            {
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.Damage(m_Damage);
            }
        }
    }

}
