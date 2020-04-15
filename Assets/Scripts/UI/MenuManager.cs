using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    [Header("Different Canvases")]
    //In Hir. assign following things
    public GameObject[] canvases;
    public GameObject creditsText;

    [Header("Different 'First Selected' Buttons")]
    public GameObject[] firstSelectedButtons;

    [Header("List of ALL Buttons")]
    public GameObject[] allButtons;

    [Header("Bools")]
    public bool waitedForADelay;
    public bool splashActive, mainMenuActive, playActive, settingsActive, creditsActive;
    [Header("'Back' Button Available")]
    public bool canBack;
    Animator anim;
    EventSystem es;
    CharacterSelect characterSelect;

    void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        es = GameObject.Find("Main Menu Canvas/EventSystem").GetComponent<EventSystem>();
        characterSelect = GetComponent<CharacterSelect>();
        creditsText = GameObject.Find("Credits Text");
    }

    void Start()
    {
        splashActive = true;
        //Cursor.visible = false;
        //InvokeRepeating("test", 0.2f, 0.8f);
    }

    void Update()
    {
        SelectedButtonOutline();

        if (splashActive)
        {
            StartCoroutine(Splash());
            mainMenuActive = false; playActive = false; settingsActive = false; creditsActive = false;
            canvases[0].SetActive(true); canvases[1].SetActive(false); canvases[2].SetActive(false); canvases[3].SetActive(false); canvases[4].SetActive(false);
        }
        else if (mainMenuActive)
        {
            StartCoroutine(MainMenu());
            ///////////////////
            splashActive = false; playActive = false; settingsActive = false; creditsActive = false;
            canvases[0].SetActive(false); canvases[1].SetActive(true); canvases[2].SetActive(false); canvases[3].SetActive(false); canvases[4].SetActive(false);
        }
        else if (playActive)
        {
            StartCoroutine(Play());
            ///////////////////
            splashActive = false; mainMenuActive = false; settingsActive = false; creditsActive = false;
            canvases[0].SetActive(false); canvases[1].SetActive(false); canvases[2].SetActive(true); canvases[3].SetActive(false); canvases[4].SetActive(false);
        }
        else if (settingsActive)
        {
            StartCoroutine(Settings());
            ///////////////////
            splashActive = false; mainMenuActive = false; playActive = false; creditsActive = false;
            canvases[0].SetActive(false); canvases[1].SetActive(false); canvases[2].SetActive(false); canvases[3].SetActive(true); canvases[4].SetActive(false);
        }
        else if (creditsActive)
        {
            StartCoroutine(Credits());
            ///////////////////
            splashActive = false; mainMenuActive = false; playActive = false; settingsActive = false;
            canvases[0].SetActive(false); canvases[1].SetActive(false); canvases[2].SetActive(false); canvases[3].SetActive(false); canvases[4].SetActive(true);

            creditsText.GetComponent<Animator>().SetBool("Show", true);
        }

        //BACK FROM ANY CANVAS EXCEPT FOR CHAR/R SELECT
        if (!canvases[2].activeSelf && Input.GetButtonDown("Back_" + GameManager.m_Instance.m_Player1.m_Controller) && canBack)
            Back();
    }
    void SelectedButtonOutline()
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            if (allButtons[i].activeInHierarchy)
            {
                if (es.currentSelectedGameObject == allButtons[i])
                {
                    allButtons[i].GetComponent<Outline>().enabled = true;
                    allButtons[i].GetComponent<Animator>().SetBool("Selected", true);
                }
                else
                {
                    allButtons[i].GetComponent<Outline>().enabled = false;
                    allButtons[i].GetComponent<Animator>().SetBool("Selected", false);
                }
            }
        }
    }

    //Main Functions for setting all the bools
    IEnumerator Splash()
    {
        //Setting Animator bools && ES.current.selected
        yield return null; es.SetSelectedGameObject(null); es.enabled = false; es.enabled = true; es.SetSelectedGameObject(firstSelectedButtons[0]); es.firstSelectedGameObject = firstSelectedButtons[0];
        anim.SetBool("Play", false); anim.SetBool("Settings", false); anim.SetBool("Credits", false);

        //Setting ****Active bool to false to prevent multiple function runs
        splashActive = false;
    }
    IEnumerator MainMenu()
    {
        //Setting Animator bools && ES.current.selected
        yield return null; es.SetSelectedGameObject(null); es.enabled = false; es.enabled = true; es.SetSelectedGameObject(firstSelectedButtons[1]); es.firstSelectedGameObject = firstSelectedButtons[1];
        anim.SetBool("Play", false); anim.SetBool("Settings", false); anim.SetBool("Credits", false);

        //Setting ****Active bool to false to prevent multiple function runs
        mainMenuActive = false;
    }
    IEnumerator Play()
    {
        //Setting Animator bools
        anim.SetBool("Play", true); anim.SetBool("Settings", false); anim.SetBool("Credits", false);

        //Setting ****Active bool to false to prevent multiple function runs
        playActive = false;
        canBack = true;
        yield return null;
    }
    IEnumerator Settings()
    {
        //Setting Animator bools
        anim.SetBool("Play", false); anim.SetBool("Settings", true); anim.SetBool("Credits", false);

        //Setting ****Active bool to false to prevent multiple function runs
        settingsActive = false;
        canBack = true;

        yield return null; es.SetSelectedGameObject(null); es.enabled = false; es.enabled = true; es.SetSelectedGameObject(firstSelectedButtons[2]); es.firstSelectedGameObject = firstSelectedButtons[2];
    }
    IEnumerator Credits()
    {
        //Setting Animator bools
        anim.SetBool("Play", false); anim.SetBool("Settings", false); anim.SetBool("Credits", true);

        //Setting ****Active bool to false to prevent multiple function runs
        creditsActive = false;
        canBack = true;
        yield return null;
    }

    //BUTTON FUNCTIONS
    public void SplashButton()
    {
        mainMenuActive = true;
        StandaloneInputModule inputModule = es.gameObject.GetComponent<StandaloneInputModule>();
        if (GameManager.m_Instance.m_NumOfPlayers <= 4)
        {
            //P2-4 Join
            if (canvases[0].activeSelf && Input.GetButtonDown("Jump_P1"))
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                characterSelect.AssignController(Player.Controller.P1);
                Debug.Log("Player " + players + " has Joined!");
                inputModule.submitButton = "Jump_P1";
                inputModule.horizontalAxis = "Horizontal_P1";
                inputModule.verticalAxis = "Vertical_P1";
                characterSelect.P1Join = true;
                characterSelect.canLockIn_P1 = true;
                characterSelect.firstPlayer = CharacterSelect.PlayerOne.P1;
            }
            if (canvases[0].activeSelf && Input.GetButtonDown("Jump_P2"))
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                characterSelect.AssignController(Player.Controller.P2);
                Debug.Log("Player " + players + " has Joined!");
                inputModule.submitButton = "Jump_P2";
                inputModule.horizontalAxis = "Horizontal_P2";
                inputModule.verticalAxis = "Vertical_P2";
                characterSelect.P2Join = true;
                characterSelect.canLockIn_P2 = true;
                characterSelect.firstPlayer = CharacterSelect.PlayerOne.P2;
            }
            if (canvases[0].activeSelf && Input.GetButtonDown("Jump_P3"))
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                characterSelect.AssignController(Player.Controller.P3);
                Debug.Log("Player " + players + " has Joined!");
                inputModule.submitButton = "Jump_P3";
                inputModule.horizontalAxis = "Horizontal_P3";
                inputModule.verticalAxis = "Vertical_P3";
                characterSelect.P3Join = true;
                characterSelect.canLockIn_P3 = true;
                characterSelect.firstPlayer = CharacterSelect.PlayerOne.P3;
            }
            if (canvases[0].activeSelf && Input.GetButtonDown("Jump_P4"))
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                characterSelect.AssignController(Player.Controller.P4);
                Debug.Log("Player " + players + " has Joined!");
                inputModule.submitButton = "Jump_P4";
                inputModule.horizontalAxis = "Horizontal_P4";
                inputModule.verticalAxis = "Vertical_P4";
                characterSelect.P4Join = true;
                characterSelect.canLockIn_P4 = true;
                characterSelect.firstPlayer = CharacterSelect.PlayerOne.P4;
            }

            if (canvases[0].activeSelf && Input.GetButtonDown("Submit_Keyboard"))
            {
                int players = ++GameManager.m_Instance.m_NumOfPlayers;
                characterSelect.AssignController(Player.Controller.Keyboard);
                inputModule.submitButton = "Submit_Keyboard";
                inputModule.horizontalAxis = "Horizontal_Keyboard";
                inputModule.verticalAxis = "Vertical_Keyboard";
                characterSelect.KeyboardJoin = true;
                characterSelect.canLockIn_Keyboard = true;
                characterSelect.firstPlayer = CharacterSelect.PlayerOne.Keyboard;
            }
        }
    }
    public void PlayButton()
    {
        playActive = true;
        characterSelect.AS.PlayOneShot(characterSelect.SFX[2]);
    }
    public void SettingsButton()
    {
        settingsActive = true;
        characterSelect.AS.PlayOneShot(characterSelect.SFX[2]);
    }
    public void CreditsButton()
    {
        creditsActive = true;
        characterSelect.AS.PlayOneShot(characterSelect.SFX[2]);
    }
    public void Back()
    {
        mainMenuActive = true;

        GameManager.m_Instance.m_NumOfPlayers = 1;

        characterSelect.AS.PlayOneShot(characterSelect.SFX[3]);

        switch (characterSelect.firstPlayer)
        {
            case CharacterSelect.PlayerOne.P1:
                characterSelect.P2Join = false;
                characterSelect.P3Join = false;
                characterSelect.P4Join = false;
                characterSelect.KeyboardJoin = false;
                break;
            case CharacterSelect.PlayerOne.P2:
                characterSelect.P1Join = false;
                characterSelect.P3Join = false;
                characterSelect.P4Join = false;
                characterSelect.KeyboardJoin = false;
                break;
            case CharacterSelect.PlayerOne.P3:
                characterSelect.P1Join = false;
                characterSelect.P2Join = false;
                characterSelect.P4Join = false;
                characterSelect.KeyboardJoin = false;
                break;
            case CharacterSelect.PlayerOne.P4:
                characterSelect.P1Join = false;
                characterSelect.P2Join = false;
                characterSelect.P3Join = false;
                characterSelect.KeyboardJoin = false;
                break;
            case CharacterSelect.PlayerOne.Keyboard:
                characterSelect.P1Join = false;
                characterSelect.P2Join = false;
                characterSelect.P3Join = false;
                characterSelect.P4Join = false;
                break;
        }

        //Set characterSelectIcon to default\
        characterSelect.P1.characterSelectIcon.texture = characterSelect.emptyTexture;
        characterSelect.P2.characterSelectIcon.texture = characterSelect.emptyTexture;
        characterSelect.P3.characterSelectIcon.texture = characterSelect.emptyTexture;
        characterSelect.P4.characterSelectIcon.texture = characterSelect.emptyTexture;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
