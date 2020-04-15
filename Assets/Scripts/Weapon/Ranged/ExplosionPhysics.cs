using UnityEngine;
using System.Collections;

public class ExplosionPhysics : MonoBehaviour {

    [SerializeField]
    private float power = 0f;

    private float radius = 4f;
    private EnemyAI enemyAiScript;

    IEnumerator OnTriggerEnter(Collider other)
    {  

        if (other.tag == "MeleeEnemy" || other.tag == "Enemy")
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach(Collider hit in colliders)
            {
                if(hit.tag == "MeleeEnemy" || hit.tag == "Enemy")
                {
                    // Disable the Nav Mesh Agent
                    hit.GetComponent<NavMeshAgent>().enabled = false;
                    // Disable Enemy AI script
                    enemyAiScript = hit.GetComponent<EnemyAI>();
                    enemyAiScript.enabled = false;
                    // Add a Rigidbody and set its mass to 0.5
                    if (hit.gameObject.GetComponent<Rigidbody>() == null)
                    {
                        hit.gameObject.AddComponent<Rigidbody>();
                    }
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    rb.mass = 0.5f;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                    // Apply Explosion Force to the Rigid Body
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
                }  
            }
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}


/*
 *  1 - Check collision with enemies Capsule Collider;
 *  2 - If it collides, do the following to each enemy that has collided:
 *  2.1 - Disable the Nav Mesh Agent
 *  2.2 - Disable Enemy AI script
 *  2.3 - Add a Rigid Body
 *  2.4 - Apply Explosion Force to the Rigid Body 
 *    
 *  -----
 *  Explosion Resolution
 *  
 *  Option A - Check the enemy's velocity vector after the explosion. When it gets to zero, reactivate Nav Mesh Agent & destroy rigid body
 *             Explosion Power should be 500 
 *             Rigid Body mass, 0.5. Freeze rotation on X & Z axis.
 *  Option B - Only activate nav mesh when enemy hits the floor
 *  Option C - Add a timer to the enemy that controls when the nav mesh is activated / rigid body is destroyed
 *  
 *  Explosion Resolution Pseudo-code
 *  
 *  Create a new script for the enemies.
 *  On Update do:
 *      check if enemy has a rigid body
 *          if true, check if enemy's velocity is zero
 *              if true, destroy rigid body, reactivate EnemyAI script & reactivate Nav Mesh Agent
 *  
 */