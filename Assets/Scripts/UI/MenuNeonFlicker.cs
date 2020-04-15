using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuNeonFlicker : MonoBehaviour
{
    public Image[] neonFrame = new Image[4];
    public Text[] playerNumber = new Text[4];
    public Text[] characterName = new Text[4];

    public float timeDelay = 2.0f;
    float timestamp;

    bool isOn;
    Color32 on = new Color32(255, 255, 255, 255);
    Color32 flicker = new Color32(245, 245, 245, 255);
    Color32 off = new Color32(40, 40, 40, 255);

    float firstDelay = 0.1f;
    float seondDelay = 0.1f;
    float thirdDelay = 0.1f;

    float animatorDisableDelayP1 = 1.6f;
    float animatorDisableDelayP2 = 1.6f;
    float animatorDisableDelayP3 = 1.6f;
    float animatorDisableDelayP4 = 1.6f;

    bool test;

    CharacterSelect CS;

    public enum NeonFrame
    {
        OFF,
        Turning,
        Flicker
    }

    public NeonFrame[] m_NeonFrames = new NeonFrame[4];
    void Awake()
    {
        neonFrame[0] = GameObject.Find("Character Select Plate P1/Neon Frame").GetComponent<Image>();
        neonFrame[1] = GameObject.Find("Character Select Plate P2/Neon Frame").GetComponent<Image>();
        neonFrame[2] = GameObject.Find("Character Select Plate P3/Neon Frame").GetComponent<Image>();
        neonFrame[3] = GameObject.Find("Character Select Plate P4/Neon Frame").GetComponent<Image>();

        playerNumber[0] = GameObject.Find("Character Select Plate P1/Player Number").GetComponent<Text>();
        playerNumber[1] = GameObject.Find("Character Select Plate P2/Player Number").GetComponent<Text>();
        playerNumber[2] = GameObject.Find("Character Select Plate P3/Player Number").GetComponent<Text>();
        playerNumber[3] = GameObject.Find("Character Select Plate P4/Player Number").GetComponent<Text>();

        characterName[0] = GameObject.Find("Character Select Plate P1/Character Name").GetComponent<Text>();
        characterName[1] = GameObject.Find("Character Select Plate P2/Character Name").GetComponent<Text>();
        characterName[2] = GameObject.Find("Character Select Plate P3/Character Name").GetComponent<Text>();
        characterName[3] = GameObject.Find("Character Select Plate P4/Character Name").GetComponent<Text>();

        CS = GetComponent<CharacterSelect>();
    }

    //START() + FLICKER() = Flickering effect as long as Animator is disabled on Frames
    //void Start()
    //{
    //    InvokeRepeating("Flicker", 2.0f, 0.1f);
    //}
    //void Flicker()
    //{
    //    if (!isOn)
    //    {
    //        neonFrame[0].color = flicker;
    //        neonFrame[1].color = flicker;
    //        neonFrame[2].color = flicker;
    //        neonFrame[3].color = flicker;
    //        isOn = true;
    //    }
    //    else
    //    {
    //        neonFrame[0].color = on;
    //        neonFrame[1].color = on;
    //        neonFrame[2].color = on;
    //        neonFrame[3].color = on;
    //        isOn = false;
    //    }
    //}

    void SetStateForNeonFrame()
    {
        //CONTROLLER CHECK & ASSIGN (IF - ON / ELSE - OFF)

        if(CS.firstPlayer != CharacterSelect.PlayerOne.nullType)
        {
            m_NeonFrames[0] = NeonFrame.Turning;
        }
        else
            m_NeonFrames[0] = NeonFrame.OFF;

        if (CS.secondPlayer != CharacterSelect.PlayerTwo.nullType)
        {
            m_NeonFrames[1] = NeonFrame.Turning;
        }
        else
            m_NeonFrames[1] = NeonFrame.OFF;

        if (CS.thirdPlayer != CharacterSelect.PlayerThree.nullType)
        {
            m_NeonFrames[2] = NeonFrame.Turning;
        }
        else
            m_NeonFrames[2] = NeonFrame.OFF;

        if (CS.fourthPlayer != CharacterSelect.PlayerFour.nullType)
        {
            m_NeonFrames[3] = NeonFrame.Turning;
        }
        else
            m_NeonFrames[3] = NeonFrame.OFF;


        //KEYBOARD CHECK & ASSIGN (IF - ON / ELSE - OFF)
        //if (CS.KeyboardJoin)
        //{
        //    if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[0] = NeonFrame.Turning;

        //    if (GameManager.m_Instance.m_Player2.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[1] = NeonFrame.Turning;

        //    if (GameManager.m_Instance.m_Player3.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[2] = NeonFrame.Turning;

        //    if (GameManager.m_Instance.m_Player4.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[3] = NeonFrame.Turning;
        //}
        //else
        //{
        //    if (GameManager.m_Instance.m_Player1.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[0] = NeonFrame.OFF;

        //    if (GameManager.m_Instance.m_Player2.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[1] = NeonFrame.OFF;

        //    if (GameManager.m_Instance.m_Player3.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[2] = NeonFrame.OFF;

        //    if (GameManager.m_Instance.m_Player4.m_Controller == Player.Controller.Keyboard)
        //        m_NeonFrames[3] = NeonFrame.OFF;
        //}
    }

    void Update()
    {
        SetStateForNeonFrame();

        for (int i = 0; i < m_NeonFrames.Length; i++)
        {
            switch (m_NeonFrames[i])
            {
                case NeonFrame.OFF:
                    neonFrame[i].GetComponent<Animator>().SetBool("Turning", false);
                    neonFrame[i].GetComponent<Animator>().enabled = false;
                    neonFrame[i].color = off;
                    //Text & Outline
                    playerNumber[i].color = off;
                    playerNumber[i].GetComponent<Outline>().enabled = false;
                    characterName[i].GetComponent<Outline>().enabled = false;
                    characterName[i].color = off;
                    break;
                case NeonFrame.Turning:
                    neonFrame[i].GetComponent<Animator>().enabled = true;
                    neonFrame[i].GetComponent<Animator>().SetBool("Turning", true);
                    //Text & Outline
                    playerNumber[i].color = on;
                    playerNumber[i].GetComponent<Outline>().enabled = true;
                    characterName[i].GetComponent<Outline>().enabled = true;
                    characterName[i].color = on;
                    break;
                case NeonFrame.Flicker:
                    //FLICKERING
                    break;
            }
        }
    }
}
