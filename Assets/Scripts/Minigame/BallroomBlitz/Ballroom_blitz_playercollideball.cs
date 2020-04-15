using UnityEngine;
using System.Collections;

public class Ballroom_blitz_playercollideball : MonoBehaviour
{
    public float respawnTimer;
    Player player;
    
    [HideInInspector]
    public bool isAlive;
    
    void Start()
    {
        isAlive = true;
        
    }

    void Update()
    {
        if(!isAlive)
        {
            Respawn(respawnTimer);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Minigame Projectile")
        {
            gameObject.SetActive(false);    
            isAlive = false;                
        }
        
    }

    IEnumerator Respawn(float setTime)
    {
        yield return new WaitForSeconds(setTime);
        gameObject.SetActive(true);
        isAlive = true;
        //Debug.Log("Alive");
    }
}
