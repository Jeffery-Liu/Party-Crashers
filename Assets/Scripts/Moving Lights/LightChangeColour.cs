using UnityEngine;
using System.Collections;

public class LightChangeColour : MonoBehaviour
{

    public Light lt;
    public Color ColorOne;
    public Color ColorTwo;
    [Header("For Swap Speed, 1 is average, 0.5 is half, 2 is double etc.")]
    public float Swapspeed;   
    private Color CurrentColor;
    private Color PreviousColor;
    public int CurrentColorNumber;
    


    // Use this for initialization
    void Start()
    {
        CurrentColor = ColorOne;
        PreviousColor = ColorTwo;
        CurrentColorNumber = 1;
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lt.color = Color.Lerp(CurrentColor, PreviousColor, Mathf.PingPong(Time.time*Swapspeed, 1));

        //For future reference, to make it only go one direction you will need to raise the variable ie.
        // lt.color = Color.Lerp(CurrentColor, PreviousColor,  "0.1 value raised by x until 1");

    }

}

