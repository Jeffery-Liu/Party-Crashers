using UnityEngine;
using System.Collections;
using System;

public class FizzyPoP : Ranged
{

    [Header("Fizzy PoP Gun")]

    #region Floats
    public float m_PlayerRecoil = 50f;
    public float m_SprayTimer = 5f;
    [SerializeField]
    private float m_FallOffTimer = 2f;
    [SerializeField]
    private float m_AngleModifier = .5f;
    [SerializeField]
    private float m_FireHealthVFXTimer;
    [SerializeField]
    private float m_ProjectileSpeed02;
    #endregion

    #region Bools
    private bool m_IsDown = false;
    private bool m_CanHeal = false;

    #endregion

    #region Components
    private Player m_Player;
    private GameObject FizzyCone;
    [SerializeField]
    private GameObject ShootSprayVFX;
    [SerializeField]
    private GameObject FallOffSprayVFX;
    [SerializeField]
    private GameObject VFXFirePoint;

    public GameObject FallOffSprayVFXEarly;

    //SFX
    public AudioSource audioSource;
    public GameObject SecondarySFXPlayer;
    public AudioClip SecondarySFX;
    
    //SFX END
    #endregion

    GameObject ShootSprayGO;
    GameObject FallOffSpray;
    private float m_SprayCooldown;

    void Start()
    {
        FizzyCone = transform.FindChild("FizzyGunCone").gameObject;
        if (m_FirePoint[0].transform.FindChild("FizzyGunCone") != null)
        {
            Destroy(transform.FindChild("FizzyGunCone").gameObject);
            FizzyCone = m_FirePoint[0].transform.FindChild("FizzyGunCone").gameObject;
        }
        else
        {
            FizzyCone.transform.SetParent(m_FirePoint[0].transform);
            FizzyCone.transform.localPosition = new Vector3(0, 0, 0);
            FizzyCone.transform.localRotation = Quaternion.identity;
        }
        FizzyCone.GetComponent<Damage>().m_Damage = m_Damage;
        FizzyCone.SetActive(false);
        m_Player = GetComponentInParent<Player>();
    }

    void Update()
    {
        m_SprayCooldown -= Time.deltaTime;
       
        if (m_SprayCooldown < 0)
        {
            if (m_IsDown)
            {
                m_CoolDown = Time.time;
                m_IsDown = false;

                FizzyCone.SetActive(false);
                FallOffEarly();
                //SFX
                audioSource.mute = true;
                //SFX End
                if (ShootSprayGO != null)
                {
                    Destroy(ShootSprayGO, .2f);
                }
            }
        }

        if (Input.GetButtonUp(m_Player.m_PrimaryAttack + m_Player.m_Controller.ToString()))
        {
            m_SprayCooldown = -1;
            //SFX
            audioSource.mute = true;
            //SFX End


        //if (FallOffSpray != null)
        //{
        //    Destroy(FallOffSpray);
        //}
        //if (m_IsDown)
        //{
        //    m_CoolDown = Time.time;
        //    m_IsDown = false;

            //    FizzyCone.SetActive(false);
            //    FallOffEarly();
            //    if (ShootSprayGO != null)
            //    {
            //        Destroy(ShootSprayGO, .05f);
            //    }
            //}
        }

        // Shoot if Button Down
        //if (m_IsDown)
        //    ShootSpray();

        // Secondary
        //if (m_CanHeal)
        //{
        //    ShootHeal();
        //    Debug.Log("Is Shooting Spray");
        //}

    }


    public override void primaryAttack()
    {
        if (m_CoolDown <= Time.time - m_Weapon1Cooldown || m_CoolDown == 0)
        {
            ShootSpray();
            m_IsDown = true;

           
        }
    }

    public override void secondaryAttack()
    {
        if (m_SecondaryCoolDown <= Time.time - m_Weapon2Cooldown || m_SecondaryCoolDown == 0)
        {
            ShootHeal();
            m_SecondaryCoolDown = Time.time;
        }
    }

    public override void terminate()
    {
        if(ShootSprayGO != null)
        {
            Destroy(ShootSprayGO);
        }

        if(FallOffSpray != null)
        {
            Destroy(FallOffSpray);
        }
    }

    private void ShootSpray()
    {
        if (m_IsDown == false)
        {
            m_SprayCooldown = m_SprayTimer;

            FizzyCone.SetActive(true);

            //SFX
            audioSource.mute = false;
            //SFX End

            //StopCoroutine(ShootSprayTimer());
            //StartCoroutine(ShootSprayTimer());
            //m_IsDown = false;

            #region Fizzy Shoot VFX
            //bool ShootSprayVFXBool = false;

            if (ShootSprayVFX != null)
            {
                //if (!ShootSprayVFXBool)
                //{
                ShootSprayGO = (GameObject)Instantiate(ShootSprayVFX, m_FirePoint[1].transform.position, transform.GetComponentInParent<Player>().transform.rotation);
                ShootSprayGO.transform.parent = m_FirePoint[1].transform.parent.transform.parent;
                //ShootSprayGO.transform.Rotate(new Vector3(-90, 0, 0));
                ShootSprayGO.transform.localScale = new Vector3(1, 1, 1);
                if (m_Player.m_Model == Player.Model.Mascot)
                    ShootSprayGO.transform.localPosition = m_FirePoint[2].transform.localPosition;
                else
                    ShootSprayGO.transform.localPosition = m_FirePoint[1].transform.localPosition;
                //ShootSprayVFXBool = true;
                //Destroy(ShootSprayGO, (m_SprayTimer - 1f));

                //StopCoroutine(FallOffTimer());
                //StartCoroutine(FallOffTimer());
                //}
            }
            #endregion
        }
    }

