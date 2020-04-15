/*
 *   Minigame Pre Game Countdown deals with the initial countdown before every mini game.
 *   
 *   PreGameCountdown (initial state) >> ScoreAndTimeTrack >> ResultSummary >> RewardSelecion (final state)
 *
 *   Each state presented above is defined in its own script.
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MinigamePreGameCountdown : MonoBehaviour
{
    public GameObject[] m_StartCounter = new GameObject[4];
    public GameObject m_PartyBar;
    public GameObject m_tutorialText;

    private string m_MinigameSceneName;

    private MinigameManager m_MinigameManager;
    private bool m_IsCoroutineExecuting;

    // Use this for initialization
    void Start()
    {
        m_StartCounter[0] = GameObject.Find("Start Counter/3");
        m_StartCounter[1] = GameObject.Find("Start Counter/2");
        m_StartCounter[2] = GameObject.Find("Start Counter/1");
        m_StartCounter[3] = GameObject.Find("Start Counter/GO!");
        m_MinigameManager = GetComponent<MinigameManager>();
        m_PartyBar = GameObject.Find("PartyBar");
        m_tutorialText = GameObject.Find("Tutorial Text");
        m_IsCoroutineExecuting = false;

        m_MinigameSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_MinigameManager.GetMinigameState() == MinigameManager.EMinigameState.PreGameCountdown
            && !m_IsCoroutineExecuting)
        {
            StartCoroutine(RunCountdown());
        }


        if (m_MinigameSceneName.Equals("BallroomBlitz"))
        {
            m_tutorialText.GetComponent<Text>().text = "Avoid the balls, and try and stay on the platform. The White Balls will explode knocking everyone back";
        }
        if (m_MinigameSceneName.Equals("BreakToTheBeat"))
        {
            m_tutorialText.GetComponent<Text>().text = "Eat as much food as you can while avoiding the flower vases";
        }
        if (m_MinigameSceneName.Equals("DanceFloorRumble"))
        {
            m_tutorialText.GetComponent<Text>().text = "Stand on the green tiles to gain points";
        }
    }

    IEnumerator RunCountdown()
    {
        m_IsCoroutineExecuting = true;
        DisableFeatures();

        //Minigame tutorial text pop-up (Disabled at "GO")
        yield return new WaitForSeconds(1f);
        m_tutorialText.GetComponent<Animator>().SetBool("Show", true);


        //3, 2, 1, GO Countdown

        // Initial 1 seconds delay then '3'
        yield return new WaitForSeconds(2);
        m_StartCounter[0].GetComponent<Text>().enabled = true;

        // '2'
        yield return new WaitForSeconds(1);

        m_StartCounter[0].GetComponent<Text>().enabled = false;
        m_StartCounter[1].GetComponent<Text>().enabled = true;


        // '1'
        yield return new WaitForSeconds(1);

        m_StartCounter[1].GetComponent<Text>().enabled = false;
        m_StartCounter[2].GetComponent<Text>().enabled = true;


        // 'GO'
        yield return new WaitForSeconds(1);

        m_StartCounter[2].GetComponent<Text>().enabled = false;
        m_StartCounter[3].GetComponent<Text>().enabled = true;

        //Disable Tutorial text pop-up
        m_tutorialText.GetComponent<Animator>().SetBool("Show", false);

        // "Erase" 'GO'and reenable features
        yield return new WaitForSeconds(1);

        m_StartCounter[3].GetComponent<Text>().enabled = false;

        EnableFeatures();

        // After the countdown, change the minigame state (PreGameCountdown ==> ScoreAndTimeTrack)
        m_MinigameManager.UpdateMinigameState();

        m_IsCoroutineExecuting = false;
    }

    private void DisableFeatures()
    {
        //Disable Players' movement
        foreach (GameObject player in GameManager.m_Instance.m_Players)
        {
            player.GetComponent<PlayerController>().m_CantMove = true;
            player.GetComponent<Player>().m_CantAttack = true;
        }

        //Disable partybar for countdown
        GameManager.m_Instance.m_PartyBar.m_Active = false;
        m_PartyBar.SetActive(false);
    }

    private void EnableFeatures()
    {
        //Enable Players' movement
        foreach (GameObject player in GameManager.m_Instance.m_Players)
        {
            player.GetComponent<PlayerController>().m_CantMove = false;
            player.GetComponent<Player>().m_CantAttack = false;
        }

        GameManager.m_Instance.m_PartyBar.m_Active = true;
        m_PartyBar.SetActive(true);
    }
}
