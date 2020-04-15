using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public Text[] scoreCounter;
    public float score, p1, p2, p3, p4;

    void Start()
    {
        //Score
        scoreCounter[0] = GameObject.Find("Minigame_HUD_Canvas/P1_Panel/P1 Score").GetComponent<Text>();
        scoreCounter[1] = GameObject.Find("Minigame_HUD_Canvas/P2_Panel/P2 Score").GetComponent<Text>();
        scoreCounter[2] = GameObject.Find("Minigame_HUD_Canvas/P3_Panel/P3 Score").GetComponent<Text>();
        scoreCounter[3] = GameObject.Find("Minigame_HUD_Canvas/P4_Panel/P4 Score").GetComponent<Text>();
    }

    void Update()
    {
        // Score
        score = score + Time.deltaTime;


        //P1
        scoreCounter[0].text = string.Format("{0:0}", p1 + score);
        //P2
        scoreCounter[1].text = string.Format("{0:0}", p2 + score);
        //P3
        scoreCounter[2].text = string.Format("{0:0}", p3 + score);
        //P4
        scoreCounter[3].text = string.Format("{0:0}", p4 + score);
    }
}
