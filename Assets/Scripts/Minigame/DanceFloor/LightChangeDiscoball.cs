using UnityEngine;
using System.Collections;
//This Script is RIGHT.
public class LightChangeDiscoball : MonoBehaviour {
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
    //[SerializeField]
    //private Color ColorSeven;

    [SerializeField]
    private float scoretime = 4;
    [SerializeField]
    private float stoptime = 4;

    private Color CurrentColor;
    public int CurrentColorInt;
    private Color PreviousColor;
    private Color TempColor;
    public int PreviousColorInt = -1;

    private bool stop = false;

    public GameObject m_GetPointEffect;
    public GameObject m_LosepointEffect;

    // Use this for initialization
    void Start()
    {
        lt = GetComponent<Light>();
        CurrentColorInt = -2;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop == true)
        {
            StartCoroutine(Stopedfor(scoretime));
            if(PreviousColorInt != -1)
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
            //lt.color = Color.Lerp(CurrentColor, PreviousColor, 1);
        }
        //Debug.Log(stop);
        //For future reference, to make it only go one direction you will need to raise the variable ie.
        // lt.color = Color.Lerp(CurrentColor, PreviousColor,  "0.1 value raised by x until 1");
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
        yield return new WaitForSeconds(wait);
        CurrentColorInt = Random.Range(0, 6);
        //PreviousColorInt = Random.Range(0, 8);
        //if (CurrentColorInt == PreviousColorInt)
        //{
        //    CurrentColorInt = Random.Range(0, 8);
        //    PreviousColorInt = Random.Range(0, 8);
        //}
        //else//im so sorry
        //{
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
            //if (CurrentColorInt == 7)
            //{
            //    CurrentColor = ColorSeven;
            //}

            //if (PreviousColorInt == 0)
            //{
            //    CurrentColor = ColorZero;
            //}
            //if (PreviousColorInt == 1)
            //{
            //    CurrentColor = ColorOne;
            //}
            //if (PreviousColorInt == 2)
            //{
            //    CurrentColor = ColorTwo;
            //}
            //if (PreviousColorInt == 3)
            //{
            //    CurrentColor = ColorThree;
            //}
            //if (PreviousColorInt == 4)
            //{
            //    CurrentColor = ColorFour;
            //}
            //if (PreviousColorInt == 5)
            //{
            //    CurrentColor = ColorFive;
            //}
            //if (PreviousColorInt == 6)
            //{
            //    CurrentColor = ColorSix;
            //}
            //if (PreviousColorInt == 7)
            //{
            //    CurrentColor = ColorSeven;
            //}
        //}

    }
}
