using UnityEngine;
using System.Collections;

public class EnemySfx : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] SFX;
    private AudioClip SFXtoPlay;
    private EnemyHealth enemyHealthValue;
    public float PreviousEnemyHealth;
    public float CurrentEnemyHealth;
    // Use this for initialization
    void Start()
    {
        enemyHealthValue = GetComponent<EnemyHealth>();
        PreviousEnemyHealth = enemyHealthValue.m_EnemyHealth;
        CurrentEnemyHealth = enemyHealthValue.m_EnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthValue = GetComponent<EnemyHealth>();
        CurrentEnemyHealth = enemyHealthValue.m_EnemyHealth;
        if (PreviousEnemyHealth > CurrentEnemyHealth)
        {
            SFXtoPlay = SFX[Random.Range(0, SFX.Length)];
            audioSource.clip = SFXtoPlay;
            audioSource.Play();
            PreviousEnemyHealth = enemyHealthValue.m_EnemyHealth;
        }
    }
}