using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Vector3 rotation = new Vector3(60, 0, 0);
    private int height = 20;
    private int distanceOffset = 15;
    public float y = 0;
    public Vector3 mPosition;
    public float m_Zoom = 0;
    [Range(1, 5)]
    public int m_ZoomAmount = 3;
    public float m_MaxZoom = 30;
    public float m_LerpAmount = 3f;

    public GameObject[] players;
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(0, height, 0);
        players = GameManager.m_Instance.m_Players;
        mPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(rotation);


        float xDistance = -1;
        float zDistance = -1;

        float x1 = 0;
        float z1 = 0;
        float x2 = 0;
        float z2 = 0;

        if (players.Length > 1)
        {
            //Loop through players and set x to greatest x distance, and y to greatest y distance between current player and any other player
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = i; j < players.Length; j++)
                {
                    if (Mathf.Abs(players[i].transform.position.x - players[j].transform.position.x) > xDistance) // if the distance is greater than current
                    {
                        xDistance = Mathf.Abs(players[i].transform.position.x - players[j].transform.position.x);
                        x1 = players[i].transform.position.x; // update x to the new greatest distance
                        x2 = players[j].transform.position.x; // set this gamobject to the other player that this player has the greatest distance with
                    }

                    CharacterController iController = players[i].GetComponent<CharacterController>();
                    CharacterController jController = players[j].GetComponent<CharacterController>();
                    if (iController.isGrounded && jController.isGrounded)
                    {
                        float iRounded = Mathf.Round(players[i].transform.position.y * 10f) / 10f;
                        float jRounded = Mathf.Round(players[j].transform.position.y * 10f) / 10f;

                        if (players[i].transform.position.y < players[j].transform.position.y)
                        {
                            //y = players[i].transform.position.y;
                            y = iRounded;
                        }
                        if (players[i].transform.position.y > players[j].transform.position.y)
                        {
                            //y = players[j].transform.position.y;
                            y = jRounded;
                        }

                    }


                    if (Mathf.Abs(players[i].transform.position.z - players[j].transform.position.z) > zDistance)
                    {
                        zDistance = Mathf.Abs(players[i].transform.position.z - players[j].transform.position.z);
                        z1 = players[i].transform.position.z;
                        z2 = players[j].transform.position.z;
                    }
                }
            }
        }
        else
        {
            /*
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = i; j < players.Length; j++)
                {
                    if (Mathf.Abs(players[i].transform.position.x - players[j].transform.position.x) > xDistance) // if the distance is greater than current
                    {
                        xDistance = Mathf.Abs(players[i].transform.position.x - players[j].transform.position.x);
                        x1 = players[i].transform.position.x; // update x to the new greatest distance
                        x2 = players[j].transform.position.x; // set this gamobject to the other player that this player has the greatest distance with
                    }

                    CharacterController iController = players[i].GetComponent<CharacterController>();
                    if (iController.isGrounded)
                    {
                        float iRounded = Mathf.Round(players[i].transform.position.y * 10f) / 10f;
                        y = iRounded;
                    }
                    if (Mathf.Abs(players[i].transform.position.z - players[j].transform.position.z) > zDistance)
                    {
                        zDistance = Mathf.Abs(players[i].transform.position.z - players[j].transform.position.z);
                        z1 = players[i].transform.position.z;
                        z2 = players[j].transform.position.z;
                    }
                }
            }*/
            x1 = players[0].transform.position.x;
            x2 = players[0].transform.position.x;

            CharacterController iController = players[0].GetComponent<CharacterController>();
            if (iController.isGrounded)
            {
                float iRounded = Mathf.Round(players[0].transform.position.y * 10f) / 10f;
                y = iRounded;
            }

            z1 = players[0].transform.position.z;
            z2 = players[0].transform.position.z;
        }


        float averageX = (x1 + x2) / 2;
        float averageZ = (z1 + z2) / 2;

        if (GameObject.Find("Boss") != null)
        {
            if (GameObject.Find("Boss").active)
            {
                GameObject boss = GameObject.Find("Boss");
                Transform bossTransform = boss.GetComponent<Transform>();
                float bossx = bossTransform.position.x;
                float bossz = bossTransform.position.z;

                averageX = (x1 + x2 + bossx) / 3;
                averageZ = (z1 + z2 + bossz) / 3;
            }
        }
            //float Y = y;
            if(m_Zoom > m_MaxZoom) { m_Zoom = m_MaxZoom; }

            gameObject.transform.position = new Vector3(Mathf.Lerp(transform.position.x,averageX,Time.deltaTime * m_LerpAmount), Mathf.Lerp(transform.position.y, y + height + m_Zoom, Time.deltaTime * m_LerpAmount), Mathf.Lerp(transform.position.z, averageZ - distanceOffset - m_Zoom,Time.deltaTime * m_LerpAmount));
        //Y + height

        mPosition = new Vector3(averageX, y, averageZ);
    }
}
