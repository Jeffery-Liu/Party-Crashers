using UnityEngine;
using System.Collections;

public class Triggereneemyspawner : MonoBehaviour {

    public GameObject[] Enemy;
    private bool enemyOn;
    private int I;
    // Use this for initialization
    void Start()
    {
        enemyOn = false;
        while( Enemy.Length >= (I + 1) )
        {
            if (Enemy[I] != null)
            {
                Enemy[I].SetActive(false);
            }
            I++;
        }


    }

    void OnTriggerEnter( Collider other )
    {
        if( enemyOn == false && other.gameObject.tag == "Player" )
        {
            I = 0;
            while( Enemy.Length >= (I + 1) )
            {
                Enemy[I].SetActive(true);
                I++;
            }
        }

        enemyOn = true;
    }

}
