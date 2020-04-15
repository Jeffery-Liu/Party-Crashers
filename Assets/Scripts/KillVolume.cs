using UnityEngine;
using System.Collections;

public class KillVolume : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player p = other.gameObject.GetComponent<Player>();
            p.respawn();
        }
    }
}
