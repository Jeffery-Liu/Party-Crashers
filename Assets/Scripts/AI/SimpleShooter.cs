using UnityEngine;
using System.Collections;

public class SimpleShooter : MonoBehaviour
{

    public GameObject shooterMesh;
    public GameObject firepoint;
    public GameObject projectile;

    public float projectileSpeed;
    [Range(0.1f,3f)]
    public float delay;

    void Start()
    {

        Fire();

        if (shooterMesh)
        {
            GameObject mesh;
            mesh = (GameObject)Instantiate(shooterMesh, this.gameObject.transform.position, this.gameObject.transform.rotation);
            mesh.gameObject.transform.parent = this.gameObject.transform;
        }

    }

    void Fire()
    {

        GameObject bullet;
        bullet = (GameObject)Instantiate(projectile, firepoint.transform.position, firepoint.transform.rotation);
        StartCoroutine("BetweenFire");

        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed,ForceMode.Impulse);
    }

    public IEnumerator BetweenFire()
    {

        yield return new WaitForSeconds(delay);

        Fire();

    }


}
