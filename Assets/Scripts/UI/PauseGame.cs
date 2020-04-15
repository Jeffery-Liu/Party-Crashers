using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PauseGame : MonoBehaviour
{
    EventSystem es;

    [Header("Different 'First Selected' Buttons")]
    public GameObject[] firstSelectedButtons;

    [Header("Different Canvases")]
    public GameObject[] canvases;

    [Header("Bools")]
    public bool pauseActive, optionsActive, controlsActive, quitActive;

    [Header("List of ALL Buttons")]
    public GameObject[] allButtons;

    [Header("'Back' Button Available")]
    public bool canBack;

    public bool openedP1, openedP2, openedP3, openedP4, openedKeyboard;

    void Awake()
    {
        if (GameObject.Find("Pause Menu/EventSystem") != null)
            es = GameObject.Find("Pause Menu/EventSystem").GetComponent<EventSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if (pauseActive)
            Pause();
        else if (optionsActive)
            Options();
        else if (controlsActive)
            Controls();
        else if (quitActive)
            Quit();

        if (canBack && openedP1 && !openedP2 && !openedP3 && !openedP4 && !openedKeyboard && Input.GetButtonDown("Back_" + GameManager.m_Instance.m_Player1.m_Controller)) //PRESS FOR BACK BUTTON
            BackButton();

        if (canBack && !openedP1 && openedP2 && !openedP3 && !openedP4 && !openedKeyboard && Input.GetButtonDown("Back_" + GameManager.m_Instance.m_Player2.m_Controller)) //PRESS FOR BACK BUTTON
            BackButton();

        if (canBack && !openedP1 && !openedP2 && openedP3 && !openedP4 && !openedKeyboard && Input.GetButtonDown("Back_" + GameManager.m_Instance.m_Player3.m_Controller)) //PRESS FOR BACK BUTTON
            BackButton();

        if (canBack && !openedP1 && !openedP2 && !openedP3 && openedP4 && !openedKeyboard && Input.GetButtonDown("Back_" + GameManager.m_Instance.m_Player4.m_Controller)) //PRESS FOR BACK BUTTON
            BackButton();

        if (canBack && !openedP1 && !openedP2 && !openedP3 && !openedP4 && openedKeyboard && (Input.GetKeyDown(KeyCode.Escape))) //PRESS FOR BACK BUTTON
            BackButton();

        //PRESS START P1-4 + KEYBOARD
        
        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.P1 || GameManager.m_Instance.m_Player2.m_Controller == Player.Controller.P1 ||
            GameManager.m_Instance.m_Player3.m_Controller == Player.Controller.P1 || GameManager.m_Instance.m_Player4.m_Controller == Player.Controller.P1)
        {
            if (Input.GetButtonDown("Pause_" + GameManager.m_Instance.m_Player1.m_Controller) && !pauseActive && !openedP2 && !openedP3 && !openedP4 && !openedKeyboard)
            {
                openedP1 = true;
                PauseMenu();
            }
        }
        
        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.P2 || GameManager.m_Instance.m_Player2.m_Controller == Player.Controller.P2 ||
    GameManager.m_Instance.m_Player3.m_Controller == Player.Controller.P2 || GameManager.m_Instance.m_Player4.m_Controller == Player.Controller.P2)
        {
            if (Input.GetButtonDown("Pause_" + GameManager.m_Instance.m_Player2.m_Controller) && !pauseActive && !openedP1 && !openedP3 && !openedP4 && !openedKeyboard)
            {
                openedP2 = true;
                PauseMenu();
            }
        }
        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.P3 || GameManager.m_Instance.m_Player2.m_Controller == Player.Controller.P3 ||
    GameManager.m_Instance.m_Player3.m_Controller == Player.Controller.P3 || GameManager.m_Instance.m_Player4.m_Controller == Player.Controller.P3)
        {
            if (Input.GetButtonDown("Pause_" + GameManager.m_Instance.m_Player3.m_Controller) && !pauseActive && !openedP2 && !openedP1 && !openedP4 && !openedKeyboard)
            {
                openedP3 = true;
                PauseMenu();
            }
        }
        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.P4 || GameManager.m_Instance.m_Player2.m_Controller == Player.Controller.P4 ||
GameManager.m_Instance.m_Player3.m_Controller == Player.Controller.P4 || GameManager.m_Instance.m_Player4.m_Controller == Player.Controller.P4)
        {
            if (Input.GetButtonDown("Pause_" + GameManager.m_Instance.m_Player4.m_Controller) && !pauseActive && !openedP2 && !openedP3 && !openedP1 && !openedKeyboard)
            {
                openedP4 = true;
                PauseMenu();
            }
        }
        
  
        if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard || GameManager.m_Instance.m_Player2.m_Controller == Player.Controller.Keyboard ||
