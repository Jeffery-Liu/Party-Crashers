using UnityEngine;
using System.Collections;

public class EnemyAfterExplosion : MonoBehaviour {

    GameObject enemy;

    private EnemyAI enemyAI;
    private NavMeshAgent nav;

    void Start()
    {
        enemy = this.gameObject;
        enemyAI = GetComponent<EnemyAI>();
        nav = GetComponent<NavMeshAgent>();
    }

	// Update is called once per frame
	void Update ()
    {
	    if(enemy.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb.velocity == Vector3.zero)
            {
                Destroy(rb);
                nav.enabled = true;
                enemyAI.enabled = true;
            }

        }


	}
}


/*
 *  On Update do:
 *      check if enemy has a rigid body
 *          if true, check if enemy's velocity is zero
 *              if true, destroy rigid body, reactivate EnemyAI script & reactivate Nav Mesh Agent  
*/