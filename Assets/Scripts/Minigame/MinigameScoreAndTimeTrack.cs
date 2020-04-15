/*
 *   Minigame Score and Time Track deals with the tracking of each player's score and the overall game time
 *   
 *   PreGameCountdown (initial state) >> ScoreAndTimeTrack >> ResultSummary >> RewardSelecion (final state)
 *
 *   Each state presented above is defined in its own script.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MinigameScoreAndTimeTrack : MonoBehaviour
{
    private MinigameManager m_MinigameManager;
    [HideInInspector]
    public bool             m_IsCoroutineRunning;
    private PartyBar        m_PartyBar;
    private string          m_MinigameSceneName;
    [HideInInspector]
    public float[]          m_RawTime;

    public int              m_PointsToAward;

    // Use this for initialization
    void Start()
    {
        m_PartyBar              = GameManager.m_Instance.m_PartyBar.GetComponent<PartyBar>();
        m_MinigameManager       = GetComponent<MinigameManager>();
        m_IsCoroutineRunning    = false;
        m_MinigameSceneName     = SceneManager.GetActiveScene().name;
        m_RawTime               = new float[GameManager.m_Instance.m_NumOfPlayers];

        for(int i = 0; i < GameManager.m_Instance.m_NumOfPlayers; ++i)
        {
            m_RawTime[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_MinigameManager.GetMinigameState() == MinigameManager.EMinigameState.ScoreAndTimeTrack 
            && !m_IsCoroutineRunning)
        {
            if (m_PartyBar.m_Current <= 0.0f)
            {
                m_MinigameManager.UpdateMinigameState();
                return;
            }

            if(m_MinigameSceneName.Equals("BallroomBlitz"))
            {
                // each player gets points (variable) for each second they are "active" (if stunned, do not receive any points).
                // if player "dies", 5 seconds to respawn without getting any points

                //
                for (int i = 0; i < GameManager.m_Instance.m_NumOfPlayers; ++i)
                {
                    if (!GameManager.m_Instance.m_Players[i].GetComponent<PlayerController>().m_CantMove 
                        && !GameManager.m_Instance.m_Players[i].GetComponent<Player>().m_IsDead)
                    {
                        m_RawTime[i] += Time.deltaTime;
                    }
                }

                StartCoroutine(UpdateScore());
            }
            else if(m_MinigameSceneName.Equals("BreakToTheBeat"))
            {
                // each player gets points (variable) as he/she gets food.
                // if players get food, party bar should not be refilled
            }
            else if(m_MinigameSceneName.Equals("DanceFloorRumble"))
            {
                // players get points (variable) per second by standing on the green squares

            }
        }
    }

    public IEnumerator UpdateScore()
    {
        if (m_IsCoroutineRunning || m_MinigameManager.GetMinigameState() != MinigameManager.EMinigameState.ScoreAndTimeTrack)
        {
            yield break;
        }

        m_IsCoroutineRunning = true;

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < GameManager.m_Instance.m_NumOfPlayers; ++i)
        {
            GameManager.m_Instance.m_Players[i].GetComponent<Player>().m_Score += Mathf.CeilToInt(m_RawTime[i]) * m_PointsToAward;
            m_RawTime[i] = 0.0f;
        }

        m_IsCoroutineRunning = false;
    }
}