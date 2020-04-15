using UnityEngine;
using System.Collections;

public class StickyTrap : Trap {

    public float m_StuckTime;
    public GameObject m_effect;
    private PlayerController playerController;

    //SFX Start
    public AudioSource audioSource;
    public AudioClip[] StuckSFX;
    private AudioClip SFXtoPlay;
    //SFX END

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HeartSystem>() != null)
        {
            playerController = other.GetComponent<PlayerController>(); 
            if (m_CurrentCooldown <= Time.time - m_Cooldown || m_CurrentCooldown == 0)
            {
                if(m_effect != null)
                {
                    GameObject effect;
					effect = (GameObject)Instantiate(m_effect, other.transform.position, Random.rotation);  
					Destroy(effect, m_StuckTime);
                }
                //SFX
                if (audioSource != null)
                {
                    SFXtoPlay = StuckSFX[Random.Range(0, StuckSFX.Length)];
                    audioSource.clip = SFXtoPlay;
                    audioSource.Play();
                }
                //SFX End
                playerController.m_CantMove = true;
                m_CurrentCooldown = Time.time;
                StartCoroutine("getUnstuck");
            }
        }
        if (other.gameObject.GetComponent<EnemyEffect>() != null)
        {
            EnemyEffect m_EnemyEffect = other.gameObject.GetComponent<EnemyEffect>();
            if (m_CurrentCooldown <= Time.time - m_Cooldown || m_CurrentCooldown == 0)
            {
                if (m_effect != null)
                {
                    GameObject effect;
                    effect = (GameObject)Instantiate(m_effect, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(effect, 3f);
                }
                m_EnemyEffect.Stun(m_StuckTime);
                m_CurrentCooldown = Time.time;
            }
        }
    }
    IEnumerator getUnstuck()
    {
        yield return new WaitForSeconds(m_StuckTime);
        if(playerController != null)
        {
            playerController.m_CantMove = false;
        }
        else
        {
            Debug.Log("Sticky Trap PlayerController value is null");
        }
    }

}
