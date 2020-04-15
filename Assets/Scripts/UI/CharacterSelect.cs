using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public enum PlayerOne
    {
        nullType,
        P1,
        P2,
        P3,
        P4,
        Keyboard
    }
    public enum PlayerTwo
    {
        nullType,
        P1,
        P2,
        P3,
        P4,
        Keyboard
    }
    public enum PlayerThree
    {
        nullType,
        P1,
        P2,
        P3,
        P4,
        Keyboard
    }
    public enum PlayerFour
    {
        nullType,
        P1,
        P2,
        P3,
        P4,
        Keyboard
    }

    [System.Serializable]
    public struct Character
    {
        public Texture[] characterTexture;
        public RawImage characterSelectIcon;
        public Text characterName;
        public int index;
        public float cooldownCounter;
    }
    public Texture emptyTexture;

    //[HideInInspector]
    public PlayerOne firstPlayer;
    //[HideInInspector]
    public PlayerTwo secondPlayer;
    //[HideInInspector]
    public PlayerThree thirdPlayer;
    //[HideInInspector]
    public PlayerFour fourthPlayer;
    //[HideInInspector]
    public Character P1, P2, P3, P4;
    float cooldown = 0.3f;

    //[HideInInspector]
    public bool P1Join, P2Join, P3Join, P4Join, KeyboardJoin;
    //[HideInInspector]
    public bool canLockIn_P1, canLockIn_P2, canLockIn_P3, canLockIn_P4, canLockIn_Keyboard;
    public bool P1Locked, P2Locked, P3Locked, P4Locked;
    //[HideInInspector]
    //public bool canBack, canLockIn, allowToLock;
    public bool canBack;

    public int playersLockedIn;
    public bool canStartGame;

    public GameObject[] readyText;
    public GameObject lockInText;

    //Bools for preventing adding playersLockedIn with each "A" button
    public bool playerOneJoined, playerTwoJoined, playerThreeJoined, playerFourJoined;

    MenuManager menuManager;

    public AudioSource AS;
    public AudioClip[] SFX;



    //TEMP.!
    public Text[] tempPlayerNumbersText;
    void Awake()
    {
        menuManager = GetComponent<MenuManager>();
        P1.characterSelectIcon = GameObject.Find("Character Select Plate P1/Portrait Image").GetComponent<RawImage>();
        P2.characterSelectIcon = GameObject.Find("Character Select Plate P2/Portrait Image").GetComponent<RawImage>();
        P3.characterSelectIcon = GameObject.Find("Character Select Plate P3/Portrait Image").GetComponent<RawImage>();
        P4.characterSelectIcon = GameObject.Find("Character Select Plate P4/Portrait Image").GetComponent<RawImage>();

        P1.characterName = GameObject.Find("Character Select Plate P1/Character Name").GetComponent<Text>();
        P2.characterName = GameObject.Find("Character Select Plate P2/Character Name").GetComponent<Text>();
        P3.characterName = GameObject.Find("Character Select Plate P3/Character Name").GetComponent<Text>();
        P4.characterName = GameObject.Find("Character Select Plate P4/Character Name").GetComponent<Text>();

        readyText[0] = GameObject.Find("Character Select Plate P1/READY");
        readyText[1] = GameObject.Find("Character Select Plate P2/READY");
        readyText[2] = GameObject.Find("Character Select Plate P3/READY");
        readyText[3] = GameObject.Find("Character Select Plate P4/READY");

        tempPlayerNumbersText[0] = GameObject.Find("Character Select Plate P1/Character Name").GetComponent<Text>();
        tempPlayerNumbersText[1] = GameObject.Find("Character Select Plate P2/Character Name").GetComponent<Text>();
        tempPlayerNumbersText[2] = GameObject.Find("Character Select Plate P3/Character Name").GetComponent<Text>();
        tempPlayerNumbersText[3] = GameObject.Find("Character Select Plate P4/Character Name").GetComponent<Text>();

        P1.cooldownCounter = -1; P2.cooldownCounter = -1; P3.cooldownCounter = -1; P4.cooldownCounter = -1;

        AS = GetComponent<AudioSource>();
        //canLockIn = true;
    }

    void Update()
    {
        PlayerIndexSorting();
        JoinGame();
        SelectCharacter();
        LockInPlayer();
        ShowLockedInImage();
        UnLockPlayer();
        BackToMainMenu();
        StartGame();

        if (P1Locked || P2Locked || P3Locked || P4Locked)
            canBack = false;
        else
            canBack = true;

        if (canStartGame)
        {
            lockInText.GetComponent<Text>().text = "'A'/ENTER START";
            if (Input.GetButtonDown("Jump_" + GameManager.m_Instance.m_Player1.m_Controller))
            {
                AssignCharacteModels();
                SceneManager.LoadScene(GameManager.m_Instance.m_LevelToStart);
            }
        }
        else
        {
            lockInText.GetComponent<Text>().text = "'A'/ENTER LOCKIN";
        }

        if (GameManager.m_Instance.m_NumOfPlayers == 1)
        {
            tempPlayerNumbersText[0].enabled = true;
            tempPlayerNumbersText[1].enabled = false;
            tempPlayerNumbersText[2].enabled = false;
            tempPlayerNumbersText[3].enabled = false;
        }
        else if (GameManager.m_Instance.m_NumOfPlayers == 2)
        {
            tempPlayerNumbersText[0].enabled = true;
            tempPlayerNumbersText[1].enabled = true;
            tempPlayerNumbersText[2].enabled = false;
            tempPlayerNumbersText[3].enabled = false;
        }
        else if (GameManager.m_Instance.m_NumOfPlayers == 3)
        {
            tempPlayerNumbersText[0].enabled = true;
            tempPlayerNumbersText[1].enabled = true;
            tempPlayerNumbersText[2].enabled = true;
            tempPlayerNumbersText[3].enabled = false;
        }

        else if (GameManager.m_Instance.m_NumOfPlayers == 4)
        {
            tempPlayerNumbersText[0].enabled = true;
            tempPlayerNumbersText[1].enabled = true;
            tempPlayerNumbersText[2].enabled = true;
            tempPlayerNumbersText[3].enabled = true;
        }
    }

    void PlayerIndexSorting()
    {
        if (P1.index > 3)
            P1.index = 0;
        if (P1.index < 0)
            P1.index = 3;
        //////////////////
        if (P2.index > 3)
            P2.index = 0;
        if (P2.index < 0)
            P2.index = 3;
        //////////////////
        if (P3.index > 3)
            P3.index = 0;
        if (P3.index < 0)
            P3.index = 3;
        //////////////////
        if (P4.index > 3)
            P4.index = 0;
        if (P4.index < 0)
            P4.index = 3;
    }
    public void AssignController(Player.Controller controller)
    {
        switch (GameManager.m_Instance.m_NumOfPlayers)
        {
            case 1:
                GameManager.m_Instance.m_Player1.m_Controller = controller;
                break;
            case 2:
                GameManager.m_Instance.m_Player2.m_Controller = controller;
                break;
            case 3:
                GameManager.m_Instance.m_Player3.m_Controller = controller;
                break;
            case 4:
                GameManager.m_Instance.m_Player4.m_Controller = controller;
                break;
        }
    }

    void JoinGame()
    {
        if (GameManager.m_Instance.m_NumOfPlayers <= 4)
        {
            if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Jump_P1"))
            {
                if (!P1Join)
                {
                    int players = ++GameManager.m_Instance.m_NumOfPlayers;
                    AssignController(Player.Controller.P1);
                    Debug.Log("Player " + players + " has Joined!");
                    P1Join = true;

                    if (firstPlayer != PlayerOne.P1)
                    {
                        if (secondPlayer == PlayerTwo.nullType)
                        {
                            secondPlayer = PlayerTwo.P1;
                        }
                        else if (thirdPlayer == PlayerThree.nullType)
                        {
                            thirdPlayer = PlayerThree.P1;
                        }
                        else if (fourthPlayer == PlayerFour.nullType)
                        {
                            fourthPlayer = PlayerFour.P1;
                        }
                    }
                }
                StartCoroutine(DelayBeforeAllowingToJoin(1));
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Jump_P2"))
        {
            if (!P2Join)
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                AssignController(Player.Controller.P2);
                Debug.Log("Player " + players + " has Joined!");
                P2Join = true;

                if (firstPlayer != PlayerOne.P2)
                {
                    if (secondPlayer == PlayerTwo.nullType)
                    {
                        secondPlayer = PlayerTwo.P2;
                    }
                    else if (thirdPlayer == PlayerThree.nullType)
                    {
                        thirdPlayer = PlayerThree.P2;
                    }
                    else if (fourthPlayer == PlayerFour.nullType)
                    {
                        fourthPlayer = PlayerFour.P2;
                    }
                }
                StartCoroutine(DelayBeforeAllowingToJoin(2));
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Jump_P3"))
        {
            if (!P3Join)
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                AssignController(Player.Controller.P3);
                Debug.Log("Player " + players + " has Joined!");
                P3Join = true;

                if (firstPlayer != PlayerOne.P3)
                {
                    if (secondPlayer == PlayerTwo.nullType)
                    {
                        secondPlayer = PlayerTwo.P3;
                    }
                    else if (thirdPlayer == PlayerThree.nullType)
                    {
                        thirdPlayer = PlayerThree.P3;
                    }
                    else if (fourthPlayer == PlayerFour.nullType)
                    {
                        fourthPlayer = PlayerFour.P3;
                    }
                }
                StartCoroutine(DelayBeforeAllowingToJoin(3));
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Jump_P4"))
        {
            if (!P4Join)
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                AssignController(Player.Controller.P4);
                Debug.Log("Player " + players + " has Joined!");
                P4Join = true;

                if (firstPlayer != PlayerOne.P4)
                {
                    if (secondPlayer == PlayerTwo.nullType)
                    {
                        secondPlayer = PlayerTwo.P4;
                    }
                    else if (thirdPlayer == PlayerThree.nullType)
                    {
                        thirdPlayer = PlayerThree.P4;
                    }
                    else if (fourthPlayer == PlayerFour.nullType)
                    {
                        fourthPlayer = PlayerFour.P4;
                    }
                }
                StartCoroutine(DelayBeforeAllowingToJoin(4));
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Submit_Keyboard"))
        {
            if (!KeyboardJoin)
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                AssignController(Player.Controller.Keyboard);
                Debug.Log("Player " + players + " has Joined!");
                KeyboardJoin = true;

                if (firstPlayer != PlayerOne.Keyboard)
                {
                    if (secondPlayer == PlayerTwo.nullType)
                    {
                        secondPlayer = PlayerTwo.Keyboard;
                    }
                    else if (thirdPlayer == PlayerThree.nullType)
                    {
                        thirdPlayer = PlayerThree.Keyboard;
                    }
                    else if (fourthPlayer == PlayerFour.nullType)
                    {
                        fourthPlayer = PlayerFour.Keyboard;
                    }
                }
                StartCoroutine(DelayBeforeAllowingToJoin(5));
            }
        }
    }

    //HERE
    IEnumerator DelayBeforeAllowingToJoin(int playerNumber)
    {
        yield return new WaitForSeconds(0.1f);
        switch (playerNumber)
        {
            case 1:
                canLockIn_P1 = true;
                break;
            case 2:
                canLockIn_P2 = true;
                break;
            case 3:
                canLockIn_P3 = true;
                break;
            case 4:
                canLockIn_P4 = true;
                break;
            case 5:
                canLockIn_Keyboard = true;
                break;
        }
    }
    void LockInPlayer()
    {
        if (menuManager.canvases[2].activeSelf && canLockIn_P1 && Input.GetButtonDown("Jump_P1"))/* && canLockIn)*/
        {
            if (firstPlayer == PlayerOne.P1 && !playerOneJoined)
            {
                P1Locked = true;
                playerOneJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (secondPlayer == PlayerTwo.P1 && !playerTwoJoined)
            {
                P2Locked = true;
                playerTwoJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (thirdPlayer == PlayerThree.P1 && !playerThreeJoined)
            {
                P3Locked = true;
                playerThreeJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (fourthPlayer == PlayerFour.P1 && !playerFourJoined)
            {
                P4Locked = true;
                playerFourJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            //allowToLock = true;
        }
        if (menuManager.canvases[2].activeSelf && canLockIn_P2 && Input.GetButtonDown("Jump_P2"))/* && canLockIn && !playerTwoJoined)*/
        {
            if (firstPlayer == PlayerOne.P2 && !playerOneJoined)
            {
                P1Locked = true;
                playerOneJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (secondPlayer == PlayerTwo.P2 && !playerTwoJoined)
            {
                P2Locked = true;
                playerTwoJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (thirdPlayer == PlayerThree.P2 && !playerThreeJoined)
            {
                P3Locked = true;
                playerThreeJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (fourthPlayer == PlayerFour.P2 && !playerFourJoined)
            {
                P4Locked = true;
                playerFourJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }

            //allowToLock = true;
        }
        if (menuManager.canvases[2].activeSelf && canLockIn_P3 && Input.GetButtonDown("Jump_P3"))/* && canLockIn && !playerThreeJoined)*/
        {
            if (firstPlayer == PlayerOne.P3 && !playerOneJoined)
            {
                P1Locked = true;
                playerOneJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (secondPlayer == PlayerTwo.P3 && !playerTwoJoined)
            {
                P2Locked = true;
                playerTwoJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (thirdPlayer == PlayerThree.P3 && !playerThreeJoined)
            {
                P3Locked = true;
                playerThreeJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (fourthPlayer == PlayerFour.P3 && !playerFourJoined)
            {
                P4Locked = true;
                playerFourJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            //allowToLock = true;
        }
        if (menuManager.canvases[2].activeSelf && canLockIn_P4 && Input.GetButtonDown("Jump_P4"))/* && canLockIn && !playerFourJoined)*/
        {
            if (firstPlayer == PlayerOne.P4 && !playerOneJoined)
            {
                P1Locked = true;
                playerOneJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (secondPlayer == PlayerTwo.P4 && !playerTwoJoined)
            {
                P2Locked = true;
                playerTwoJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (thirdPlayer == PlayerThree.P4 && !playerThreeJoined)
            {
                P3Locked = true;
                playerThreeJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (fourthPlayer == PlayerFour.P4 && !playerFourJoined)
            {
                P4Locked = true;
                playerFourJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            //allowToLock = true;
        }
        if (menuManager.canvases[2].activeSelf && canLockIn_Keyboard && Input.GetButtonDown("Jump_Keyboard"))
        {
            if (firstPlayer == PlayerOne.Keyboard && !playerOneJoined)
            {
                P1Locked = true;
                playerOneJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (secondPlayer == PlayerTwo.Keyboard && !playerTwoJoined)
            {
                P2Locked = true;
                playerTwoJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (thirdPlayer == PlayerThree.Keyboard && !playerThreeJoined)
            {
                P3Locked = true;
                playerThreeJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
            if (fourthPlayer == PlayerFour.Keyboard && !playerFourJoined)
            {
                P4Locked = true;
                playerFourJoined = true;
                playersLockedIn++;
                AS.PlayOneShot(SFX[1]);
            }
        }
    }

    void ShowLockedInImage()
    {
        if (P1Locked)
            readyText[0].SetActive(true);
        else
            readyText[0].SetActive(false);

        if (P2Locked)
            readyText[1].SetActive(true);
        else
            readyText[1].SetActive(false);

        if (P3Locked)
            readyText[2].SetActive(true);
        else
            readyText[2].SetActive(false);

        if (P4Locked)
            readyText[3].SetActive(true);
        else
            readyText[3].SetActive(false);
    }

    void BackToMainMenu()
    {
        //ONLY P1
        if (menuManager.canvases[2].activeSelf && canBack)
        {
            if (GameManager.m_Instance.m_Player1.m_Controller != Player.Controller.Keyboard)
            {
                if (Input.GetButtonDown("Back_" + GameManager.m_Instance.m_Player1.m_Controller))
                {
                    GameManager.m_Instance.m_NumOfPlayers = 1;
                    playersLockedIn = 0;

                    switch (firstPlayer)
                    {
                        case PlayerOne.P1:
                            P2Join = false;
                            P3Join = false;
                            P4Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = true;
                            canLockIn_P2 = false;
                            canLockIn_P3 = false;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = false;
                            break;
                        case PlayerOne.P2:
                            P1Join = false;
                            P3Join = false;
                            P4Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = true;
                            canLockIn_P3 = false;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = false;
                            break;
                        case PlayerOne.P3:
                            P1Join = false;
                            P2Join = false;
                            P4Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = false;
                            canLockIn_P3 = true;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = false;
                            break;
                        case PlayerOne.P4:
                            P1Join = false;
                            P2Join = false;
                            P3Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = false;
                            canLockIn_P3 = false;
                            canLockIn_P4 = true;
                            canLockIn_Keyboard = false;
                            break;
                        case PlayerOne.Keyboard:
                            P1Join = false;
                            P2Join = false;
                            P3Join = false;
                            P4Join = false;
                            break;
                    }

                    secondPlayer = PlayerTwo.nullType;
                    thirdPlayer = PlayerThree.nullType;
                    fourthPlayer = PlayerFour.nullType;

                    //Set characterSelectIcon to default\
                    //P1.characterSelectIcon.texture = emptyTexture;
                    P2.characterSelectIcon.texture = emptyTexture;
                    P3.characterSelectIcon.texture = emptyTexture;
                    P4.characterSelectIcon.texture = emptyTexture;

                    menuManager.mainMenuActive = true;
                }
            }
            else
            {
                if (Input.GetButtonDown("Back_Keyboard"))
                {
                    menuManager.mainMenuActive = true;
                    GameManager.m_Instance.m_NumOfPlayers = 1;
                    playersLockedIn = 0;

                    switch (firstPlayer)
                    {
                        case PlayerOne.P1:
                            P2Join = false;
                            P3Join = false;
                            P4Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = false;
                            canLockIn_P3 = false;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = true;
                            break;
                        case PlayerOne.P2:
                            P1Join = false;
                            P3Join = false;
                            P4Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = false;
                            canLockIn_P3 = false;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = true;
                            break;
                        case PlayerOne.P3:
                            P1Join = false;
                            P2Join = false;
                            P4Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = false;
                            canLockIn_P3 = false;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = true;
                            break;
                        case PlayerOne.P4:
                            P1Join = false;
                            P2Join = false;
                            P3Join = false;
                            KeyboardJoin = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = false;
                            canLockIn_P3 = false;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = true;
                            break;
                        case PlayerOne.Keyboard:
                            P1Join = false;
                            P2Join = false;
                            P3Join = false;
                            P4Join = false;
                            canLockIn_P1 = false;
                            canLockIn_P2 = false;
                            canLockIn_P3 = false;
                            canLockIn_P4 = false;
                            canLockIn_Keyboard = true;
                            break;
                    }

                    secondPlayer = PlayerTwo.nullType;
                    thirdPlayer = PlayerThree.nullType;
                    fourthPlayer = PlayerFour.nullType;

                    //Set characterSelectIcon to default\
                    //P1.characterSelectIcon.texture = emptyTexture;
                    P2.characterSelectIcon.texture = emptyTexture;
                    P3.characterSelectIcon.texture = emptyTexture;
                    P4.characterSelectIcon.texture = emptyTexture;
                }
            }
        }
    }

    void UnLockPlayer()
    {
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Back_P1"))
        {
            if (firstPlayer == PlayerOne.P1 && P1Locked)
            {
                P1Locked = false;
                canLockIn_P1 = true;
                playerOneJoined = false;
                playersLockedIn--;
            }
            if (secondPlayer == PlayerTwo.P1 && P2Locked)
            {
                P2Locked = false;
                canLockIn_P2 = true;
                playerTwoJoined = false;
                playersLockedIn--;
            }
            if (thirdPlayer == PlayerThree.P1 && P3Locked)
            {
                P3Locked = false;
                canLockIn_P3 = true;
                playerThreeJoined = false;
                playersLockedIn--;
            }
            if (fourthPlayer == PlayerFour.P1 && P4Locked)
            {
                P4Locked = false;
                canLockIn_P4 = true;
                playerFourJoined = false;
                playersLockedIn--;
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Back_P2"))
        {
            if (firstPlayer == PlayerOne.P2 && P1Locked)
            {
                P1Locked = false;
                canLockIn_P1 = true;
                playerOneJoined = false;
                playersLockedIn--;
            }
            if (secondPlayer == PlayerTwo.P2 && P2Locked)
            {
                P2Locked = false;
                canLockIn_P2 = true;
                playerTwoJoined = false;
                playersLockedIn--;
            }
            if (thirdPlayer == PlayerThree.P2 && P3Locked)
            {
                P3Locked = false;
                canLockIn_P3 = true;
                playerThreeJoined = false;
                playersLockedIn--;
            }
            if (fourthPlayer == PlayerFour.P2 && P4Locked)
            {
                P4Locked = false;
                canLockIn_P4 = true;
                playerFourJoined = false;
                playersLockedIn--;
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Back_P3"))
        {
            if (firstPlayer == PlayerOne.P1 && P1Locked)
            {
                P1Locked = false;
                canLockIn_P1 = true;
                playerOneJoined = false;
            }
            if (secondPlayer == PlayerTwo.P3 && P2Locked)
            {
                P2Locked = false;
                canLockIn_P2 = true;
                playerTwoJoined = false;
                playersLockedIn--;
            }
            if (thirdPlayer == PlayerThree.P3 && P3Locked)
            {
                P3Locked = false;
                canLockIn_P3 = true;
                playerThreeJoined = false;
                playersLockedIn--;
            }
            if (fourthPlayer == PlayerFour.P3 && P4Locked)
            {
                P4Locked = false;
                canLockIn_P4 = true;
                playerFourJoined = false;
                playersLockedIn--;
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Back_P4"))
        {
            if (firstPlayer == PlayerOne.P4 && P1Locked)
            {
                P1Locked = false;
                canLockIn_P1 = true;
                playerOneJoined = false;
                playersLockedIn--;
            }
            if (secondPlayer == PlayerTwo.P4 && P2Locked)
            {
                P2Locked = false;
                canLockIn_P2 = true;
                playerTwoJoined = false;
                playersLockedIn--;
            }
            if (thirdPlayer == PlayerThree.P4 && P3Locked)
            {
                P3Locked = false;
                canLockIn_P3 = true;
                playerThreeJoined = false;
                playersLockedIn--;
            }
            if (fourthPlayer == PlayerFour.P4 && P4Locked)
            {
                P4Locked = false;
                canLockIn_P4 = true;
                playerFourJoined = false;
                playersLockedIn--;
            }
        }
        if (menuManager.canvases[2].activeSelf && Input.GetButtonDown("Back_Keyboard"))
        {
            if (firstPlayer == PlayerOne.Keyboard && P1Locked)
            {
                P1Locked = false;
                playerOneJoined = false;
            }
            if (secondPlayer == PlayerTwo.Keyboard && P2Locked)
            {
                P2Locked = false;
                playerTwoJoined = false;
            }
            if (thirdPlayer == PlayerThree.Keyboard && P3Locked)
            {
                P3Locked = false;
                playerThreeJoined = false;
            }
            if (fourthPlayer == PlayerFour.Keyboard && P4Locked)
            {
                P4Locked = false;
                playerFourJoined = false;
            }

            playersLockedIn--;
        }

    }

    void P1Selection()
    {
        if (firstPlayer != PlayerOne.nullType)
        {
            if (menuManager.canvases[2].activeSelf && !P1Locked)
            {
                P1.characterSelectIcon.texture = P1.characterTexture[P1.index];
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller) < 0)    //Scroll Left
                {
                    if (P1.cooldownCounter < Time.time - cooldown || P1.cooldownCounter == -1)
                    {
                        P1.index--;
                        AS.PlayOneShot(SFX[0]);

                        P1.cooldownCounter = Time.time;
                    }
                }
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller) > 0)     //Scroll Right
                {
                    if (P1.cooldownCounter < Time.time - cooldown || P1.cooldownCounter == -1)
                    {
                        P1.index++;
                        AS.PlayOneShot(SFX[0]);

                        P1.cooldownCounter = Time.time;
                    }
                }
            }
            switch (P1.index)
            {
                case 0:
                    P1.characterName.text = "Mascot";
                    break;
                case 1:
                    P1.characterName.text = "Nerd";
                    break;
                case 2:
                    P1.characterName.text = "Bad Boy";
                    break;
                case 3:
                    P1.characterName.text = "Goth";
                    break;
            }
        }
        else
        {
            P1.characterSelectIcon.texture = emptyTexture;
            P1.characterName.text = "";
        }
    }
    void P2Selection()
    {
        if (secondPlayer != PlayerTwo.nullType)
        {
            P2.characterSelectIcon.texture = P2.characterTexture[P2.index];
            if (menuManager.canvases[2].activeSelf && !P2Locked)
            {
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller) < 0)
                {
                    if (P2.cooldownCounter < Time.time - cooldown || P2.cooldownCounter == -1)
                    {
                        P2.index--;
                        AS.PlayOneShot(SFX[0]);

                        P2.cooldownCounter = Time.time;
                    }
                }
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller) > 0)
                {
                    if (P2.cooldownCounter < Time.time - cooldown || P2.cooldownCounter == -1)
                    {
                        P2.index++;
                        AS.PlayOneShot(SFX[0]);

                        P2.cooldownCounter = Time.time;
                    }
                }
            }
            switch (P2.index)
            {
                case 0:
                    P2.characterName.text = "Mascot";
                    break;
                case 1:
                    P2.characterName.text = "Nerd";
                    break;
                case 2:
                    P2.characterName.text = "Bad Boy";
                    break;
                case 3:
                    P2.characterName.text = "Goth";
                    break;
            }
        }
        else
        { 
            P2.characterSelectIcon.texture = emptyTexture;
            P2.characterName.text = "";
        }
    }
    void P3Selection()
    {
        if (thirdPlayer != PlayerThree.nullType)
        {
            P3.characterSelectIcon.texture = P3.characterTexture[P3.index];
            if (menuManager.canvases[2].activeSelf && !P3Locked)
            {
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller) < 0)
                {
                    if (P3.cooldownCounter < Time.time - cooldown || P3.cooldownCounter == -1)
                    {
                        P3.index--;
                        AS.PlayOneShot(SFX[0]);

                        P3.cooldownCounter = Time.time;
                    }
                }
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller) > 0)
                {
                    if (P3.cooldownCounter < Time.time - cooldown || P3.cooldownCounter == -1)
                    {
                        P3.index++;
                        AS.PlayOneShot(SFX[0]);

                        P3.cooldownCounter = Time.time;
                    }
                }
            }
            switch (P3.index)
            {
                case 0:
                    P3.characterName.text = "Mascot";
                    break;
                case 1:
                    P3.characterName.text = "Nerd";;
                    break;
                case 2:
                    P3.characterName.text = "Bad Boy";
                    break;
                case 3:
                    P3.characterName.text = "Goth";
                    break;
            }
        }
        else
        { 
            P3.characterSelectIcon.texture = emptyTexture;
            P3.characterName.text = "";
        }
    }
    void P4Selection()
    {
        if (fourthPlayer != PlayerFour.nullType)
        {
            P4.characterSelectIcon.texture = P4.characterTexture[P4.index];
            if (menuManager.canvases[2].activeSelf && !P4Locked)
            {
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller) < 0)
                {
                    if (P4.cooldownCounter < Time.time - cooldown || P4.cooldownCounter == -1)
                    {
                        P4.index--;
                        AS.PlayOneShot(SFX[0]);

                        P4.cooldownCounter = Time.time;
                    }
                }
                if (Input.GetAxisRaw("Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller) > 0)
                {
                    if (P4.cooldownCounter < Time.time - cooldown || P4.cooldownCounter == -1)
                    {
                        P4.index++;
                        AS.PlayOneShot(SFX[0]);

                        P4.cooldownCounter = Time.time;
                    }
                }
            }
            switch (P4.index)
            {
                case 0:
                    P4.characterName.text = "Mascot";
                    break;
                case 1:
                    P4.characterName.text = "Nerd";
                    break;
                case 2:
                    P4.characterName.text = "Bad Boy";
                    break;
                case 3:
                    P4.characterName.text = "Goth";
                    break;
            }
        }
        else
        { 
            P4.characterSelectIcon.texture = emptyTexture;
            P4.characterName.text = "";
        }
    }
    void SelectCharacter()
    {
        P1Selection();
        P2Selection();
        P3Selection();
        P4Selection();
    }

    void AssignCharacteModels()
    {
        switch (P1.index)
        {
            case 0:
                GameManager.m_Instance.m_Player1.model = Player.Model.Mascot;
                break;
            case 1:
                GameManager.m_Instance.m_Player1.model = Player.Model.Nerd;
                break;
            case 2:
                GameManager.m_Instance.m_Player1.model = Player.Model.Badboy;
                break;
            case 3:
                GameManager.m_Instance.m_Player1.model = Player.Model.Goth;
                break;
        }
        switch (P2.index)
        {
            case 0:
                GameManager.m_Instance.m_Player2.model = Player.Model.Mascot;
                break;
            case 1:
                GameManager.m_Instance.m_Player2.model = Player.Model.Nerd;
                break;
            case 2:
                GameManager.m_Instance.m_Player2.model = Player.Model.Badboy;
                break;
            case 3:
                GameManager.m_Instance.m_Player2.model = Player.Model.Goth;
                break;
        }
        switch (P3.index)
        {
            case 0:
                GameManager.m_Instance.m_Player3.model = Player.Model.Mascot;
                break;
            case 1:
                GameManager.m_Instance.m_Player3.model = Player.Model.Nerd;
                break;
            case 2:
                GameManager.m_Instance.m_Player3.model = Player.Model.Badboy;
                break;
            case 3:
                GameManager.m_Instance.m_Player3.model = Player.Model.Goth;
                break;
        }
        switch (P4.index)
        {
            case 0:
                GameManager.m_Instance.m_Player4.model = Player.Model.Mascot;
                break;
            case 1:
                GameManager.m_Instance.m_Player4.model = Player.Model.Nerd;
                break;
            case 2:
                GameManager.m_Instance.m_Player4.model = Player.Model.Badboy;
                break;
            case 3:
                GameManager.m_Instance.m_Player4.model = Player.Model.Goth;
                break;
        }
    }

    void StartGame()
    {
        if (menuManager.canvases[2].activeSelf && playersLockedIn == GameManager.m_Instance.m_NumOfPlayers)
            StartCoroutine(DelayForCanStartGameBool());
        else
            canStartGame = false;
    }

    IEnumerator DelayForCanStartGameBool()
    {
        yield return new WaitForSeconds(0.1f);
        canStartGame = true;
    }
}