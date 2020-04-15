using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class HUDManager : MonoBehaviour
{
    [Header ("HUD Panels")]
    public GameObject P1_Panel, P2_Panel, P3_Panel, P4_Panel;
    [Header("Panel Transforms")]
    public RectTransform P1_PanelTransform, P2_PanelTransform, P3_PanelTransform, P4_PanelTransform;
    [Header("Score Counters")]
    public Text P1_ScoreCounter, P2_ScoreCounter, P3_ScoreCounter, P4_ScoreCounter;
    [Header("Player Portrait Images")]
    public Image[] playerPortraitImages = new Image[4];
    [Header("Portrait Sprites")]
    public Sprite[] portraitSprites = new Sprite[4];

    void Start()
    {
        if (GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P1_Panel") != null)
        {
            P1_Panel = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P1_Panel");
            P2_Panel = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P2_Panel");
            P3_Panel = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P3_Panel");
            P4_Panel = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P4_Panel");

            P1_PanelTransform = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P1_Panel").GetComponent<RectTransform>();
            P2_PanelTransform = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P2_Panel").GetComponent<RectTransform>();
            P3_PanelTransform = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P3_Panel").GetComponent<RectTransform>();
            P4_PanelTransform = GameObject.Find(GameManager.m_Instance.m_GameState.ToString() + "Canvas/P4_Panel").GetComponent<RectTransform>();

            P1_ScoreCounter = GameObject.Find("P1_Panel/Score").GetComponent<Text>();
            P2_ScoreCounter = GameObject.Find("P2_Panel/Score").GetComponent<Text>();
            P3_ScoreCounter = GameObject.Find("P3_Panel/Score").GetComponent<Text>();
            P4_ScoreCounter = GameObject.Find("P4_Panel/Score").GetComponent<Text>();

            playerPortraitImages[0] = GameObject.Find("P1_Panel/Portrait BG/Portrait Image").GetComponent<Image>();
            playerPortraitImages[1] = GameObject.Find("P2_Panel/Portrait BG/Portrait Image").GetComponent<Image>();
            playerPortraitImages[2] = GameObject.Find("P3_Panel/Portrait BG/Portrait Image").GetComponent<Image>();
            playerPortraitImages[3] = GameObject.Find("P4_Panel/Portrait BG/Portrait Image").GetComponent<Image>();
        }

        UpdateUIPanels();
        UpdatePortraitImages();
    }

    void Update()
    {
        UpdateScoreCounters();
    }

    void UpdateUIPanels()
    {
        if (GameManager.m_Instance.m_Players.Length == 1)
        {
            P2_Panel.SetActive(false);
            P3_Panel.SetActive(false);
            P4_Panel.SetActive(false);

            P1_PanelTransform.anchoredPosition = new Vector2(0, 20);
        }
        if (GameManager.m_Instance.m_Players.Length == 2)
        {
            P3_Panel.SetActive(false);
            P4_Panel.SetActive(false);

            P1_PanelTransform.anchoredPosition = new Vector2(-67, 20);
            P2_PanelTransform.anchoredPosition = new Vector2(67, 20);
        }
        if (GameManager.m_Instance.m_Players.Length == 3)
        {
            P4_Panel.SetActive(false);

            P1_PanelTransform.anchoredPosition = new Vector2(-135, 20);
            P2_PanelTransform.anchoredPosition = new Vector2(0, 20);
            P3_PanelTransform.anchoredPosition = new Vector2(135, 20);
        }
        if (GameManager.m_Instance.m_Players.Length == 4)
        {
            return;
        }
    }

    void UpdatePortraitImages()
    {
        //P1 PORTRAIT
        switch(GameManager.m_Instance.m_Player1.model)
        {
            case Player.Model.Mascot:
                playerPortraitImages[0].sprite = portraitSprites[0];
                break;
            case Player.Model.Nerd:
                playerPortraitImages[0].sprite = portraitSprites[1];
                break;
            case Player.Model.Badboy:
                playerPortraitImages[0].sprite = portraitSprites[2];
                break;
            case Player.Model.Goth:
                playerPortraitImages[0].sprite = portraitSprites[3];
                break;
        }
        //P2 PORTRAIT
        switch (GameManager.m_Instance.m_Player2.model)
        {
            case Player.Model.Mascot:
                playerPortraitImages[1].sprite = portraitSprites[0];
                break;
            case Player.Model.Nerd:
                playerPortraitImages[1].sprite = portraitSprites[1];
                break;
            case Player.Model.Badboy:
                playerPortraitImages[1].sprite = portraitSprites[2];
                break;
            case Player.Model.Goth:
                playerPortraitImages[1].sprite = portraitSprites[3];
                break;
        }
        //P3 PORTRAIT
        switch (GameManager.m_Instance.m_Player3.model)
        {
            case Player.Model.Mascot:
                playerPortraitImages[2].sprite = portraitSprites[0];
                break;
            case Player.Model.Nerd:
                playerPortraitImages[2].sprite = portraitSprites[1];
                break;
            case Player.Model.Badboy:
                playerPortraitImages[2].sprite = portraitSprites[2];
                break;
            case Player.Model.Goth:
                playerPortraitImages[2].sprite = portraitSprites[3];
                break;
        }
        //P4 PORTRAIT
        switch (GameManager.m_Instance.m_Player4.model)
        {
            case Player.Model.Mascot:
                playerPortraitImages[3].sprite = portraitSprites[0];
                break;
            case Player.Model.Nerd:
                playerPortraitImages[3].sprite = portraitSprites[1];
                break;
            case Player.Model.Badboy:
                playerPortraitImages[3].sprite = portraitSprites[2];
                break;
            case Player.Model.Goth:
                playerPortraitImages[3].sprite = portraitSprites[3];
                break;
        }
    }

    void UpdateScoreCounters()
    {
        P1_ScoreCounter.text = GameManager.m_Instance.m_Players[0].GetComponent<Player>().m_Score.ToString();
        if (GameManager.m_Instance.m_NumOfPlayers >= 2)
            P2_ScoreCounter.text = GameManager.m_Instance.m_Players[1].GetComponent<Player>().m_Score.ToString();
        if (GameManager.m_Instance.m_NumOfPlayers >= 3)
            P3_ScoreCounter.text = GameManager.m_Instance.m_Players[2].GetComponent<Player>().m_Score.ToString();
        if (GameManager.m_Instance.m_NumOfPlayers >= 4)
            P4_ScoreCounter.text = GameManager.m_Instance.m_Players[3].GetComponent<Player>().m_Score.ToString();
    }
}
