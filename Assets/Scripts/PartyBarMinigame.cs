using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PartyBarMinigame : MonoBehaviour
{
    /*
    //public int m_Max = 60;
    //public int m_Current = 60;
    //public float m_Cooldown = 1f;
    //public int m_ReduceBy = 1;
    //public float m_fillSpeed = 2f;

    //public bool m_Active = true;

    //Image m_Bar;

    //float m_TempTimer;
    // Use this for initialization
    void Start()
    {
        m_Bar = GetComponent<Image>();

        //Debug.Log(GameManager.m_Instance.m_Player2.lastLocation);
    }

    // Update is called once per frame
    void Update()
    {
        m_Bar.fillAmount = Mathf.Lerp(m_Bar.fillAmount, (float)m_Current / m_Max, m_fillSpeed * Time.deltaTime);

        if (m_Active)
        {
            //set bar equal to percentage

            if (m_TempTimer <= Time.time - m_DecreaseRate)
            {
                m_Current -= m_DecreaseAmount;
                m_TempTimer = Time.time;
            }

            //if bar hits 0 load minigame
            if (m_Current <= 0)
            {
                loadBackToGame();
            }
        }
    }

    void loadBackToGame()
    {
        //int randomNumber = Random.Range(1, 3);

        GameManager.m_Instance.savePlayers();
        SceneManager.LoadScene("Lobby_01"); //tutorial scene
    }*/
}