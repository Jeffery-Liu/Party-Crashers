using UnityEngine;
using System.Collections;

public class DanceFloor : MonoBehaviour {

    public GameObject m_Light;
    //public GameObject m_DiscoBall;
    public int m_CurrentFloorColor;
    //public int m_CurrentBallColor;
    public int m_PreviousFloorColor;
    //public int m_PreviousBallColor;
    private int m_GreenColorPercentage;

    //public GameObject m_GetPointEffect;
    //public GameObject m_LosepointEffect;

    private bool m_GetPoint = true;
    private bool m_LosePoint = true;

    private bool m_GetPointFX = false;
    private bool m_LosePointFX = false;

    private MinigameScoreAndTimeTrack   m_MinigameScoreAndTimeTrack;
    private MinigameManager             m_MinigameManager;

    public GameObject m_GetPointEffect;
    public GameObject m_LosepointEffect;
    public AudioClip m_PositiveSound;
    public AudioClip m_NegativeSound;
    public AudioSource m_audiosource;

    private LightChangeDancefloor m_LightChangeDancefloor;
    //private LightChangeDiscoball m_LightChangeDiscoball;

    private GameObject GettingPoint = null;
    private GameObject LosingPoint = null;

    //GameObject m_Discoball2;

    // Use this for initialization
    void Start () {
        m_LightChangeDancefloor = m_Light.GetComponent<LightChangeDancefloor>();
        //m_LightChangeDiscoball = m_DiscoBall.GetComponent<LightChangeDiscoball>();

        //m_GetPointEffect = m_LightChangeDiscoball.m_GetPointEffect;
        //m_LosepointEffect = m_LightChangeDiscoball.m_LosepointEffect;
        m_MinigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
        m_MinigameScoreAndTimeTrack = m_MinigameManager.GetComponent<MinigameScoreAndTimeTrack>();

        for (int i = 0; i < GameManager.m_Instance.m_NumOfPlayers; ++i)
        {
            if(i < m_MinigameScoreAndTimeTrack.m_RawTime.Length)
                m_MinigameScoreAndTimeTrack.m_RawTime[i] = 0.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        m_CurrentFloorColor = m_LightChangeDancefloor.CurrentColorInt;
        //m_CurrentBallColor = m_LightChangeDiscoball.CurrentColorInt;
        m_PreviousFloorColor = m_LightChangeDancefloor.PreviousColorInt;
        //m_PreviousBallColor = m_LightChangeDiscoball.PreviousColorInt;
        m_GreenColorPercentage = m_LightChangeDancefloor.GreenColorPercentage;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<HeartSystem>() != null)
        {
            if (m_Light.GetComponent<LightChangeDancefloor>().stop == false)
            {
                    //Debug.Log("No point.");
                if (GettingPoint != null)
                {
                    Destroy(GettingPoint);
                }
                if (LosingPoint != null)
                {
                    Destroy(LosingPoint);
                }
            }
            if (m_Light.GetComponent<LightChangeDancefloor>().stop == true)
            {

                //if (m_PreviousFloorColor == 0)
                if(m_PreviousFloorColor >= 0 && m_PreviousFloorColor < m_GreenColorPercentage)
                {
                    // Getting Score.
                    //Debug.Log("Getting Score!");

                    //Get points!!
                    m_MinigameScoreAndTimeTrack.m_RawTime[(int)other.GetComponent<Player>().m_Player - 1] += Time.deltaTime;

                    if (!m_MinigameScoreAndTimeTrack.m_IsCoroutineRunning)
                    {
                        StartCoroutine(m_MinigameScoreAndTimeTrack.UpdateScore());
                    }

                    m_GetPoint = false;

                    if(m_GetPointFX == false)
                    {
                        m_GetPointFX = true;
                        GettingPoint = (GameObject)Instantiate(m_GetPointEffect, gameObject.transform.position, gameObject.transform.rotation);
                        if(m_PositiveSound != null && m_audiosource !=null)
                        {
                            m_audiosource.clip = m_PositiveSound;
                            m_audiosource.Play();
                        }
                    }
                        m_LosePointFX = false;
                    if (LosingPoint != null)
                    {
                        Destroy(LosingPoint);
                    }
                    //Destroy(GettingPoint, 5f);
                    StartCoroutine(WaitForSec(1f));
                }
                //else if(m_PreviousFloorColor == 1)
                else if (m_PreviousFloorColor >= m_GreenColorPercentage && m_PreviousFloorColor < 100)
                {
                    // Lose Score.
                    //Debug.Log("LOSE point!");
                    m_LosePoint = false;
                    m_GetPointFX = false;
                    if (m_LosePointFX == false)
                    {
                        m_LosePointFX = true;
                        LosingPoint = (GameObject)Instantiate(m_LosepointEffect, gameObject.transform.position, gameObject.transform.rotation);
                        m_audiosource.clip = m_NegativeSound;
                        m_audiosource.Play();
                        if (m_NegativeSound != null && m_audiosource != null)
                        {
                           
                        }

                    }
                    if (GettingPoint != null)
                    {
                        Destroy(GettingPoint);
                    }
                    StartCoroutine(WaitForSec2(1f));
                }
                else
                {
                    m_GetPointFX = false;
                    m_LosePointFX = false;
                    if (GettingPoint != null)
                    {
                        Destroy(GettingPoint);
                    }
                    if (LosingPoint != null)
                    {
                        Destroy(LosingPoint);
                    }
                    //Debug.Log("No point.");
                }
            }
        }
    }

    
    public void OnTriggerExit(Collider other)
    {
        m_GetPointFX = false;
        m_LosePointFX = false;
        if (GettingPoint != null)
        {
            Destroy(GettingPoint);
        }
        if (LosingPoint != null)
        {
            Destroy(LosingPoint);
        }
    }

    IEnumerator WaitForSec(float s)
    {
        
        
        yield return new WaitForSeconds(s);
       
        m_GetPoint = true;
    }

    IEnumerator WaitForSec2(float s)
    {
        yield return new WaitForSeconds(s);
        m_LosePoint = true;
    }
}

// How to change Tiles' color:
// Reference: https://www.youtube.com/watch?v=kN7Rx3uPBuU
