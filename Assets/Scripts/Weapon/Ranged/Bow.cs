using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Bow : Ranged
{
    [Header("WaterBalloon Bow")]
    #region Ints
    [SerializeField]
    private int m_MaxBullets;
    private int m_bulletsLeft;
    #endregion
    #region Floats
    [SerializeField]
    private float m_BulletSpeed;
    [SerializeField]
    private float BulletRegenTimer = 1.0f;
    [SerializeField]
    private float m_BombSpeed;
    private float timer;
    #endregion
    #region Bools
    private bool m_CanFirePrimary = false;
    private bool m_CanFireSecondary = false;
    private bool m_InitBullets = true;
    #endregion
    #region Components
    private Player Player;
    [SerializeField]
    private GameObject m_FullChargeVFX;
    private GameObject FullChargeVFX;
    #endregion
    
    void start()
    {
        Player = GetComponent<Player>();
    }

    private void Update()
    {       
        #region Primary Attack
        Bullets();
        if (m_CanFirePrimary)
            ShootPrimary();                    
        #endregion

        #region Secondary Attack
        if (m_CanFireSecondary)
            ShootSecondary();
        #endregion
    }

    private void Bullets()
    {
        if(m_InitBullets)
        {
            m_InitBullets = false;
            m_bulletsLeft = m_MaxBullets;
        }

        timer += Time.deltaTime;
        if (m_bulletsLeft < m_MaxBullets)
        {
            if (timer >= BulletRegenTimer)
            {
                timer = 0.0f;
                m_bulletsLeft++;
            }
        }

        #region VFX
        if (m_bulletsLeft == m_MaxBullets)
        {
            if (!FullChargeVFX)
            {
                FullChargeVFX = Instantiate(m_FullChargeVFX, transform.position, transform.rotation) as GameObject;
                FullChargeVFX.transform.parent = gameObject.transform;
                FullChargeVFX.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
            Destroy(FullChargeVFX);
        #endregion
    }

    public override void primaryAttack()
    {
        if (m_CoolDown <= Time.time - m_Weapon1Cooldown || m_CoolDown == 0)
        {
            if (m_bulletsLeft != 0)
            {
                m_CanFirePrimary = true;
                m_bulletsLeft--;
            }
         
            m_CoolDown = Time.time;
        }
    }

    public override void secondaryAttack()
    {
        if(m_SecondaryCoolDown <= Time.time - m_Weapon2Cooldown || m_SecondaryCoolDown == 0)
        {
            m_CanFireSecondary = true;
            m_SecondaryCoolDown = Time.time;
        }
    }

    public override void terminate()
    {
    }

    private void ShootPrimary()
    {
        GameObject bullet;
        bullet = (GameObject)Instantiate(m_RightTriggerProjectile, m_FirePoint[0].gameObject.transform.position, m_FirePoint[0].gameObject.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * m_BulletSpeed);
        AssignDamage(bullet, 1);
        m_CanFirePrimary = false;
    }

    private void ShootSecondary()
    {
        GameObject bomb;
        bomb = (GameObject)Instantiate(m_LeftTriggerProjectile, m_FirePoint[0].gameObject.transform.position, m_FirePoint[0].gameObject.transform.rotation);                
        bomb.GetComponent<Rigidbody>().AddForce(bomb.transform.forward * m_BombSpeed);                
        m_CanFireSecondary = false;        
    }

    private void AssignDamage(GameObject bullet, int multiplier)
    {
        if (bullet.GetComponent<Damage>() != null)
            bullet.GetComponent<Damage>().m_Damage = m_Damage * multiplier;
        else
            Debug.Log("Bullet doesn't have a Damage Component.");
    }

    void OnDestroy()
    {
        if(FullChargeVFX != null)
        {
            Destroy(FullChargeVFX);
        }
    }
}    // End