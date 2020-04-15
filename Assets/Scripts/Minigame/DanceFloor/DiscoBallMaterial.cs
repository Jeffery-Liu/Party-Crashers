using UnityEngine;
using System.Collections;

public class DiscoBallMaterial : MonoBehaviour
{
    public Renderer m_DiscoBallLight;
    public Material m_ColorZero;
    public Material m_ColorOne;
    public Material m_ColorTwo;
    public Material m_ColorThree;
    public Material m_ColorFour;
    public Material m_ColorFive;
    public Material m_ColorSix;

    private float scoretime = 4;
    private float stoptime = 4;

    private bool stop = false;

    public Material m_DiscoBallON;
    private Material m_Temp;
    public GameObject m_DiscoBall;
    public int m_CurrentBallColorInt;
    public int m_PreviousColorInt = -1;


    // Use this for initialization
    void Start()
    {
        m_CurrentBallColorInt = -1;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentBallColorInt = m_DiscoBall.GetComponent<LightChangeDiscoball>().CurrentColorInt;
        if (stop == true)
        {
            StartCoroutine(Stopedfor(scoretime));
            if (m_PreviousColorInt != -1)
            {
                m_DiscoBallLight.material = m_Temp;
            }
        }
        if (stop == false)
        {
            StartCoroutine(Stopfor(scoretime));
            StartCoroutine(ColorRandomiser(stoptime));

            m_DiscoBallLight.material = m_DiscoBallON;
            float emission = Mathf.PingPong(Time.time * 1.5f, 1);
            m_DiscoBallLight.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * emission);
            m_PreviousColorInt = m_CurrentBallColorInt;
            m_Temp = m_DiscoBallON;
        }
    }

    IEnumerator ColorRandomiser(float wait)
    {
        
        if (m_CurrentBallColorInt == 0)
        {
            m_DiscoBallON = m_ColorZero;
        }
        if (m_CurrentBallColorInt == 1)
        {
            m_DiscoBallON = m_ColorOne;
        }
        if (m_CurrentBallColorInt == 2)
        {
            m_DiscoBallON = m_ColorTwo;
        }
        if (m_CurrentBallColorInt == 3)
        {
            m_DiscoBallON = m_ColorThree;
        }
        if (m_CurrentBallColorInt == 4)
        {
            m_DiscoBallON = m_ColorFour;
        }
        if (m_CurrentBallColorInt == 5)
        {
            m_DiscoBallON = m_ColorFive;
        }
        if (m_CurrentBallColorInt == 6)
        {
            m_DiscoBallON = m_ColorSix;
        }
        yield return new WaitForSeconds(wait);
       
    }
    IEnumerator Stopfor(float wait)
    {
        //this is the amount of time i want it to wait
        yield return new WaitForSeconds(wait);
        //this is what it will do when the timehas passed

        stop = true;
    }

    IEnumerator Stopedfor(float wait)
    {
        //this is the amount of time i want it to wait
        yield return new WaitForSeconds(wait);
        //this is what it will do when the timehas passed
        stop = false;

    }
}
