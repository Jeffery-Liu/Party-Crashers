using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class RewardManager : MonoBehaviour {

    public GameObject[] m_Rewards;

    public static RewardManager m_Instance;

    public Button[] m_Buttons;

    public Text m_PickRewardText;

    public Player.PLAYER m_First;
    public Player.PLAYER m_Second;
    public Player.PLAYER m_Third;
    public Player.PLAYER m_Fourth;

    private int m_PickCounter = 0;

    // Use this for initialization
    void Awake()
    {
        if (m_Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            m_Instance = this;
            m_First = Player.PLAYER.P1;
            m_Second = Player.PLAYER.P2;
            m_Third = Player.PLAYER.P3;
            m_Fourth = Player.PLAYER.P4;

            m_PickRewardText.text = "Choose reward: " + m_First.ToString();

            LoadRewards();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadRewards()
    {
        for (int i = 0; i < m_Buttons.Length; ++i)
        {
            int randomNumber = Random.Range(0, m_Rewards.Length);
            Debug.Log(randomNumber);
            GameObject chosenReward = m_Rewards[randomNumber];
            Debug.Log(chosenReward.name);
            m_Buttons[i].GetComponentInChildren<Text>().text = chosenReward.name;

            m_Buttons[i].GetComponentInChildren<Reward>().m_Reward = chosenReward;
        }
    }

    public void ChooseRewards()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        GameObject chosenWeapon = EventSystem.current.currentSelectedGameObject.GetComponent<Reward>().m_Reward;
        WEAPONTYPE weaponID = chosenWeapon.GetComponent<WeaponID>().m_WeaponType;
        /*
        switch (m_PickCounter)
        {
            case 0:
                GameManager.m_Instance.m_Player1.weaponID = weaponID;
                Debug.Log("Player 1 weapon set as " + chosenWeapon.name);
                m_PickRewardText.text = "Choose reward: " + m_Second.ToString();
                break;
            case 1:
                GameManager.m_Instance.m_Player2.weaponID = weaponID;
                Debug.Log("Player 2 weapon set as " + chosenWeapon.name);
                m_PickRewardText.text = "Choose reward: " + m_Third.ToString();
                break;
            case 2:
                GameManager.m_Instance.m_Player3.weaponID = weaponID;
                Debug.Log("Player 3 weapon set as " + chosenWeapon.name);
                m_PickRewardText.text = "Choose reward: " + m_Fourth.ToString();
                break;
            case 3:
                Debug.Log("Player 4 weapon set as " + chosenWeapon.name);
                GameManager.m_Instance.m_Player4.weaponID = weaponID;

                PickNewLevel();
                Debug.Log("Rewards Given Out");
                break;
        }*/
        m_PickCounter++;
    }

    void PickNewLevel()
    {
        SceneManager.LoadScene(1);
    }
}
