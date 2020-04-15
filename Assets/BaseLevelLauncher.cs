using UnityEngine;
using System.Collections;

public class BaseLevelLauncher : MonoBehaviour
{
    public GameObject m_Projectile;
    public Transform m_ShotPos;
    public float m_ProjectileSpeed = 5;
    public float m_FireInterval = 5;
    private float m_Timer;
    private float m_LastShotTime;
    // Use this for initialization
    void Start()
    {
        m_LastShotTime = Time.time;
        m_Timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        bool canShoot = (m_LastShotTime + m_FireInterval) < Time.time;
        if (canShoot)
        {
            if (m_Projectile != null)
            {
                if (m_ShotPos != null)
                {
                    GameObject shot = Instantiate(m_Projectile, m_ShotPos.position, m_ShotPos.rotation) as GameObject;
                    shot.GetComponent<VaseSpeed>().velocity = m_ProjectileSpeed;
                    m_LastShotTime = Time.time;
                }
                else
                {
                    Debug.Log("Shot Position is null");
                }
            }
            else
            {
                Debug.Log("Projectile is null");
            }
        }
    }
}
