using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

    public Button m_PlayButton;
    public Button m_ExitButton;

	// Use this for initialization
	void Start () {
        m_PlayButton = m_PlayButton.GetComponent<Button>();
        m_ExitButton = m_ExitButton.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
