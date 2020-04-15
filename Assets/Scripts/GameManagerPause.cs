using UnityEngine;
using System.Collections;

public class GameManagerPause : MonoBehaviour {

    private bool paused;

    public bool Paused
    {
        get { return paused;}
    }

    private static GameManagerPause instance;

    public static GameManagerPause Instance
    {
        get {
            if (instance == null)
            {

                instance = GameObject.FindObjectOfType<GameManagerPause>();
            }
            return GameManagerPause.instance;
        }
        

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
	
	}

    public void PauseGame()
    {
        paused = !paused;
    }
}



/* Code that checks if game is paused and make sure to pause games functionality 
if(!GameManagerPaused.Instance.Paused)
    {
    transform.Translate(dir * (speed * Time.deltaTime)); 
    }
    */