    private void ShootHeal()
    {
        PlayerController playerController = transform.GetComponentInParent<PlayerController>();
        GameObject healPrefab;
        healPrefab = (GameObject)Instantiate(m_LeftTriggerProjectile, m_FirePoint[1].gameObject.transform.position + (playerController.transform.forward * 1.3f), m_FirePoint[1].gameObject.transform.rotation);
        healPrefab.GetComponent<Rigidbody>().AddForce(healPrefab.transform.forward * m_ProjectileSpeed02);
        playerController.m_Velocity = playerController.transform.forward.normalized * (m_PlayerRecoil * -1);

        //SFX Start
        if (SecondarySFX != null)
        {
            AudioSource source = SecondarySFXPlayer.GetComponent<AudioSource>();
            source.clip = SecondarySFX;
            GameObject SFXtest = Instantiate(SecondarySFXPlayer, transform.position, transform.rotation) as GameObject;
        }

       
        //SFX End
        //m_CanHeal = false;
    }

    private IEnumerator ShootSprayTimer()
    {
        yield return new WaitForSeconds(m_SprayTimer);
        FizzyCone.SetActive(false);
    }


    private void FallOffEarly()
    {
        if (FallOffSprayVFX != null)
        {
            FallOffSpray = (GameObject)Instantiate(FallOffSprayVFXEarly, m_FirePoint[1].transform.position, transform.GetComponentInParent<Player>().transform.rotation);
            FallOffSpray.transform.parent = m_FirePoint[1].transform.parent.transform.parent;
            //FallOffSpray.transform.Rotate(new Vector3(0, 0, 0));
            FallOffSpray.transform.localScale = new Vector3(1, 1, 1);
            if(m_Player.m_Model == Player.Model.Mascot)
                FallOffSpray.transform.localPosition = m_FirePoint[2].transform.localPosition;
            else
                FallOffSpray.transform.localPosition = m_FirePoint[1].transform.localPosition;
            Destroy(FallOffSpray, 2f);
        }
    }

    //private void FallOff()
    //{
    //    if (FallOffSprayVFX != null)
    //    {
    //        FallOffSpray = (GameObject)Instantiate(FallOffSprayVFX, m_FirePoint[1].transform.position, transform.GetComponentInParent<Player>().transform.rotation);
    //        FallOffSpray.transform.parent = m_FirePoint[1].transform.parent.transform.parent;
    //        //FallOffSpray.transform.Rotate(new Vector3(-90, 0, 0));
    //        FallOffSpray.transform.localScale = new Vector3(1, 1, 1);
    //        FallOffSpray.transform.localPosition = m_FirePoint[1].transform.localPosition;
    //        Destroy(FallOffSpray, m_Weapon1Cooldown - 1f);
    //    }
    //}

    //private IEnumerator FallOffTimer()
    //{
    //    bool ShootSprayFallOffVFXBool = false;
    //    yield return new WaitForSeconds(m_SprayTimer - 1.5f);
    //    if (FallOffSprayVFX != null)
    //    {
    //        if (!ShootSprayFallOffVFXBool)
    //        {
    //            FallOffSpray = (GameObject)Instantiate(FallOffSprayVFX, m_FirePoint[1].transform.position, transform.rotation);
    //            FallOffSpray.transform.parent = m_FirePoint[1].transform.parent.transform.parent;
    //            FallOffSpray.transform.Rotate(new Vector3(-90, 0, 0));
    //            FallOffSpray.transform.localScale = new Vector3(1, 1, 1);
    //            FallOffSpray.transform.localPosition = m_FirePoint[1].transform.localPosition;
    //            ShootSprayFallOffVFXBool = true;
    //            Destroy(FallOffSpray, (m_FallOffTimer + 2));
    //        }
    //    }
    //}

    private void assignDamage(GameObject bullet)
    {
        if (bullet.GetComponent<Damage>() != null)
        {
            bullet.GetComponent<Damage>().m_Damage = m_Damage;
        }
        else
        {
            Debug.Log("Bullet doesn't have a Damage Component");
        }
    }

    void OnDestroy()
    {
        if (ShootSprayGO != null)
        {
            Destroy(ShootSprayGO);
        }

        if (FallOffSpray != null)
        {
            Destroy(FallOffSpray);
        }
        FizzyCone.SetActive(false);
    }
}