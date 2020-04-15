using UnityEngine;
using System.Collections;

public class BulletExplosion : MonoBehaviour {

    [SerializeField]
    private float power = 0f;
    private float radius = 1f;

    private EnemyAI enemyAiScript;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MeleeEnemy" || other.tag == "Enemy")
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach(Collider hit in colliders)
            {
                if(hit.tag == "MeleeEnemy" || hit.tag == "Enemy")
                {
                    // Disable the Nav Mesh Agent
                    hit.GetComponent<NavMeshAgent>().enabled = false;
                    // Disable Enemy AI Script
                    enemyAiScript = hit.GetComponent<EnemyAI>();
                    enemyAiScript.enabled = false;
                    // Add a Rigidbody and set its mass to 0.5
                    hit.gameObject.AddComponent<Rigidbody>();
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    rb.mass = 0.5f;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                    // Apply Explosion Force to the Rigidbody
                    rb.AddExplosionForce(power, explosionPos, radius, 2.0f);
                    // Destroy Rigidbody
                    Destroy(hit.GetComponent("Rigidbody"));
                    // Enable Enemy AI Script
                    enemyAiScript.enabled = true;
                    // Enable the Nav Mesh Agent
                    hit.GetComponent<NavMeshAgent>().enabled = true;
                }
            }
        }
    }
}
