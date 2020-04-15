/*
 *   Minigame manager holds a Finite State Machine with the following states that should be the same for all mini games:
 *   
 *   PreGameCountdown (initial state) >> ScoreAndTimeTrack >> ResultSummary >> RewardSelecion (final state)
 * 
 *   Each state presented above is defined in its own script.
 */
using UnityEngine;

[RequireComponent(typeof(MinigamePreGameCountdown))]
public class MinigameManager : MonoBehaviour
{
    [HideInInspector]
    public enum EMinigameState
        {
            PreGameCountdown
          , ScoreAndTimeTrack
          , ResultSummary
//          , RewardSelection
          , BossPrompt
        };
    [HideInInspector]
    public int m_P1Place, m_P2Place, m_P3Place, m_P4Place;
    [HideInInspector]
    public int m_ScorePlace1, m_ScorePlace2, m_ScorePlace3, m_ScorePlace4;

    // Member variables
    public EMinigameState m_CurrentState;
    private PartyBar m_PartyBar;

    public Canvas m_MinigameCanvas;
    public Canvas m_RewardSelectionCanvas;
    public Canvas m_BossPromptCanvas;
    public CanvasGroup m_FirstFadingCanvas;
    public CanvasGroup m_SecondFadingCanvas;

    public float m_DelayToFadeIn;
    public float m_DelayToShowResultBar;
    public float m_DelayToShowRewards;
    public float m_FadeTime;

    private void Start()
    {
        m_MinigameCanvas = GameObject.Find("MinigameCanvas").GetComponent<Canvas>();
        m_PartyBar = GetComponentInChildren<PartyBar>();
        m_CurrentState = EMinigameState.PreGameCountdown;
        m_P1Place = 0;
        m_P2Place = 0;
        m_P3Place = 0;
        m_P4Place = 0;
        m_ScorePlace1 = 0;
        m_ScorePlace2 = 0;
        m_ScorePlace3 = 0;
        m_ScorePlace4 = 0;
        m_RewardSelectionCanvas.gameObject.SetActive(false);
        m_BossPromptCanvas.gameObject.SetActive(false);
    }

    public EMinigameState GetMinigameState()
    {
        return m_CurrentState;
    }

    public void UpdateMinigameState()
    {
        switch (m_CurrentState)
        {
            case EMinigameState.PreGameCountdown:
                m_CurrentState = EMinigameState.ScoreAndTimeTrack;
                break;
            case EMinigameState.ScoreAndTimeTrack:
                m_CurrentState = EMinigameState.ResultSummary;
                break;
            case EMinigameState.ResultSummary:
            //    m_CurrentState = EMinigameState.RewardSelection;
            //    break;
            //case EMinigameState.RewardSelection:
                m_CurrentState = EMinigameState.BossPrompt;
                break;
            default:
                //Debug.LogAssertion("[Minigame Manager] Invalid state update");
                m_CurrentState = EMinigameState.BossPrompt;
                break;
        }
    }
}