using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CollectObjects : MonoBehaviour
{
    private Player player;
    public PartyBar partyBar;

    public GameObject scoreCounter;

    public float duration = 1f;

    private int m_PointsToCollect = 0;

    void Start()
    {
        scoreCounter = GameObject.Find("P1_Panel/Score");

        player = gameObject.GetComponent<Player>();
        if (GameManager.m_Instance.m_PartyBar != null)
        {
            partyBar = GameManager.m_Instance.m_PartyBar;
        }
        else
        {
            Debug.LogError("Partybar Not Found");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Collectible collectible = other.GetComponent<Collectible>();

        if (collectible != null)
        {
            if (GameManager.m_Instance.m_GameState == GameManager.GameState.Dungeon && player.m_State == Player.State.Alive)
            {
                //player.m_Gold += collectible.gold;
                //player.m_Score += 100;


                StopCoroutine("CountTo");

                player.m_Score += m_PointsToCollect;
                m_PointsToCollect = 100;
                StartCoroutine("CountTo");

                partyBar.m_Current += collectible.gold;
                partyBar.partybarLogo.SetBool("Gain", true);
                //partyBar.partybarLogo.SetBool("Drain", false);

                if (collectible.type == Collectible.Type.Death)
                {
                    //lose health
                }

                other.gameObject.SetActive(false);
            }
            else if (GameManager.m_Instance.m_GameState == GameManager.GameState.Minigame && collectible.type == Collectible.Type.Simple && player.m_State == Player.State.Alive && partyBar.m_Current > 0.0f)
            {
                player.m_Score += 100;
            }
        }
    }
    IEnumerator CountTo()
    {
        //int start = player.m_Score;
        int temp = m_PointsToCollect;
        for (int i = 0; i < temp; i += 1)
        {
            //float progress = timer / duration;
            //player.m_Score = (int)Mathf.Lerp(start, target, progress);
            player.m_Score += 1;
            m_PointsToCollect -= 1;
            yield return null;
        }
        //player.m_Score = target;
    }
}
