using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using System;

public class Sword : Melee
{
    [Header("Sword Setting")]
    [SerializeField]
    private float m_DashTime = 1.0f;
    public float m_DashDistance = 50f;
    [SerializeField]
    private float dashDelay = 0.1f;
    [SerializeField]
    private float smooth = 20f;
    [SerializeField]
    private float triggerLife = 0.25f;
    private float numOfParticles = 0;
    public float m_SecondSlashTime = 0.75f;

    public GameObject followEffect;

    [SerializeField]
    public bool attack { get; private set; }

    public GameObject effect;

    public GameObject DashVFX;

    private bool m_FirstAnimation;
    private float m_FirstAnimCooldown;

    private float m_DashCooldown;

    CharacterController m_CharacterController;
    Player m_Player;

    //SFX
    private AudioManager SFXManager;
    public AudioSource audioSource;
    public AudioClip[] dashSFX;
    private AudioClip SFXtoPlay;

    public AudioClip[] slashSFX;
    private AudioClip slashSFXtoPlay;

    public float maxRandomPitch;
    public float minRandomPitch;
    private float randomPitch;
    //SFX End

    void Start()
    {
        SFXManager = GetComponent<AudioManager>();
        m_CharacterController = GetComponentInParent<CharacterController>();
        m_Player = GetComponentInParent<Player>();
        sliceEffect.SetActive(false);
        swordTrigger.SetActive(false);
    }

    void Update()
    {
        m_FirstAnimCooldown += Time.deltaTime;
        m_DashCooldown -= Time.deltaTime;

        if(m_DashCooldown > 0)
        {
            m_CharacterController.Move(m_CharacterController.transform.forward * Time.deltaTime * m_DashDistance);
        }

        if (attack == true)
        {
            triggerLife -= Time.deltaTime;
            sliceEffect.SetActive(true);
            swordTrigger.SetActive(true);
            //SFX Start
            if (audioSource != null && !audioSource.isPlaying)
            {
                randomPitch = UnityEngine.Random.Range(maxRandomPitch, minRandomPitch);
                slashSFXtoPlay = slashSFX[UnityEngine.Random.Range(0, slashSFX.Length)];
                audioSource.clip = slashSFXtoPlay;
                audioSource.pitch = randomPitch;
                audioSource.Play();
            }
            //SFX END

            if (effect && numOfParticles <= 0)
            {
                numOfParticles = 1;
                GameObject swordEffect;
                swordEffect = (GameObject)Instantiate(effect, transform.position, transform.rotation);

                Destroy(swordEffect, 1);

            }
        }

        if (triggerLife <= 0)
        {
            attack = false;
            swordTrigger.SetActive(false);
            sliceEffect.SetActive(false);
            numOfParticles = 0;
            triggerLife = 0.5f;
        }

        #region Erase if no bugs
        //  if (attack == true)
        //  {
        //      triggerLife -= Time.deltaTime;
        //  }

        //  if (triggerLife <= 0)
        //  {
        //      attack = false;
        //      triggerLife = 0.5f;

        //  }

        //  if (attack == true)
        //  {
        //      swordTrigger.SetActive(true);

        //if (effect)
        //      {
        //	GameObject swordEffect;
        //	swordEffect = (GameObject)Instantiate (effect, transform.position, transform.rotation);

        //	Destroy (swordEffect, 5);
        //}
        //  }

        //  if (attack == false)
        //  {
        //      swordTrigger.SetActive(false);
        //  }        
        #endregion        
    }

    override public void primaryAttack()
    {
        if (m_CoolDown <= Time.time - m_Weapon1Cooldown || m_CoolDown == 0)
        {
            if (m_FirstAnimCooldown >= m_SecondSlashTime)
            {
                m_Player.m_Animator.SetBool("isSlashing", true);
                StartCoroutine(setPrimaryAttackFalse(1));
                m_FirstAnimCooldown = 0;
            }
            else
            {
                m_Player.m_Animator.SetBool("isSlashing2", true);
                StartCoroutine(setPrimaryAttackFalse(2));
                m_FirstAnimCooldown = m_SecondSlashTime;
            }
            attack = true;
            m_CoolDown = Time.time;
        }
    }

    override public void secondaryAttack()
    {
        m_Player.m_Animator.SetBool("isDashing", true);
        StartCoroutine(setSecondaryAttackFalse());
        if (m_SecondaryCoolDown <= Time.time - m_Weapon2Cooldown || m_SecondaryCoolDown == 0)
        {
            //SFX Start
            //if (audioSource != null)
            //{
            //    randomPitch = Random.RandomRange(maxRandomPitch, minRandomPitch);
            //    SFXtoPlay = dashSFX[Random.Range(0, dashSFX.Length)];
            //    audioSource.clip = SFXtoPlay;
            //    audioSource.pitch = randomPitch;
            //    SFXManager.PushMusic(SFXtoPlay);
            //    audioSource.Play();
            //}
            //SFX END
            attack = true;
            m_DashCooldown = m_DashTime;
            followEffect.SetActive(true);
           
            if(DashVFX != null)
            {
                GameObject DashMF;
                DashMF = (GameObject)Instantiate(DashVFX, transform.position, transform.rotation);
                Destroy(DashMF, 0.2f);
            }

            m_SecondaryCoolDown = Time.time;
        }
    }

    public override void terminate()
    {
    }

    /*IEnumerator dash()
    {
        yield return new WaitForSeconds(dashDelay);

        m_SecondaryCoolDown = Time.time;
    }*/

    private IEnumerator setPrimaryAttackFalse(int i)
    {
        yield return new WaitForSeconds(.1f);
        if(i == 1)
            m_Player.m_Animator.SetBool("isSlashing", false);
        else if(i == 2)
            m_Player.m_Animator.SetBool("isSlashing2", false);
    }

    private IEnumerator setSecondaryAttackFalse()
    {
        yield return new WaitForSeconds(.1f);
        m_Player.m_Animator.SetBool("isDashing", false);
        followEffect.SetActive(false);
    }

    void OnDestroy()
    {
        sliceEffect.SetActive(false);
        swordTrigger.SetActive(false);
        m_DashCooldown = 0;
    }
}
