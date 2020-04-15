using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PartyBar : MonoBehaviour
{

    public float m_Max = 100.0f;
    public float m_Current = 0.0f;
    public float m_DecreaseRateDungeon = 5.0f;
    public float m_DecreaseAmountDungeon = 5.0f;
    public float m_DecreaseRateMinigame = 1.0f;
    public float m_DecreaseAmountMinigame = 3.3333f;
    public float m_fillSpeed = 2.0f;

    public bool m_Active;

    private Image m_Bar;
    public Animator partybarLogo;

    float m_TempTimer;

    //Boss object variable
    GameObject m_Boss = null;

    // Use this for initialization
    void Start()
    {
        m_Bar = GetComponent<Image>();
        partybarLogo = transform.parent.GetComponentInChildren<Animator>();

        if (GameManager.m_Instance.m_GameState == GameManager.GameState.Dungeon)
        {
            m_Current = 0.0f;
        }
        else if (GameManager.m_Instance.m_GameState == GameManager.GameState.Minigame)
        {
            m_Current = 100.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "KaminsBoss")
        {
            bossPartyBarDrain();
        }
        else
        {
            if (GameManager.m_Instance.m_GameState != GameManager.GameState.Minigame)
            {
                dungeonPartyBarDrain();
            }
            else if (GameManager.m_Instance.m_GameState == GameManager.GameState.Minigame)
            {
                minigamePartyBarDrain();
            }
        }
    }

    void bossPartyBarDrain()
    {
        m_Boss = GameObject.Find("Boss");
        if (m_Boss != null)
        {
            AdvancedBossAi bossScript = m_Boss.GetComponent<AdvancedBossAi>();
            EnemyHealth bossHealth = m_Boss.GetComponent<EnemyHealth>();
            m_Bar.fillAmount = Mathf.Lerp(m_Bar.fillAmount, bossHealth.m_EnemyHealth / (bossScript.m_BaseMaxHealth * bossScript.m_NumOfPlayersHealthMultiplier),
                 m_fillSpeed * Time.deltaTime);
        }
        else
        {
            m_Boss = GameObject.Find("Boss");
        }

    }
    void dungeonPartyBarDrain()
    {
        //set bar equal to percentage
        m_Bar.fillAmount = Mathf.Lerp(m_Bar.fillAmount, m_Current / m_Max, m_fillSpeed * Time.deltaTime);

        if (m_Active)
        {

            if (m_TempTimer <= Time.time - m_DecreaseRateDungeon)
            {
                if (m_Current >= m_DecreaseAmountDungeon)
                {
                    m_Current -= m_DecreaseAmountDungeon;
                    partybarLogo.SetBool("Drain", true);
                }
                else
                {
                    m_Current = 0.0f;
                }
                m_TempTimer = Time.time;
            }

            //if bar hits 0 load minigame
            if (m_Current >= m_Max)
            {
                loadMinigame();
            }
        }
    }

    void minigamePartyBarDrain()
    {
        m_Bar.fillAmount = Mathf.Lerp(m_Bar.fillAmount, (float)m_Current / m_Max, m_fillSpeed * Time.deltaTime);

        if (m_Active)
        {
            //set bar equal to percentage

            if (m_Current > 0.0f)
            {
                if (m_TempTimer <= Time.time - m_DecreaseRateMinigame)
                {
                    m_Current -= m_DecreaseAmountMinigame;
                    m_TempTimer = Time.time;
                }
            }
            //            //if bar hits 0 load minigame
            //            else if(!m_MiniGameStatusChanged)
            //            {
            //                MinigameManager minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
            //                minigameManager.UpdateMinigameState();
            //                m_MiniGameStatusChanged = true;
            ////          Edit ==> Minigame is being managed by the MinigameManager

            //                //                RewardsAndLoadBackToGame();
            //            }
        }
    }

    void loadMinigame()
    {
        GameManager.m_Instance.m_GameState = GameManager.GameState.Minigame;
        //int randomNumber = Random.Range(1, 3);

        GameManager.m_Instance.savePlayers();

        /*if( GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_01 ||
            GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_02 ||
            GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_03)
        {
            ++GameManager.m_Instance.m_Tutorial;
        }*/
        GameManager.m_Instance.m_LastLevelPlayedIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(Random.Range(6, 8));
    }

    //void RewardsAndLoadBackToGame()
    //{
    //    MiniGameRewards minigameReward = GameObject.Find("MinigameManager").GetComponent<MiniGameRewards>();
    //    MinigameManager miniGameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();

    //    //int randomNumber = Random.Range(1, 3);

    //    miniGameManager.endMinigame();

    //    if (GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_01 ||
    //GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_02 ||
    //GameManager.m_Instance.m_Tutorial == GameManager.Tutorial.Lobby_03)
    //    {
    //        //SceneManager.LoadScene(GameManager.m_Instance.m_Tutorial.ToString()); //ballroom blitz

    //        //Reward time
    //        if (miniGameManager.bossNo)
    //        {
    //            GameManager.m_Instance.m_GameState = GameManager.GameState.Dungeon;
    //            SceneManager.LoadScene(GameManager.m_Instance.m_Tutorial.ToString());
    //        }
    //        else if (miniGameManager.bossYes)
    //        {
    //            GameManager.m_Instance.m_GameState = GameManager.GameState.Boss;
    //            SceneManager.LoadScene("BossRoom");
    //        }
    //    }
    //    else
    //    {
    //        if (miniGameManager.bossNo)
    //        {
    //            GameManager.m_Instance.m_GameState = GameManager.GameState.Dungeon;
    //            SceneManager.LoadScene(Random.Range(8, 10));
    //        }
    //        else if (miniGameManager.bossYes)
    //        {
    //            GameManager.m_Instance.m_GameState = GameManager.GameState.Boss;
    //            SceneManager.LoadScene("BossRoom");
    //        }
    //    }
    //}
}
