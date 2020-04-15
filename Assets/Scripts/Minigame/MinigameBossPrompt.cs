using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MinigameBossPrompt : MonoBehaviour
{

    private MinigameManager m_MinigameManager;
    private MinigameRewardSelection m_MinigameRewardSelection;
    private GameObject m_RewardCanvas;
    private GameObject m_BossPromptCanvas;
    public  GameObject m_BossPromptCanvasButtonNO, m_BossPromptCanvasButtonYES;

    public bool m_PromptShown;
    EventSystem es;

    // Use this for initialization
    void Start()
    {
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        m_MinigameManager = GetComponent<MinigameManager>();
        m_MinigameRewardSelection = GetComponent<MinigameRewardSelection>();

        m_RewardCanvas = m_MinigameManager.m_RewardSelectionCanvas.gameObject;// GameObject.Find("Reward Canvas");
        m_BossPromptCanvas = m_MinigameManager.m_BossPromptCanvas.gameObject; // GameObject.Find("BossPrompt Canvas");

        m_BossPromptCanvasButtonNO = GameObject.Find("NO");
        m_BossPromptCanvasButtonYES = GameObject.Find("YES");
    }

    void SelectedButtonOutline()
    {
        if (m_MinigameRewardSelection.m_ES.currentSelectedGameObject == m_BossPromptCanvasButtonNO)
        {
            m_BossPromptCanvasButtonNO.GetComponent<Outline>().enabled = true;
            m_BossPromptCanvasButtonNO.GetComponent<Animator>().SetBool("Selected", true);

            m_BossPromptCanvasButtonYES.GetComponent<Outline>().enabled = false;
            m_BossPromptCanvasButtonYES.GetComponent<Animator>().SetBool("Selected", false);
        }
        if (m_MinigameRewardSelection.m_ES.currentSelectedGameObject == m_BossPromptCanvasButtonYES)
        {
            m_BossPromptCanvasButtonYES.GetComponent<Outline>().enabled = true;
            m_BossPromptCanvasButtonYES.GetComponent<Animator>().SetBool("Selected", true);

            m_BossPromptCanvasButtonNO.GetComponent<Outline>().enabled = false;
            m_BossPromptCanvasButtonNO.GetComponent<Animator>().SetBool("Selected", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (es.currentSelectedGameObject == null)
            es.SetSelectedGameObject(m_BossPromptCanvasButtonNO);

        if (m_MinigameManager.GetMinigameState().Equals(MinigameManager.EMinigameState.BossPrompt))
        {
            if (!m_PromptShown)
            {
                m_RewardCanvas.SetActive(false);
                m_BossPromptCanvas.SetActive(true);
                m_MinigameRewardSelection.m_ES.SetSelectedGameObject(null);
                m_MinigameRewardSelection.m_ES.enabled = false;
                m_MinigameRewardSelection.m_ES.enabled = true;
                m_MinigameRewardSelection.m_ES.SetSelectedGameObject(m_MinigameRewardSelection.m_BossPromptButtons[0].gameObject);
                m_PromptShown = true;
            }

            SelectedButtonOutline();

            if (m_MinigameRewardSelection.m_IsBossFightAnswered)
            {
                //if (GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_01 ||
                //    GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_02 ||
                //    GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_03)
                //{
                //    //SceneManager.LoadScene(GameManager.m_Instance.m_Tutorial.ToString()); //ballroom blitz

                //    //Reward time
                //    if (!m_MinigameRewardSelection.m_IsFightingBoss)
                //    {
                //        GameManager.m_Instance.m_GameState = GameManager.GameState.Dungeon;
                //        SceneManager.LoadScene(GameManager.m_Instance.m_Tutorial.ToString());
                //    }
                //    else
                //    {
                //        GameManager.m_Instance.m_GameState = GameManager.GameState.Boss;
                //        SceneManager.LoadScene("KaminsBoss");
                //    }
                //}
                //else
                //{
                if (!m_MinigameRewardSelection.m_IsFightingBoss)
                {
                    GameManager.m_Instance.m_GameState = GameManager.GameState.Dungeon;
                    int randInt = Random.Range(2, 5);

                  
                    while (randInt == GameManager.m_Instance.m_LastLevelPlayedIndex)
                    {
                        randInt = Random.Range(2, 5);
                    }
                    SceneManager.LoadScene(randInt);
                }
                else
                {
                    GameManager.m_Instance.m_GameState = GameManager.GameState.Boss;
                    SceneManager.LoadScene("KaminsBoss");
                }
                //}
            }
        }
    }
}
