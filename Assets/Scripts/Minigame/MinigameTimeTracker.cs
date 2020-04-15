using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinigameTimeTracker : MonoBehaviour
{
    MinigameManager minigameManger;

    public Canvas miniGameCanvas;
    public CanvasGroup firstFadingCanvas;
    public CanvasGroup secondFadingCanvas;

    CanvasGroup rewardFadeInCanvas;
    [Header("Starting Counter")]
    public GameObject[] startCounter;

    float minigameTimerRaw;     //floats gets rounded to "minigameTimer" int in Update
    public int minigameTimer;

    float fadeTime = 2f;

    //Screen Fading floats
    float delayToFadeIn = 1f;
    float delayToShowResultBar = 2f;
    float delayToShowRewards = 2f;
    void Awake()
    {
        minigameManger = GetComponent<MinigameManager>();
        miniGameCanvas = GameObject.Find("MinigameCanvas").GetComponent<Canvas>();
        firstFadingCanvas = GameObject.Find("First Fading Canvas").GetComponent<CanvasGroup>();
        secondFadingCanvas = GameObject.Find("Second Fading Canvas").GetComponent<CanvasGroup>();
        startCounter[0] = GameObject.Find("3"); startCounter[1] = GameObject.Find("2"); startCounter[2] = GameObject.Find("1"); startCounter[3] = GameObject.Find("GO!");
    }
    void Update()
    {
        //minigameTimer = (int)Mathf.Round(minigameTimerRaw);

        //if (!minigameManger.minigameEnded)
        //{
        //    minigameTimerRaw += Time.deltaTime;
        //    MinigameStart();
        //}
        //else
        //    MinigameEnd();

        ////Minigame ends in 30s from start (6s is from 3, 2, 1 Countdown in MinigameStart())
        //if (minigameTimerRaw >= 36f)
        //    minigameManger.minigameEnded = true;

        ////TESTING
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    minigameManger.minigameEnded = true;
        //}
        ///////////
        //if (minigameManger.barsRaised)
        //    ScreenFading();
    }

    //void MinigameStart()
    //{
    //    //Disable Players' movement
    //    foreach (GameObject player in GameManager.m_Instance.m_Players)
    //    {
    //        player.GetComponent<PlayerController>().m_CantMove = true;
    //        player.GetComponent<Player>().m_CantAttack = true;
    //    }

    //    //Disable partybar for countdown
    //    GameManager.m_Instance.m_PartyBar.m_Active = false;

    //    //3, 2, 1, GO Countdown
    //    if (minigameTimer == 2)
    //    {
    //        startCounter[0].GetComponent<Text>().enabled = true;

    //        startCounter[0].transform.GetChild(0).GetComponent<Text>().enabled = true;
    //    }
    //    else if (minigameTimer == 3)
    //    {
    //        startCounter[0].GetComponent<Text>().enabled = false;
    //        startCounter[1].GetComponent<Text>().enabled = true;

    //        startCounter[0].transform.GetChild(0).GetComponent<Text>().enabled = false;
    //        startCounter[1].transform.GetChild(0).GetComponent<Text>().enabled = true;
    //    }

    //    else if (minigameTimer == 4)
    //    {
    //        startCounter[1].GetComponent<Text>().enabled = false;
    //        startCounter[2].GetComponent<Text>().enabled = true;

    //        startCounter[1].transform.GetChild(0).GetComponent<Text>().enabled = false;
    //        startCounter[2].transform.GetChild(0).GetComponent<Text>().enabled = true;
    //    }
    //    else if (minigameTimer == 5)
    //    {
    //        startCounter[2].GetComponent<Text>().enabled = false;
    //        startCounter[3].GetComponent<Text>().enabled = true;

    //        startCounter[2].transform.GetChild(0).GetComponent<Text>().enabled = false;
    //        startCounter[3].transform.GetChild(0).GetComponent<Text>().enabled = true;

    //    }

    //    else if (minigameTimer >= 6)
    //    {
    //        startCounter[3].GetComponent<Text>().enabled = false;
    //        startCounter[3].transform.GetChild(0).GetComponent<Text>().enabled = false;
    //        //Enable Players' movement
    //        foreach (GameObject player in GameManager.m_Instance.m_Players)
    //        {
    //            player.GetComponent<PlayerController>().m_CantMove = false;
    //            player.GetComponent<Player>().m_CantAttack = false;
    //        }

    //        GameManager.m_Instance.m_PartyBar.m_Active = true;
    //    }
    //}

    public void MinigameEnd()
    {
        foreach (GameObject player in GameManager.m_Instance.m_Players)
            player.GetComponent<PlayerController>().m_CantMove = true;

        //Black Background Fading Canvas
        delayToFadeIn -= Time.deltaTime;
        if (delayToFadeIn < 0)
        {
            if (firstFadingCanvas.alpha < 0.6)
            {
                firstFadingCanvas.alpha += Time.deltaTime / fadeTime;
            }

            delayToShowResultBar -= Time.deltaTime;
            if (delayToShowResultBar < 0)
            {
                //minigameManger.showResultBar = true; //PASSES TO NEIGHBOUR SCRIPT


                //Work-around of canvases hirarchy order
                secondFadingCanvas.gameObject.transform.SetParent(miniGameCanvas.transform);
                secondFadingCanvas.gameObject.transform.SetParent(null);
            }
        }


    }

    void ScreenFading()
    {
        if (secondFadingCanvas.alpha < 0.6)
        {
            secondFadingCanvas.alpha += Time.deltaTime / fadeTime;
        }
        else
        {
            delayToShowRewards -= Time.deltaTime;
            if (delayToShowRewards < 0)
            {
                //minigameManger.showRewardCanvas = true;
            }
        }
    }
}
