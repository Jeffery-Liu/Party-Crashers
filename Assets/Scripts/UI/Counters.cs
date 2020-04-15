using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Counters : MonoBehaviour {

	public Text[] scoreCounter;
	public Text[] goldCounter;

	void Start() 
	{
        if (GameManager.m_Instance.m_Players.Length == 1)
        {
            //Score
            scoreCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Score").GetComponent<Text>();
            //Gold
            goldCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Gold").GetComponent<Text>();
        }
        if (GameManager.m_Instance.m_Players.Length == 2)
        {
            //Score
            scoreCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Score").GetComponent<Text>();
            scoreCounter[1] = GameObject.Find("Dungeon_HUD_Canvas/P2_Panel/P2 Score").GetComponent<Text>();

            //Gold
            goldCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Gold").GetComponent<Text>();
            goldCounter[1] = GameObject.Find("Dungeon_HUD_Canvas/P2_Panel/P2 Gold").GetComponent<Text>();
        }
        if (GameManager.m_Instance.m_Players.Length == 3)
        {
            //Score
            scoreCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Score").GetComponent<Text>();
            scoreCounter[1] = GameObject.Find("Dungeon_HUD_Canvas/P2_Panel/P2 Score").GetComponent<Text>();
            scoreCounter[2] = GameObject.Find("Dungeon_HUD_Canvas/P3_Panel/P3 Score").GetComponent<Text>();

            //Gold
            goldCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Gold").GetComponent<Text>();
            goldCounter[1] = GameObject.Find("Dungeon_HUD_Canvas/P2_Panel/P2 Gold").GetComponent<Text>();
            goldCounter[2] = GameObject.Find("Dungeon_HUD_Canvas/P3_Panel/P3 Gold").GetComponent<Text>();
        }
        if (GameManager.m_Instance.m_Players.Length == 4)
        {
            //Score
            scoreCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Score").GetComponent<Text>();
            scoreCounter[1] = GameObject.Find("Dungeon_HUD_Canvas/P2_Panel/P2 Score").GetComponent<Text>();
            scoreCounter[2] = GameObject.Find("Dungeon_HUD_Canvas/P3_Panel/P3 Score").GetComponent<Text>();
            scoreCounter[3] = GameObject.Find("Dungeon_HUD_Canvas/P4_Panel/P4 Score").GetComponent<Text>();

            //Gold
            goldCounter[0] = GameObject.Find("Dungeon_HUD_Canvas/P1_Panel/P1 Gold").GetComponent<Text>();
            goldCounter[1] = GameObject.Find("Dungeon_HUD_Canvas/P2_Panel/P2 Gold").GetComponent<Text>();
            goldCounter[2] = GameObject.Find("Dungeon_HUD_Canvas/P3_Panel/P3 Gold").GetComponent<Text>();
            goldCounter[3] = GameObject.Find("Dungeon_HUD_Canvas/P4_Panel/P4 Gold").GetComponent<Text>();
        }
    }

	void Update() 
	{

        for (int i = 0; i < GameManager.m_Instance.m_Players.Length; ++i)
        {
            Player player = GameManager.m_Instance.m_Players[i].GetComponent<Player>();
            switch (player.m_Player)
            {
                case Player.PLAYER.P1:
                    //P1
                    scoreCounter[0].text = "" + player.m_Score;
                    goldCounter[0].text = "" + player.m_Gold;
                    break;
                case Player.PLAYER.P2:
                    //P2
                    scoreCounter[1].text = "" + player.m_Score;
                    goldCounter[1].text = "" + player.m_Gold;
                    break;
                case Player.PLAYER.P3:
                    //P3
                    scoreCounter[2].text = "" + player.m_Score;
                    goldCounter[2].text = "" + player.m_Gold;
                    break;
                case Player.PLAYER.P4:
                    //P4
                    scoreCounter[3].text = "" + player.m_Score;
                    goldCounter[3].text = "" + player.m_Gold;
                    break;
            }
        }





        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.m_Instance.m_Player1.gold += 1000;
        }
    }
}
