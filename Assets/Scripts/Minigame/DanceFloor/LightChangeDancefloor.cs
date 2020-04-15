using UnityEngine;
using System.Collections;

public class LightChangeDancefloor : MonoBehaviour {

	[SerializeField]
	private Light lt;
	
	[SerializeField]
	private Color ColorRight;
	[SerializeField]
	private Color ColorWrong;
    
	public float scoretime = 4;
	public float stoptime = 4;

	private Color CurrentColor;
	public int CurrentColorInt;
	private Color PreviousColor;
    private Color TempColor;
	public int PreviousColorInt = -1;
	//public int CurrentColorNumber;

	public bool stop = false;

    public int GreenColorPercentage = 20;

    public float gameDelayTime = 4f;
    public bool updateOn = false;

	// Use this for initialization
	void Start()
	{
        updateOn = false;
        StartCoroutine(updateTrigger(gameDelayTime));
        //CurrentColorNumber = 1;
        lt = GetComponent<Light>();
        CurrentColorInt = -1;
	}

	// Update is called once per frame
	void Update()
	{
        if (updateOn)
        {
		    if (stop == true) 
		    {
			    StartCoroutine (Stopedfor (scoretime));
                if (PreviousColorInt != -1)
                {
                    lt.color = TempColor;
                }
            }
		    if (stop == false) 
		    {
			    StartCoroutine(Stopfor(scoretime));
			    StartCoroutine(ColorRandomiser(stoptime));
                lt.color = Color.Lerp (CurrentColor, PreviousColor, Mathf.PingPong (Time.time*1.5f, 1));
                PreviousColorInt = CurrentColorInt;
                TempColor = CurrentColor;
                //lt.color = Color.Lerp(CurrentColor, PreviousColor, 1);
            }
            //Debug.Log(stop);
		    //For future reference, to make it only go one direction you will need to raise the variable ie.
		    // lt.color = Color.Lerp(CurrentColor, PreviousColor,  "0.1 value raised by x until 1");

        }
	}
    
	IEnumerator Stopfor(float wait) 
	{
		//this is the amount of time i want it to wait
		yield return new WaitForSeconds(wait);
		//this is what it will do when the timehas passed

		stop=true;
	} 

	IEnumerator Stopedfor(float wait) 
	{
		//this is the amount of time i want it to wait
		yield return new WaitForSeconds(wait);
		//this is what it will do when the timehas passed
		stop=false;

	} 

	IEnumerator ColorRandomiser(float wait)
	{
		yield return new WaitForSeconds(wait);
		CurrentColorInt = Random.Range (0,100);
		
			if (CurrentColorInt >= 0 && CurrentColorInt < GreenColorPercentage) 
			{
				CurrentColor = ColorRight;
			}
			if (CurrentColorInt >= GreenColorPercentage && CurrentColorInt < 100) 
			{
				CurrentColor = ColorWrong;
			}

	}

    IEnumerator updateTrigger(float t)
    {
        yield return new WaitForSeconds(t);
        updateOn = true;
    }
}
