using UnityEngine;
using System.Collections;

public class Mosh_Royale_MinigameMaster : MonoBehaviour {

    public GameObject moshpitEnemy;
    public GameObject enemySpawnpoint1;
    public GameObject enemySpawnpoint2;
    public GameObject enemySpawnpoint3;
    public GameObject enemySpawnpoint4;
    public GameObject enemySpawnpoint5;
    public GameObject enemySpawnpoint6;
    public GameObject enemySpawnpoint7;
    public GameObject enemySpawnpoint8;
    public GameObject enemySpawnpoint9;
    public GameObject enemySpawnpoint10;
    public GameObject enemySpawnpoint11;
    public GameObject enemySpawnpoint12;
    private float difficultyTimer;//seconds passed
    private float incrementAmmount;

	// Use this for initialization
	void Start () 
    {
        difficultyTimer = 0;
        GameObject enemyTest1;
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint1.gameObject.transform.position, enemySpawnpoint1.gameObject.transform.rotation );
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint2.gameObject.transform.position, enemySpawnpoint2.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint3.gameObject.transform.position, enemySpawnpoint3.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint4.gameObject.transform.position, enemySpawnpoint4.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint5.gameObject.transform.position, enemySpawnpoint5.gameObject.transform.rotation );
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint6.gameObject.transform.position, enemySpawnpoint6.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint7.gameObject.transform.position, enemySpawnpoint7.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint8.gameObject.transform.position, enemySpawnpoint8.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint9.gameObject.transform.position, enemySpawnpoint9.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint10.gameObject.transform.position, enemySpawnpoint10.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate( moshpitEnemy, enemySpawnpoint11.gameObject.transform.position, enemySpawnpoint11.gameObject.transform.rotation);
        enemyTest1 = (GameObject)Instantiate (moshpitEnemy, enemySpawnpoint12.gameObject.transform.position, enemySpawnpoint12.gameObject.transform.rotation);
       
	}
	
}
