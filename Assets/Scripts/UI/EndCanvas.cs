using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndCanvas : MonoBehaviour
{
    public bool activated;
    public bool gameWon;
    public string winText;
    public string loseText;

    Canvas endCanvas;
    Button backToMainMenuButton;

    EventSystem ES;
    StandaloneInputModule SIM;

    void Awake()
    {
        endCanvas = GetComponent<Canvas>();
        backToMainMenuButton = transform.GetChild(1).GetComponent<Button>();

        ES = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        SIM = GameObject.Find("EventSystem").GetComponent<StandaloneInputModule>();

        if (ES == null)
            Debug.Log("YOU ARE MISSING EVENTSYSTEM GAMEOBJECT IN THIS SCENE; ADD IT");
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Tab))
        //    activated = true;

        if (activated)
        {
            endCanvas.enabled = true;
            foreach (GameObject player in GameManager.m_Instance.m_Players)
            {
                player.GetComponent<PlayerController>().m_CantMove = true;
            }

            //SIM.submitButton = ("Submit_" + GameManager.m_Instance.m_Player1.m_Controller);

            //ES.enabled = false;
            //ES.enabled = true;

            GetComponentInChildren<Button>().enabled = true;
            ES.SetSelectedGameObject(backToMainMenuButton.gameObject);

            if (gameWon)
                transform.GetChild(0).GetComponent<Text>().text = winText;
            else
                transform.GetChild(0).GetComponent<Text>().text = loseText;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
