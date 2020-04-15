using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//James Shaw

public class Timer : MonoBehaviour
{
    private float score, p1, p2, p3, p4;
    public Text p1text, p2text, p3text, p4text;
    Text text;
    
    

    public float remainingSeconds;
    public Text timerText;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }   

   

    void Update()
    {
        if (remainingSeconds > 0)
        {
            remainingSeconds = remainingSeconds - Time.deltaTime;

            //Milliseconds
            //timerText.text = string.Format("Time:{0:00.00}", remainingSeconds);
            //Seconds
            timerText.text = string.Format("Time:{0:00}", remainingSeconds);





            // Score
            score = score + Time.deltaTime;

            if (GameObject.Find("P1"))
                p1text.text = string.Format("Player 1: {0:00}", p1 + score);

            if (GameObject.Find("P2"))
                p2text.text = string.Format("Player 2: {0:00}", p2 + score);

            if (GameObject.Find("P3"))
                p3text.text = string.Format("Player 3: {0:00}", p3 + score);

            if (GameObject.Find("P4"))
                p4text.text = string.Format("Player 4: {0:00}", p4 + score);

            if (!GameObject.Find("P1") && !GameObject.Find("P2") && !GameObject.Find("P3") && !GameObject.Find("P4"))
            {
                Debug.Log("Game Over!!!!!!!!!!");
                //Tranfer to end minigame screen
            }
            else if (remainingSeconds <= 0)
            {
                Debug.Log("Game Over!!!!!!!!!!");
                //Tranfer to end minigame screen                
            }
        }

    }
}
