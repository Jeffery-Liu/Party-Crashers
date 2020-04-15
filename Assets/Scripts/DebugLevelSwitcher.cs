using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DebugLevelSwitcher : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Lobby_01");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Lobby_02");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Libraryroom");
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("DiningRoom");
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            SceneManager.LoadScene("BowlingRoomRedesign");
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            SceneManager.LoadScene("KaminsBoss");
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            SceneManager.LoadScene("BallroomBlitz");
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("BreakToTheBeat");
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            SceneManager.LoadScene("DanceFloorRumble");
        }
		else if (Input.GetKeyDown(KeyCode.F7))
		{
			SceneManager.LoadScene("DiningRoom02");
		}
    }

}
