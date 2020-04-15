using UnityEngine;
using System.Collections;

public class StateEffect : MonoBehaviour
{
    public float m_StunTime = 5f;
    public float m_KnockBack = 50f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyEffect>() != null)
        {
            EnemyEffect enemyEffect = other.gameObject.GetComponent<EnemyEffect>();
            enemyEffect.Stun(m_StunTime);
            enemyEffect.KnockBack(m_KnockBack, gameObject);
        }
    }
}
