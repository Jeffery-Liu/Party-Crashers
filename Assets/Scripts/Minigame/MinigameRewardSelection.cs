using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MinigameRewardSelection : MonoBehaviour
{
    public enum Rewards
    {
        Damage,
        Hearts,
        AttackSpeed,
        MovementSpeed
    }

    private MinigameManager m_MinigameManager;
    private GameObject m_RewardCanvas;
    private GameObject m_BossPromptCanvas;

    //private bool m_IsRewardEnded;

    public Button[] m_RewardButtons = new Button[4];
    public Button[] m_BossPromptButtons = new Button[2];

    [HideInInspector]
    public EventSystem m_ES;
    [HideInInspector]
    public StandaloneInputModule m_SIM;

    //private bool m_IsInputSet;

    public Text m_RewardTitle;
    public Text m_RewardTilePlayerNumber;

    [HideInInspector]
    public bool rewardsShown;
    [HideInInspector]
    public bool firstButtonPressed, secondButtonPressed, thirdButtonPressed, fourthButtonPressed;
    [HideInInspector]
    public bool m_IsFightingBoss;
    public bool m_IsBossFightAnswered;


    public int m_RewardsSelected;

    void Awake()
    {
        m_MinigameManager = GetComponent<MinigameManager>();
        m_RewardCanvas = m_MinigameManager.m_RewardSelectionCanvas.gameObject;// GameObject.Find("Reward Canvas");
        m_BossPromptCanvas = m_MinigameManager.m_BossPromptCanvas.gameObject; // GameObject.Find("BossPrompt Canvas");

        m_ES = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        m_SIM = GameObject.Find("EventSystem").GetComponent<StandaloneInputModule>();

        m_RewardButtons[0] = GameObject.Find("First Reward Button").GetComponent<Button>();   //Damage
        m_RewardButtons[1] = GameObject.Find("Second Reward Button").GetComponent<Button>();  //Hearts
        m_RewardButtons[2] = GameObject.Find("Third Reward Button").GetComponent<Button>();   //Attack Speed
        m_RewardButtons[3] = GameObject.Find("Fourth Reward Button").GetComponent<Button>();  //Movement Speed

        m_BossPromptButtons[0] = GameObject.Find("NO").GetComponent<Button>();
        m_BossPromptButtons[1] = GameObject.Find("YES").GetComponent<Button>();

        m_RewardsSelected = 0;

        //m_IsInputSet = false;
        m_IsBossFightAnswered = false;
        m_IsFightingBoss = false;
        //m_IsRewardEnded = false;
    }

    void Update()
    {
        //if (m_MinigameManager.GetMinigameState().Equals(MinigameManager.EMinigameState.RewardSelection))
        if (m_MinigameManager.GetMinigameState().Equals(MinigameManager.EMinigameState.BossPrompt))
        {
            if (!rewardsShown)
            {
//                m_RewardCanvas.SetActive(true);
                m_ES.SetSelectedGameObject(null);
                m_ES.enabled = false;
                m_ES.enabled = true;
                m_ES.SetSelectedGameObject(m_RewardButtons[0].gameObject);
                rewardsShown = true;
            }

            SetupInput();

            //if(!m_IsRewardEnded)
            //{
            //    m_IsRewardEnded = true;
            //    EndRewards();
            //}
//            UpdateCurrentPlayer();
        }
    }

    void SetupInput()
    {
        switch (GameManager.m_Instance.m_NumOfPlayers)
        {
            //PLAYER 1
            case 1:
                if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                {
                    m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                    m_SIM.verticalAxis = ("Vertical_Keyboard");
                    m_SIM.submitButton = ("Submit_Keyboard");
                }
                else
                {
                    m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                    m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                    m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                }
                break;

            //PLAYER 2
            case 2:
                if (m_RewardsSelected == 0)                                          //FIRST BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 1)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 1)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                        }
                    }
                }
                else if (m_RewardsSelected == 1)                                    //SECOND BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 2)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 2)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {

                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                        }
                    }
                }
                break;

            //PLAYER 3
            case 3:
                if (m_RewardsSelected == 0)                                          //FIRST BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 1)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 1)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                    }
                    else if (m_MinigameManager.m_P3Place == 1)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player3.m_Controller);
                    }
                }
                else if (m_RewardsSelected == 1)                                    //SECOND BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 2)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 2)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                    }
                    else if (m_MinigameManager.m_P3Place == 2)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player3.m_Controller);
                    }
                }
                else if (m_RewardsSelected == 2)                                    //THIRD BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 3)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 3)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                    }
                    else if (m_MinigameManager.m_P3Place == 3)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player3.m_Controller);
                    }
                }
                break;
            //PLAYER 4
            case 4:
                if (m_RewardsSelected == 0)                                          //FIRST BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 1)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 1)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                    }
                    else if (m_MinigameManager.m_P3Place == 1)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player3.m_Controller);
                    }
                    else if (m_MinigameManager.m_P4Place == 1)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player4.m_Controller);
                    }
                }
                else if (m_RewardsSelected == 1)                                    //SECOND BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 2)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 2)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                    }
                    else if (m_MinigameManager.m_P3Place == 2)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player3.m_Controller);
                    }
                    else if (m_MinigameManager.m_P4Place == 2)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player4.m_Controller);
                    }

                }
                else if (m_RewardsSelected == 2)                                    //THIRD BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 3)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 3)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                    }
                    else if (m_MinigameManager.m_P3Place == 3)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player3.m_Controller);
                    }
                    else if (m_MinigameManager.m_P4Place == 3)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player4.m_Controller);
                    }
                }
                else if (m_RewardsSelected == 3)                                    //FOURTH BUTTON PRESS
                {
                    if (m_MinigameManager.m_P1Place == 4)
                    {
                        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
                        {
                            m_SIM.horizontalAxis = ("Horizontal_Keyboard");
                            m_SIM.verticalAxis = ("Vertical_Keyboard");
                            m_SIM.submitButton = ("Submit_Keyboard");
                        }
                        else
                        {
                            m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player1.m_Controller);
                            m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);
                        }
                    }
                    else if (m_MinigameManager.m_P2Place == 4)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player2.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player2.m_Controller);
                    }
                    else if (m_MinigameManager.m_P3Place == 4)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player3.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player3.m_Controller);
                    }
                    else if (m_MinigameManager.m_P4Place == 4)
                    {
                        m_SIM.horizontalAxis = ("Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.verticalAxis = ("Vertical_" + GameManager.m_Instance.m_Player4.m_Controller);
                        m_SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player4.m_Controller);
                    }
                }
                break;
        }

    }

    //FUNCTI.  TO SET UP ES.CURRENTLYSELECTED BUTTONS AFTER PRESSING

    //Damage Button
    public void FirstRewardButton()
    {
        m_RewardButtons[0].gameObject.SetActive(false);
        firstButtonPressed = true;
        ++m_RewardsSelected;

        addReward(Rewards.Damage);

        if (!secondButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[1].gameObject);

        else if (!thirdButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[2].gameObject);

        else if (!fourthButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[3].gameObject);
    }

    //Hearts Button
    public void SecondRewardButton()
    {
        m_RewardButtons[1].gameObject.SetActive(false);
        secondButtonPressed = true;
        ++m_RewardsSelected;

        addReward(Rewards.Hearts);

        if (!firstButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[0].gameObject);

        else if (!thirdButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[2].gameObject);


        else if (!fourthButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[3].gameObject);
    }

    //Attack Speed Button
    public void ThirdRewardButton()
    {
        m_RewardButtons[2].gameObject.SetActive(false);
        thirdButtonPressed = true;
        ++m_RewardsSelected;

        addReward(Rewards.AttackSpeed);

        if (!firstButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[0].gameObject);


        else if (!secondButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[1].gameObject);


        else if (!fourthButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[3].gameObject);
    }

    //Movement Speed Button
    public void FourthRewardButton()
    {
        m_RewardButtons[3].gameObject.SetActive(false);
        fourthButtonPressed = true;
        ++m_RewardsSelected;

        addReward(Rewards.MovementSpeed);

        if (!firstButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[0].gameObject);


        else if (!secondButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[1].gameObject);


        else if (!thirdButtonPressed)
            m_ES.SetSelectedGameObject(m_RewardButtons[2].gameObject);
    }

    public void EndRewards()
    {
        //if (m_RewardsSelected >= GameManager.m_Instance.m_NumOfPlayers)
        //{
            m_MinigameManager.UpdateMinigameState();
        //}
    }

    public void BossNo()
    {
        m_IsBossFightAnswered = true;
        m_IsFightingBoss = false;
    }
    public void BossYes()
    {
        m_IsBossFightAnswered = true;
        m_IsFightingBoss = true;
    }
    private void UpdateCurrentPlayer()
    {
        switch (m_RewardsSelected)
        {
            case 0:
                if (m_MinigameManager.m_P1Place == 1)
                {
                    m_RewardTilePlayerNumber.text = "Player 1";
                    m_RewardTilePlayerNumber.color = new Color(240, 110, 110);
                }
                else if (m_MinigameManager.m_P2Place == 1)
                {
                    m_RewardTilePlayerNumber.text = "Player 2";
                    m_RewardTilePlayerNumber.color = new Color(110, 138, 240);
                }
                else if (m_MinigameManager.m_P3Place == 1)
                {
                    m_RewardTilePlayerNumber.text = "Player 3";
                    m_RewardTilePlayerNumber.color = new Color(125, 212, 136);
                }
                else if (m_MinigameManager.m_P4Place == 1)
                {
                    m_RewardTilePlayerNumber.text = "Player 4";
                    m_RewardTilePlayerNumber.color = new Color(227, 217, 90);
                }
                break;
            case 1:
                if (m_MinigameManager.m_P1Place == 2)
                {
                    m_RewardTilePlayerNumber.text = "Player 1";
                    m_RewardTilePlayerNumber.color = new Color(240, 110, 110);
                }
                else if (m_MinigameManager.m_P2Place == 2)
                {
                    m_RewardTilePlayerNumber.text = "Player 2";
                    m_RewardTilePlayerNumber.color = new Color(110, 138, 240);
                }
                else if (m_MinigameManager.m_P3Place == 2)
                {
                    m_RewardTilePlayerNumber.text = "Player 3";
                    m_RewardTilePlayerNumber.color = new Color(125, 212, 136);
                }
                else if (m_MinigameManager.m_P4Place == 2)
                {
                    m_RewardTilePlayerNumber.text = "Player 4";
                    m_RewardTilePlayerNumber.color = new Color(227, 217, 90);
                }
                break;
            case 2:
                if (m_MinigameManager.m_P1Place == 3)
                {
                    m_RewardTilePlayerNumber.text = "Player 1";
                    m_RewardTilePlayerNumber.color = new Color(240, 110, 110);
                }
                else if (m_MinigameManager.m_P2Place == 3)
                {
                    m_RewardTilePlayerNumber.text = "Player 2";
                    m_RewardTilePlayerNumber.color = new Color(110, 138, 240);
                }
                else if (m_MinigameManager.m_P3Place == 3)
                {
                    m_RewardTilePlayerNumber.text = "Player 3";
                    m_RewardTilePlayerNumber.color = new Color(125, 212, 136);
                }
                else if (m_MinigameManager.m_P4Place == 3)
                {
                    m_RewardTilePlayerNumber.text = "Player 4";
                    m_RewardTilePlayerNumber.color = new Color(227, 217, 90);
                }
                break;
            case 3:
                if (m_MinigameManager.m_P1Place == 4)
                {
                    m_RewardTilePlayerNumber.text = "Player 1";
                    m_RewardTilePlayerNumber.color = new Color(240, 110, 110);
                }
                else if (m_MinigameManager.m_P2Place == 4)
                {
                    m_RewardTilePlayerNumber.text = "Player 2";
                    m_RewardTilePlayerNumber.color = new Color(110, 138, 240);
                }
                else if (m_MinigameManager.m_P3Place == 4)
                {
                    m_RewardTilePlayerNumber.text = "Player 3";
                    m_RewardTilePlayerNumber.color = new Color(125, 212, 136);
                }
                else if (m_MinigameManager.m_P4Place == 4)
                {
                    m_RewardTilePlayerNumber.text = "Player 4";
                    m_RewardTilePlayerNumber.color = new Color(227, 217, 90);
                }
                break;
            default:
                m_RewardTilePlayerNumber.text = "Player 1";
                break;
        }
    }

    private Player.PLAYER getWhosPicking()
    {
        switch (m_RewardsSelected)
        {
            case 1:
                return (Player.PLAYER)System.Enum.Parse(typeof(Player.PLAYER), "P" + m_MinigameManager.m_P1Place);
            case 2:
                return (Player.PLAYER)System.Enum.Parse(typeof(Player.PLAYER), "P" + m_MinigameManager.m_P2Place);
            case 3:
                return (Player.PLAYER)System.Enum.Parse(typeof(Player.PLAYER), "P" + m_MinigameManager.m_P3Place);
            case 4:
                return (Player.PLAYER)System.Enum.Parse(typeof(Player.PLAYER), "P" + m_MinigameManager.m_P4Place);
            default:
                return (Player.PLAYER)System.Enum.Parse(typeof(Player.PLAYER), "P" + m_MinigameManager.m_P2Place);
        }
    }

    private void addReward(Rewards reward)
    {
        if (reward == Rewards.Damage)
        {
            if (getWhosPicking() == Player.PLAYER.P1)
            {
                GameManager.m_Instance.m_Player1.damage++;
            }
            else if (getWhosPicking() == Player.PLAYER.P2)
            {
                GameManager.m_Instance.m_Player2.damage++;
            }
            else if (getWhosPicking() == Player.PLAYER.P3)
            {
                GameManager.m_Instance.m_Player3.damage++;
            }
            else if (getWhosPicking() == Player.PLAYER.P4)
            {
                GameManager.m_Instance.m_Player4.damage++;
            }
        }
        if (reward == Rewards.Hearts)
        {
            if (getWhosPicking() == Player.PLAYER.P1)
            {
                GameManager.m_Instance.m_Player1.heartUpgrades++;
            }
            else if (getWhosPicking() == Player.PLAYER.P2)
            {
                GameManager.m_Instance.m_Player2.heartUpgrades++;
            }
            else if (getWhosPicking() == Player.PLAYER.P3)
            {
                GameManager.m_Instance.m_Player3.heartUpgrades++;
            }
            else if (getWhosPicking() == Player.PLAYER.P4)
            {
                GameManager.m_Instance.m_Player4.heartUpgrades++;
            }
        }
        if (reward == Rewards.AttackSpeed)
        {
            if (getWhosPicking() == Player.PLAYER.P1)
            {
                GameManager.m_Instance.m_Player1.attackSpeed++;
            }
            else if (getWhosPicking() == Player.PLAYER.P2)
            {
                GameManager.m_Instance.m_Player2.attackSpeed++;
            }
            else if (getWhosPicking() == Player.PLAYER.P3)
            {
                GameManager.m_Instance.m_Player3.attackSpeed++;
            }
            else if (getWhosPicking() == Player.PLAYER.P4)
            {
                GameManager.m_Instance.m_Player4.attackSpeed++;
            }
        }
        if (reward == Rewards.MovementSpeed)
        {
            if (getWhosPicking() == Player.PLAYER.P1)
            {
                GameManager.m_Instance.m_Player1.movementSpeed++;
            }
            else if (getWhosPicking() == Player.PLAYER.P2)
            {
                GameManager.m_Instance.m_Player2.movementSpeed++;
            }
            else if (getWhosPicking() == Player.PLAYER.P3)
            {
                GameManager.m_Instance.m_Player3.movementSpeed++;
            }
            else if (getWhosPicking() == Player.PLAYER.P4)
            {
                GameManager.m_Instance.m_Player4.movementSpeed++;
            }
        }
    }
}
