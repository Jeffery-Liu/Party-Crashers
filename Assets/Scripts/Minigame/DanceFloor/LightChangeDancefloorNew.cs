using UnityEngine;
using System.Collections;

public class NEWLightChangeDancefloor : MonoBehaviour {

    // Green
	public Color ColorGreen;
    // Red
	public Color ColorRed;

	[SerializeField]
	private float scoretime = 4;
	[SerializeField]
	private float stoptime = 4;

	private Color CurrentColor;
	public int CurrentColorInt;
	private Color PreviousColor;
    private Color TempColor;
	public int PreviousColorInt = -1;
	//public int CurrentColorNumber;

	public bool stop = false;


	// Use this for initialization
	void Start()
	{		
		//CurrentColorNumber = 1;
        CurrentColorInt = -1;
	}

	// Update is called once per frame
	void Update()
	{
		if (stop == true) 
		{
			StartCoroutine (Stopedfor (scoretime));
            if (PreviousColorInt != -1)
            {
                //lt.color = TempColor;
            }
        }
		if (stop == false) 
		{
			StartCoroutine(Stopfor(scoretime));
			StartCoroutine(ColorRandomiser(stoptime));
            //lt.color = Color.Lerp (CurrentColor, PreviousColor, Mathf.PingPong (Time.time*1.5f, 1));
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
		CurrentColorInt = Random.Range (0,8);

			if (CurrentColorInt == 0)
			{
				CurrentColor = ColorGreen;
			}		
			
			if (CurrentColorInt == 1) 
			{
				CurrentColor = ColorRed;
			}
	}
}
