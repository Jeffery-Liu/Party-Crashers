using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public int m_Damage;

    public int hitmaxChance;
    public int hitChanceNumber;
    public AudioClip[] DamageSFX;
    public AudioClip SFXtoPlay;
    static private int Chance = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HeartSystem>() != null)
        {
            HeartSystem playerHealth = other.gameObject.GetComponent<HeartSystem>();
            playerHealth.TakeDamage(m_Damage);
            playerHealth.UpdateHearts();

            hitChanceNumber = Random.Range(0, hitmaxChance);
            if (hitChanceNumber == Chance)
            {
                SFXtoPlay = DamageSFX[Random.Range(0, DamageSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay);
            }
        }
    }
}
