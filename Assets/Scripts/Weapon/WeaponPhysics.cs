using UnityEngine;
using System.Collections;
using System;

public class WeaponPhysics : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;
    private bool[] is_touched = new bool[4] { false, false, false, false };
    protected GameObject[] m_player;

    //[SerializedField]
    //private Transform[] players;

    //public Transform player1;
    //public Transform player2;
    //public Transform player3;
    //public Transform player4;

    //public float interactRadius = 10;


   void Start()
    {
        m_player = GameManager.m_Instance.m_Players;
        rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //foreach (Transform player in players)
        //{
        //    switch (player.GetComponent<Player>().m_Player)
        //    {
        //        case Player.PLAYER.P1:
        //            player1 = player.transform;
        //            break;
        //        case Player.PLAYER.P2:
        //            player2 = player.transform;
        //            break;


        //    }

        //}

    }

    void Update()
    {
        for (int i = 0; i < m_player.Length; i++)
        {
            if (is_touched[i] == true)
            {
                rb.AddForce((transform.position - m_player[i].transform.position) * speed);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < m_player.Length; i++)
        {
            if (other.GetComponent<Player>() != null && other.GetComponent<Player>().m_State == Player.State.Alive)
            {
                if (other.GetComponent<Player>().m_Player == m_player[i].GetComponent<Player>().m_Player)
                {
                    is_touched[i] = false;
                }
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < m_player.Length; i++)
        {
            if (other.GetComponent<Player>() != null && other.GetComponent<Player>().m_State == Player.State.Alive)
            {
                if (other.GetComponent<Player>().m_Player == m_player[i].GetComponent<Player>().m_Player)
                {
                    is_touched[i] = true;
                }
            }
        }
    }
}
