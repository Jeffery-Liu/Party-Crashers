using UnityEngine;
using System.Collections;

public class RecycleBullet : MonoBehaviour
{
    [SerializeField]
    private float bulletLifeTimer;

    private Bow bow;

    public void Start()
    {
        bow = GetComponentInParent<Bow>();
        StartCoroutine(DeactivateCallback());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Coins" /*&& other.tag != "Player"*/)
            DestroyBullet();
    }

    private IEnumerator DeactivateCallback()
    {
        yield return new WaitForSeconds(bulletLifeTimer);
        DestroyBullet();
    }

    private void DestroyBullet()
    {		
        Destroy(gameObject);
    }
}
