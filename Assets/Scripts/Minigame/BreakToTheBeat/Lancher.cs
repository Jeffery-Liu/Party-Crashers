using UnityEngine;
using System.Collections;

public class Lancher : MonoBehaviour
{
    public float m_Period1FireIntervalMin = 1;
    public float m_Period1FireIntervalMax = 20;
    public float m_Period1BlueSpeed = 1;
    public float m_Period1GoldSpeed = 1;
    public float m_Period1BlackSpeed = 1;
    public float m_Period1EndTime = 15f;
    public float m_Period2FireIntervalMin = 5;
    public float m_Period2FireIntervalMax = 10;
    public float m_Period2BlueSpeed = 5;
    public float m_Period2GoldSpeed = 5;
    public float m_Period2BlackSpeed = 5;
    public float m_Period2EndTime = 30f;
    public float m_Period3FireIntervalMin = 3;
    public float m_Period3FireIntervalMax = 8;
    public float m_Period3BlueSpeed = 10;
    public float m_Period3GoldSpeed = 10;
    public float m_Period3BlackSpeed = 10;
    public GameObject m_VasePrefeb;
    public GameObject m_VasePrefeb1;
    public GameObject m_VasePrefeb2;
    public Transform m_ShotPos;
    private float m_LastShotTime;
    private int m_ramdom;
    private int m_BluePercentage;
    public int m_GoldPercentage = 20;
    public int m_BlackPercentage = 20;
    private float m_Timer;

    private float m_FireIntervalMin;
    private float m_FireIntervalMax;
    private bool m_Activated;
    private int m_LauncherRandom;
    private int m_LauncherPer;
    //public int LauncherPercentage = 60;
    // Use this for initialization
    //SFX AND VFX
    public GameObject cannonfireFX;
    public AudioSource audiosource;
    public AudioClip CannonSFX;
    //SFX AND VFX
    void Start()
    {
        m_LastShotTime = Time.time;
        m_Timer = 0f;
        m_Activated = false;
        m_FireIntervalMin = m_Period1FireIntervalMin;
        m_FireIntervalMax = m_Period1FireIntervalMax;
        m_BluePercentage = 100 - m_GoldPercentage - m_BlackPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_Period1EndTime && m_Timer < m_Period2EndTime)
        {
            m_FireIntervalMin = m_Period2FireIntervalMin;
            m_FireIntervalMax = m_Period2FireIntervalMax;
        }
        if (m_Timer >= m_Period2EndTime/* && m_Timer < 30*/)
        {
            m_FireIntervalMin = m_Period3FireIntervalMin;
            m_FireIntervalMax = m_Period3FireIntervalMax;
        }
        //if (m_Timer >= 30)
        //{
        //    // game end
        //}
        bool canShoot = (m_LastShotTime + Random.Range(m_FireIntervalMin, m_FireIntervalMax)) < Time.time;

        // Launcher system
        // Still need to test to get best values for m_FireIntervalMin and m_FireIntervalMax
        // method 1: Launcher or not
        m_LauncherRandom = Random.Range(0, 2);
        if (m_LauncherRandom == 0)
        {
            m_Activated = true;
        }
        else
        {
            m_Activated = false;
        }
        // method 2: percentage to decide launcher or not
        //m_LauncherPer = Random.Range(0, 101);
        //if (m_LauncherPer >= 0 && m_LauncherPer <= LanuncherPercentage)
        //{
        //    m_Activated = true;
        //}
        //else if (m_LauncherPer >= LanuncherPercentage)
        //{
        //    m_Activated = false;
        //}

        // Shoot!
        if (canShoot /*&& m_Activated*/)
        {
            m_LastShotTime = Time.time;
            if (m_Activated)
            {
                if (m_VasePrefeb != null && m_VasePrefeb1 != null && m_VasePrefeb2 != null)
                {
                    if (m_ShotPos != null)
                    {
                        m_ramdom = Random.Range(0, 100);
                        if (m_ramdom >= 0 && m_ramdom < m_BluePercentage)
                        {
                            // Blue Vase
                            GameObject shot = Instantiate(m_VasePrefeb, m_ShotPos.position, m_ShotPos.rotation) as GameObject;
                            if (m_Timer < m_Period1EndTime)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period1BlueSpeed;
                            }
                            if (m_Timer >= m_Period1EndTime && m_Timer < m_Period2EndTime)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period2BlueSpeed;
                            }
                            if (m_Timer >= m_Period2EndTime /*&& m_Timer < 30*/)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period3BlueSpeed;
                            }
                            //if (m_Timer >= 30)
                            //{
                            //    // game end
                            //}
                        }
                        if (m_ramdom >= m_BluePercentage && m_ramdom < (m_BluePercentage + m_GoldPercentage))
                        {
                            // Gold Vase
                            GameObject shot = Instantiate(m_VasePrefeb1, m_ShotPos.position, m_ShotPos.rotation) as GameObject;
                            if (m_Timer < m_Period1EndTime)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period1GoldSpeed;
                            }
                            if (m_Timer >= m_Period1EndTime && m_Timer < m_Period2EndTime)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period2GoldSpeed;
                            }
                            if (m_Timer >= m_Period2EndTime /*&& m_Timer < 30*/)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period3GoldSpeed;
                            }
                            //if (m_Timer >= 30)
                            //{
                            //    // game end
                            //}
                        }
                        if (m_ramdom >= (m_BluePercentage + m_GoldPercentage) && m_ramdom <= 100)
                        {
                            // Black Vase
                            GameObject shot = Instantiate(m_VasePrefeb2, m_ShotPos.position, m_ShotPos.rotation) as GameObject;
                            if (m_Timer < m_Period1EndTime)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period1BlackSpeed;
                            }
                            if (m_Timer >= m_Period1EndTime && m_Timer < m_Period2EndTime)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period2BlackSpeed;
                            }
                            if (m_Timer >= m_Period2EndTime /*&& m_Timer < 30*/)
                            {
                                shot.GetComponent<VaseSpeed>().velocity = m_Period3BlackSpeed;
                            }
                            //if (m_Timer >= 30)
                            //{
                            //    // game end
                            //}
                        }
                    }
                }
                //else
                //{
                //    GameObject shot = GameObject.Instantiate<GameObject>(m_VasePrefeb);
                //    shot.transform.position = transform.position;
                //    shot.transform.forward = transform.forward;
                //    shot.transform.up = transform.up;
                //}

                //SFX AND VFX
                if (audiosource != null &&  CannonSFX != null)
                {                
                    audiosource.clip = CannonSFX;
                    audiosource.Play();
                }
                if(cannonfireFX != null)
                {
                    GameObject firevfx;
                    firevfx = (GameObject)Instantiate(cannonfireFX, m_ShotPos.position, m_ShotPos.rotation);
                    Destroy(firevfx, 0.5f);
                }
                
                //SFX AND VFX
            }
        }
    }
}