GameManager.m_Instance.m_Player3.m_Controller == Player.Controller.Keyboard || GameManager.m_Instance.m_Player4.m_Controller == Player.Controller.Keyboard)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !pauseActive && !openedP2 && !openedP3 && !openedP4 && !openedP1)//Change to asdasdas
            {
                openedKeyboard = true;
                PauseMenu();
            }
        }

        if (pauseActive)
            canBack = false;

    }

    void Pause()
    {
        pauseActive = false;
        es.SetSelectedGameObject(null); es.enabled = false; es.enabled = true; es.SetSelectedGameObject(firstSelectedButtons[0]); es.firstSelectedGameObject = firstSelectedButtons[0];

        canvases[0].SetActive(true);
        canvases[1].SetActive(false);
        canvases[2].SetActive(false);
        canvases[3].SetActive(false);

    }
    void Options()
    {
        es.SetSelectedGameObject(null); es.enabled = false; es.enabled = true; es.SetSelectedGameObject(firstSelectedButtons[1]); es.firstSelectedGameObject = firstSelectedButtons[1];
        optionsActive = false;
        canvases[0].SetActive(false);
        canvases[1].SetActive(true);
        canvases[2].SetActive(false);
        canvases[3].SetActive(false);

        canBack = true;

    }
    void Controls()
    {
        es.SetSelectedGameObject(null); es.enabled = false; es.enabled = true; es.SetSelectedGameObject(firstSelectedButtons[2]); es.firstSelectedGameObject = firstSelectedButtons[2];
        controlsActive = false;
        canvases[0].SetActive(false);
        canvases[1].SetActive(false);
        canvases[2].SetActive(true);
        canvases[3].SetActive(false);

        canBack = true;

    }
    void Quit()
    {
        Time.timeScale = 0;
        es.SetSelectedGameObject(null); es.enabled = false; es.enabled = true; es.SetSelectedGameObject(firstSelectedButtons[3]); es.firstSelectedGameObject = firstSelectedButtons[3];
        quitActive = false;
        canvases[0].SetActive(false);
        canvases[1].SetActive(false);
        canvases[2].SetActive(false);
        canvases[3].SetActive(true);

        canBack = true;

    }

    public void PauseMenu()
    {

        for (int i = 0; i < GameManager.m_Instance.m_NumOfPlayers; i++)
        {
            GameManager.m_Instance.m_Players[i].GetComponent<PlayerController>().m_CantMove = true;
        }
        Time.timeScale = 0;
        canvases[0].SetActive(true);
        StandaloneInputModule inputModule = es.gameObject.GetComponent<StandaloneInputModule>();

        if (openedP1)
        {
            pauseActive = true;
            inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player1.m_Controller;
            inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller;
            inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player1.m_Controller;
        }
        if (openedP2)
        {
            pauseActive = true;
            inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player2.m_Controller;
            inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller;
            inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player2.m_Controller;
        }

        if (openedP3)
        {
            pauseActive = true;
            inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player3.m_Controller;
            inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller;
            inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player3.m_Controller;
        }

        if (openedP4)
        {
            pauseActive = true;
            inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player4.m_Controller;
            inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller;
            inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player4.m_Controller;
        }

        if (openedKeyboard)
        {
            pauseActive = true;
            //Input.GetButtonDown("Back_Keyboard") _ INPUT
            inputModule.submitButton = "Submit_Keyboard";
            inputModule.horizontalAxis = "Horizontal_Keyboard";
            inputModule.verticalAxis = "Vertical_Keyboard";
        }

    }

    public void OptionsMenu()
    {
        Time.timeScale = 0;
        canvases[1].SetActive(true);

        StandaloneInputModule inputModule = es.gameObject.GetComponent<StandaloneInputModule>();
        if (canvases[1].activeSelf)
        {
            if (openedP1)
            {
                optionsActive = true;
                inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player1.m_Controller;
                inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player1.m_Controller;
                inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player1.m_Controller;
            }
            if (openedP2)
            {
                optionsActive = true;
                inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player2.m_Controller;
                inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player2.m_Controller;
                inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player2.m_Controller;
            }

            if (openedP3)
            {
                optionsActive = true;
                inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player3.m_Controller;
                inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player3.m_Controller;
                inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player3.m_Controller;
            }

            if (openedP4)
            {
                optionsActive = true;
                inputModule.submitButton = "Jump_" + GameManager.m_Instance.m_Player4.m_Controller;
                inputModule.horizontalAxis = "Horizontal_" + GameManager.m_Instance.m_Player4.m_Controller;
                inputModule.verticalAxis = "Vertical_" + GameManager.m_Instance.m_Player4.m_Controller;
            }

            if (openedKeyboard)
            {
                optionsActive = true;
                //Input.GetButtonDown("Back_Keyboard") _ INPUT
                inputModule.submitButton = "Submit_Keyboard";
                inputModule.horizontalAxis = "Horizontal_Keyboard";
                inputModule.verticalAxis = "Vertical_Keyboard";
            }
        }
    }

    public void ResumeButton()
    {
        for (int i = 0; i < GameManager.m_Instance.m_NumOfPlayers; i++)//MIGHT REDO AS ARRAY SIZE IF CAUSES ERRORS
        {
            GameManager.m_Instance.m_Players[i].GetComponent<PlayerController>().m_CantMove = false;
        }

        Time.timeScale = 1;
        canvases[0].SetActive(false);
        openedP1 = false;
        openedP2 = false;
        openedP3 = false;
        openedP4 = false;
        openedKeyboard = false;
    }
    public void OptionsButton()
    {
        optionsActive = true;
    }
    public void ControlsButton()
    {
        controlsActive = true;
    }
    public void QuitButton()
    {
        quitActive = true;

    }
    public void YesButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void BackButton()
    {
        PauseMenu();
    }


}



