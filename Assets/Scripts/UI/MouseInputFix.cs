using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseInputFix : MonoBehaviour
{
    public EventSystem es;
    public GameObject previouslySelectedGameObject;


    public MenuManager menuManager;
    public MinigameManager minigameManager;
    public MinigameRewardSelection minigameRewardSelection;

    void Awake()
    {
        if (GameObject.Find("Menu Manager") != null)
        {
            GameManager.m_Instance.GetComponent<MouseInputFix>().menuManager = GameObject.Find("Menu Manager").GetComponent<MenuManager>();
        }
    }

    void DisableMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void EnableMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {

        if (GameObject.Find("Menu Manager") != null)
            GameManager.m_Instance.GetComponent<MouseInputFix>().es = GameObject.Find("Main Menu Canvas/EventSystem").GetComponent<EventSystem>();
        else if (GameManager.m_Instance.m_GameState == GameManager.GameState.Minigame)
            GameManager.m_Instance.GetComponent<MouseInputFix>().es = GameObject.Find("MinigameManager/EventSystem").GetComponent<EventSystem>();

        //MENU
        //case GameManager.GameState.MainMenu:
        if (GameObject.Find("Menu Manager") != null)
        {
            DisableMouse();
            MainMenuButtonManualSetting();
        }
        else
        {
            switch (GameManager.m_Instance.m_GameState)
            {
                //DUNGEON
                case GameManager.GameState.Dungeon:
                    if (GameManager.m_Instance.m_Player1.m_Controller != Player.Controller.Keyboard &&
                    GameManager.m_Instance.m_Player2.m_Controller != Player.Controller.Keyboard &&
                    GameManager.m_Instance.m_Player3.m_Controller != Player.Controller.Keyboard &&
                    GameManager.m_Instance.m_Player4.m_Controller != Player.Controller.Keyboard)
                    {
                        DisableMouse();
                    }
                    else //NO KEYBOARD
                    {
                        EnableMouse();
                    }
                    break;

                // MINIGAME
                case GameManager.GameState.Minigame:
                    minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
                    minigameRewardSelection = GameObject.Find("MinigameManager").GetComponent<MinigameRewardSelection>();
                    switch (minigameManager.m_CurrentState)
                    {
                        case MinigameManager.EMinigameState.ScoreAndTimeTrack:

                            //Keyboard Check
                            if (GameManager.m_Instance.m_Player1.m_Controller != Player.Controller.Keyboard &&
                            GameManager.m_Instance.m_Player2.m_Controller != Player.Controller.Keyboard &&
                            GameManager.m_Instance.m_Player3.m_Controller != Player.Controller.Keyboard &&
                            GameManager.m_Instance.m_Player4.m_Controller != Player.Controller.Keyboard)
                            {
                                DisableMouse();
                            }
                            else
                            {
                                EnableMouse();
                            }
                            break;
                        case MinigameManager.EMinigameState.ResultSummary:
                            DisableMouse();
                            break;
                        //case MinigameManager.EMinigameState.RewardSelection:
                        //    MiniGameRewardButtonManualSetting();
                        //    break;
                        case MinigameManager.EMinigameState.BossPrompt:
                            BossPromptButtonManualSetting();
                            break;
                    }
                    break;
            }
        }
    }

    void MiniGameRewardButtonManualSetting()
    {
        if (es != null)
        {
            if (es.currentSelectedGameObject == minigameRewardSelection.m_RewardButtons[0].gameObject)
                previouslySelectedGameObject = minigameRewardSelection.m_RewardButtons[0].gameObject;

            if (es.currentSelectedGameObject == minigameRewardSelection.m_RewardButtons[1].gameObject)
                previouslySelectedGameObject = minigameRewardSelection.m_RewardButtons[1].gameObject;

            if (es.currentSelectedGameObject == minigameRewardSelection.m_RewardButtons[2].gameObject)
                previouslySelectedGameObject = minigameRewardSelection.m_RewardButtons[2].gameObject;

            if (es.currentSelectedGameObject == minigameRewardSelection.m_RewardButtons[3].gameObject)
                previouslySelectedGameObject = minigameRewardSelection.m_RewardButtons[3].gameObject;

            es.SetSelectedGameObject(previouslySelectedGameObject);
        }
    }

    void BossPromptButtonManualSetting()
    {
        if (es != null)
        {
            if (es.currentSelectedGameObject == minigameRewardSelection.m_BossPromptButtons[0].gameObject)
                previouslySelectedGameObject = minigameRewardSelection.m_BossPromptButtons[0].gameObject;

            if (es.currentSelectedGameObject == minigameRewardSelection.m_BossPromptButtons[1].gameObject)
                previouslySelectedGameObject = minigameRewardSelection.m_BossPromptButtons[1].gameObject;

            es.SetSelectedGameObject(previouslySelectedGameObject);
        }
    }

    void MainMenuButtonManualSetting()
    {
        //Menu
        if (es != null)
        {
            for (int i = 0; i < menuManager.allButtons.Length; i++)
            {
                if (es.currentSelectedGameObject == menuManager.allButtons[i])
                {
                    previouslySelectedGameObject = menuManager.allButtons[i];
                }
            }
            es.SetSelectedGameObject(previouslySelectedGameObject);
        }
    }
}
