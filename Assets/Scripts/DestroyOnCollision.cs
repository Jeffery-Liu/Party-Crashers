using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField]
    private float LifeTimer;
    private Collider col;

    //James VFX code
    public GameObject hitVFX;
    //James VFX code
    // Use this for initialization
    void Start()
    {
        StartCoroutine(DeactivateCallback());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //hames VFX code
        if (hitVFX != null)
        {
            GameObject hitParticle;
            hitParticle = (GameObject)Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(hitParticle, 1f);
        }
        //James VFX code
        if (other.gameObject.GetComponent<WeaponID>() == null) 
        {
            DestroyBullet();
        }
    }

    private IEnumerator DeactivateCallback()
    {
        yield return new WaitForSeconds(LifeTimer);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
