using UnityEngine;
using System.Collections;

public class DuplicateDiscoLight : MonoBehaviour {
    [SerializeField]
    private Light lt;
    [SerializeField]
    private Color ColorZero;
    [SerializeField]
    private Color ColorOne;
    [SerializeField]
    private Color ColorTwo;
    [SerializeField]
    private Color ColorThree;
    [SerializeField]
    private Color ColorFour;
    [SerializeField]
    private Color ColorFive;
    [SerializeField]
    private Color ColorSix;
    

    [SerializeField]
    private float scoretime = 4;
    [SerializeField]
    private float stoptime = 4;

    public GameObject m_Light;

    private Color CurrentColor;
    public int CurrentColorInt;
    private Color PreviousColor;
    private Color TempColor;
    public int PreviousColorInt = -1;

    private bool stop = false;


    // Use this for initialization
    void Start()
    {
        lt = GetComponent<Light>();
        CurrentColorInt = -2;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentColorInt = m_Light.GetComponent<LightChangeDiscoball>().CurrentColorInt;
        if (stop == true)
        {
            StartCoroutine(Stopedfor(scoretime));
            if (PreviousColorInt != -1)
            {
                lt.color = TempColor;
            }
        }
        if (stop == false)
        {
            StartCoroutine(Stopfor(scoretime));
            StartCoroutine(ColorRandomiser(stoptime));
            lt.color = Color.Lerp(CurrentColor, PreviousColor, Mathf.PingPong(Time.time * 1.5f, 1));
            PreviousColorInt = CurrentColorInt;
            TempColor = CurrentColor;
        }
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

    IEnumerator ColorRandomiser(float wait)
    {
        if (CurrentColorInt == 0)
        {
            CurrentColor = ColorZero;
        }
        if (CurrentColorInt == 1)
        {
            CurrentColor = ColorOne;
        }
        if (CurrentColorInt == 2)
        {
            CurrentColor = ColorTwo;
        }
        if (CurrentColorInt == 3)
        {
            CurrentColor = ColorThree;
        }
        if (CurrentColorInt == 4)
        {
            CurrentColor = ColorFour;
        }
        if (CurrentColorInt == 5)
        {
            CurrentColor = ColorFive;
        }
        if (CurrentColorInt == 6)
        {
            CurrentColor = ColorSix;
        }
        
        yield return new WaitForSeconds(wait);
    }
}
