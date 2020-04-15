using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class MinigameResultSummary : MonoBehaviour
{
    private MinigameManager m_MinigameManager;

    private Slider m_P1Bar, m_P2Bar, m_P3Bar, m_P4Bar;
    private float m_Speed = 2.0f;
    private int m_MaxScore;

    private bool m_IsScoreSorted;

    //Delays between Raising Result Bars
    //private float m_FirstDelay;
    //private float m_SecondDelay;
    //private float m_ThirdDelay;

    //List & Array for sorting players'score
    private List<int> m_AllScoresList = new List<int>();
    private int[] m_AllScores = new int[4];

    bool neonTurnedOn;
    public Image[] resultBarNeonTopImage = new Image[4];
    public Animator[] resultBarNeonTopAnimator = new Animator[4];

    // Methods

    private float ResultBarAmount(float score, float scoreMin, float scoreMax, float scoreMinvalue, float scoreMaxvalue)
    {
        return (score - scoreMin) * (scoreMaxvalue - scoreMinvalue) / (scoreMax - scoreMin) + scoreMinvalue;
    }

    void Awake()
    {
        m_MinigameManager = GetComponent<MinigameManager>();
        m_IsScoreSorted = false;

        m_P1Bar = GameObject.Find("P1_Panel/Slider").GetComponent<Slider>();
        m_P2Bar = GameObject.Find("P2_Panel/Slider").GetComponent<Slider>();
        m_P3Bar = GameObject.Find("P3_Panel/Slider").GetComponent<Slider>();
        m_P4Bar = GameObject.Find("P4_Panel/Slider").GetComponent<Slider>();

        resultBarNeonTopAnimator[0] = GameObject.Find("P1_Panel/Slider/Handle Slide Area/Handle").GetComponent<Animator>();
        resultBarNeonTopAnimator[1] = GameObject.Find("P2_Panel/Slider/Handle Slide Area/Handle").GetComponent<Animator>();
        resultBarNeonTopAnimator[2] = GameObject.Find("P3_Panel/Slider/Handle Slide Area/Handle").GetComponent<Animator>();
        resultBarNeonTopAnimator[3] = GameObject.Find("P4_Panel/Slider/Handle Slide Area/Handle").GetComponent<Animator>();

        //m_FirstDelay = 3.5f;
        //m_SecondDelay = 2.0f * m_FirstDelay;
        //m_ThirdDelay = 3.0f * m_FirstDelay;
    }

    void Update()
    {
        m_MaxScore = m_MinigameManager.m_ScorePlace1;

        if (m_MinigameManager.GetMinigameState() == MinigameManager.EMinigameState.ResultSummary)
        {
            foreach (GameObject player in GameManager.m_Instance.m_Players)
            {
                player.GetComponent<PlayerController>().m_CantMove = true;
            }

            ShowResultCanvas();
            SortingScores();
            SetPlayerPlace();
        }
    }

    void ShowResultCanvas()
    {
        //Black Background Fading Canvas
        m_MinigameManager.m_DelayToFadeIn -= Time.deltaTime;

        if (m_MinigameManager.m_DelayToFadeIn < 0)
        {
            if (m_MinigameManager.m_FirstFadingCanvas.alpha < 0.6)
            {
                m_MinigameManager.m_FirstFadingCanvas.alpha += Time.deltaTime / m_MinigameManager.m_FadeTime;
            }

            m_MinigameManager.m_DelayToShowResultBar -= Time.deltaTime;

            if (m_MinigameManager.m_DelayToShowResultBar < 0)
            {
                //Work-around of canvases hirarchy order
                //m_MinigameManager.m_SecondFadingCanvas.gameObject.transform.SetParent(m_MinigameManager.m_MinigameCanvas.transform);
                //m_MinigameManager.m_SecondFadingCanvas.gameObject.transform.SetParent(null);
            }
        }
    }

    void SortingScores()
    {
        if (!m_IsScoreSorted)
        {
            //Adds all players' scores to an array
            switch (GameManager.m_Instance.m_NumOfPlayers)
            {
                case 4:
                    m_AllScores[3] = GameManager.m_Instance.m_Players[3].GetComponent<Player>().m_Score;
                    goto case 3;
                case 3:
                    m_AllScores[2] = GameManager.m_Instance.m_Players[2].GetComponent<Player>().m_Score;
                    goto case 2;
                case 2:
                    m_AllScores[1] = GameManager.m_Instance.m_Players[1].GetComponent<Player>().m_Score;
                    goto case 1;
                case 1:
                    m_AllScores[0] = GameManager.m_Instance.m_Players[0].GetComponent<Player>().m_Score;
                    break;
            }
            //If statements instead of switch [needs to be a step-by-step removal from the list]
            if (GameManager.m_Instance.m_NumOfPlayers >= 1) //Place 1
            {
                m_MinigameManager.m_ScorePlace1 = m_AllScores.Max();               //Gives Place 1 the highest score from all players
                m_AllScoresList = m_AllScores.ToList();     //Converts Array to List
                m_AllScoresList.Remove(m_AllScores.Max());  //Deletes the highest score that was given to Place 1
                m_AllScores = m_AllScoresList.ToArray();    //Converts List back to Array
            }
            if (GameManager.m_Instance.m_NumOfPlayers >= 2) //Place 2
            {
                m_MinigameManager.m_ScorePlace2 = m_AllScores.Max(); m_AllScoresList = m_AllScores.ToList();
                m_AllScoresList.Remove(m_AllScores.Max()); m_AllScores = m_AllScoresList.ToArray();
            }
            if (GameManager.m_Instance.m_NumOfPlayers >= 3) //Place 3
            {
                m_MinigameManager.m_ScorePlace3 = m_AllScores.Max(); m_AllScoresList = m_AllScores.ToList();
                m_AllScoresList.Remove(m_AllScores.Max()); m_AllScores = m_AllScoresList.ToArray();
            }
            if (GameManager.m_Instance.m_NumOfPlayers >= 4) //Place 4
            {
                m_MinigameManager.m_ScorePlace4 = m_AllScores.Max();
                m_AllScoresList = m_AllScores.ToList(); m_AllScoresList.Remove(m_AllScores.Max()); m_AllScores = m_AllScoresList.ToArray();
                //Now both Array and List are empty but m_ScorePlace1, m_ScorePlace2, m_ScorePlace3, m_ScorePlace4 have sorted scores
            }
            m_IsScoreSorted = true;
        }

        if (m_MinigameManager.m_FirstFadingCanvas.alpha >= 0.6f)
            StartCoroutine(DelayBeforeRaisingResultBar());

        ScreenFading();
    }

    void SetPlayerPlace()
    {
        if (GameManager.m_Instance.m_Player1.score == m_MinigameManager.m_ScorePlace1) m_MinigameManager.m_P1Place = 1;
        if (GameManager.m_Instance.m_Player1.score == m_MinigameManager.m_ScorePlace2) m_MinigameManager.m_P1Place = 2;
        if (GameManager.m_Instance.m_Player1.score == m_MinigameManager.m_ScorePlace3) m_MinigameManager.m_P1Place = 3;
        if (GameManager.m_Instance.m_Player1.score == m_MinigameManager.m_ScorePlace4) m_MinigameManager.m_P1Place = 4;

        if (GameManager.m_Instance.m_Player2.score == m_MinigameManager.m_ScorePlace1) m_MinigameManager.m_P2Place = 1;
        if (GameManager.m_Instance.m_Player2.score == m_MinigameManager.m_ScorePlace2) m_MinigameManager.m_P2Place = 2;
        if (GameManager.m_Instance.m_Player2.score == m_MinigameManager.m_ScorePlace3) m_MinigameManager.m_P2Place = 3;
        if (GameManager.m_Instance.m_Player2.score == m_MinigameManager.m_ScorePlace4) m_MinigameManager.m_P2Place = 4;

        if (GameManager.m_Instance.m_Player3.score == m_MinigameManager.m_ScorePlace1) m_MinigameManager.m_P3Place = 1;
        if (GameManager.m_Instance.m_Player3.score == m_MinigameManager.m_ScorePlace2) m_MinigameManager.m_P3Place = 2;
        if (GameManager.m_Instance.m_Player3.score == m_MinigameManager.m_ScorePlace3) m_MinigameManager.m_P3Place = 3;
        if (GameManager.m_Instance.m_Player3.score == m_MinigameManager.m_ScorePlace4) m_MinigameManager.m_P3Place = 4;

        if (GameManager.m_Instance.m_Player4.score == m_MinigameManager.m_ScorePlace1) m_MinigameManager.m_P4Place = 1;
        if (GameManager.m_Instance.m_Player4.score == m_MinigameManager.m_ScorePlace2) m_MinigameManager.m_P4Place = 2;
        if (GameManager.m_Instance.m_Player4.score == m_MinigameManager.m_ScorePlace3) m_MinigameManager.m_P4Place = 3;
        if (GameManager.m_Instance.m_Player4.score == m_MinigameManager.m_ScorePlace4) m_MinigameManager.m_P4Place = 4;
    }

    IEnumerator DelayBeforeRaisingResultBar()
    {
        resultBarNeonTopImage[0].enabled = true;
        resultBarNeonTopImage[1].enabled = true;
        resultBarNeonTopImage[2].enabled = true;
        resultBarNeonTopImage[3].enabled = true;

        yield return new WaitForSeconds(1.5f);
        RaisingResultBar();
    }

    IEnumerator DelayBeforeTurnNeonOn()
    {
        if (!neonTurnedOn)
        {
            yield return new WaitForSeconds(1f);
            neonTurnedOn = true;
            TurnNeonOn();
        }
    }

    void TurnNeonOn()
    {
        resultBarNeonTopAnimator[0].GetComponent<Animator>().SetBool("TurnOn", true);
        resultBarNeonTopAnimator[1].GetComponent<Animator>().SetBool("TurnOn", true);
        resultBarNeonTopAnimator[2].GetComponent<Animator>().SetBool("TurnOn", true);
        resultBarNeonTopAnimator[3].GetComponent<Animator>().SetBool("TurnOn", true);

        StartCoroutine(DelayBeforeUpdatingMinigameState());
    }

    IEnumerator DelayBeforeUpdatingMinigameState()
    {
        yield return new WaitForSeconds(2.5f);
        m_MinigameManager.UpdateMinigameState();
    }

    void RaisingResultBar()
    {
        StartCoroutine(DelayBeforeTurnNeonOn());

        switch (GameManager.m_Instance.m_NumOfPlayers)
        {

            case 1:
                m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[0].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                break;
            case 2:
                m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[0].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[1].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                break;
            case 3:
                m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[0].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[1].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[2].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                break;
            case 4:
                m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[0].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[1].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[2].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                m_P4Bar.value = Mathf.Lerp(m_P4Bar.value, ResultBarAmount(GameManager.m_Instance.m_Players[3].GetComponent<Player>().m_Score, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                break;



                //    case 1:
                //        m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        break;
                //    case 2:
                //        m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        m_FirstDelay -= Time.deltaTime;

                //        if (m_MinigameManager.m_P1Place == 1)
                //            m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        else
                //            m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        break;
                //    case 3:
                //        m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace3, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace3, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace3, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        m_FirstDelay -= Time.deltaTime;

                //        if (m_MinigameManager.m_P1Place == 1 || m_MinigameManager.m_P1Place == 2)
                //            m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P2Place == 1 || m_MinigameManager.m_P2Place == 2)
                //            m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P3Place == 1 || m_MinigameManager.m_P3Place == 2)
                //            m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        m_SecondDelay -= Time.deltaTime;

                //        if (m_MinigameManager.m_P1Place == 1)
                //            m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        else if (m_MinigameManager.m_P2Place == 1)
                //            m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        else
                //            m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        break;
                //    case 4:
                //        m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace4, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace4, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace4, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        m_P4Bar.value = Mathf.Lerp(m_P4Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace4, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        m_FirstDelay -= Time.deltaTime;

                //        if (m_MinigameManager.m_P1Place == 1 || m_MinigameManager.m_P1Place == 2 || m_MinigameManager.m_P1Place == 3)
                //            m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace3, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P2Place == 1 || m_MinigameManager.m_P2Place == 2 || m_MinigameManager.m_P2Place == 3)
                //            m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace3, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P3Place == 1 || m_MinigameManager.m_P3Place == 2 || m_MinigameManager.m_P3Place == 3)
                //            m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace3, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P4Place == 1 || m_MinigameManager.m_P4Place == 2 || m_MinigameManager.m_P4Place == 3)
                //            m_P4Bar.value = Mathf.Lerp(m_P4Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace3, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        m_SecondDelay -= Time.deltaTime;

                //        if (m_MinigameManager.m_P1Place == 1 || m_MinigameManager.m_P1Place == 2)
                //            m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P2Place == 1 || m_MinigameManager.m_P2Place == 2)
                //            m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P3Place == 1 || m_MinigameManager.m_P3Place == 2)
                //            m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        if (m_MinigameManager.m_P4Place == 1 || m_MinigameManager.m_P4Place == 2)
                //            m_P4Bar.value = Mathf.Lerp(m_P4Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace2, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        m_ThirdDelay -= Time.deltaTime;

                //        if (m_MinigameManager.m_P1Place == 1)
                //            m_P1Bar.value = Mathf.Lerp(m_P1Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        else if (m_MinigameManager.m_P2Place == 1)
                //            m_P2Bar.value = Mathf.Lerp(m_P2Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        else if (m_MinigameManager.m_P3Place == 1)
                //            m_P3Bar.value = Mathf.Lerp(m_P3Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);
                //        else
                //            m_P4Bar.value = Mathf.Lerp(m_P4Bar.value, ResultBarAmount(m_MinigameManager.m_ScorePlace1, 0, m_MaxScore, 0, 1), m_Speed * Time.deltaTime);

                //        break;
                //}
        }
    }

    void ScreenFading()
    {
        if (m_MinigameManager.m_SecondFadingCanvas.alpha < 0.6f)
        {
            m_MinigameManager.m_SecondFadingCanvas.alpha += Time.deltaTime / m_MinigameManager.m_FadeTime;
        }
        else
        {
            m_MinigameManager.m_DelayToShowRewards -= Time.deltaTime;
            if (m_MinigameManager.m_DelayToShowRewards < 0.0f)
            {
                // Going to the Reward Selection Minigame state;
                //m_MinigameManager.UpdateMinigameState();
            }
        }
    }
}