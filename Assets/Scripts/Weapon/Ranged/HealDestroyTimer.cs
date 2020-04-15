using UnityEngine;
using System.Collections;

public class HealDestroyTimer : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField]
    private float m_HealDestroyTimer;
    [SerializeField]
    private GameObject m_ShootVFX;
    private bool isShootVFX = false;
    [SerializeField]
    private GameObject m_HealVFX;
    private bool isHealVFX = false;
    [SerializeField]
    private GameObject m_DestroyHealVFX;
    private bool isDestroyHealVFX = false;
    [SerializeField]
    private GameObject VFXFirePoint;

    private float speed;
    private float initspeed;
 
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        initspeed = rb.velocity.magnitude;    
    }
	
	void Update ()
    {
        ShootVFX();

        speed = rb.velocity.magnitude;
        if (speed < 0.5f)
        {    
            StopCoroutine(Heal());
            StartCoroutine(Heal());
        }      
    }
   
    private IEnumerator Heal()
    {
        HealVFX();
        yield return new WaitForSeconds(m_HealDestroyTimer);       
        DestroyVFX();
        Destroy(this.gameObject);
    }

    private void ShootVFX()
    {
        if(m_ShootVFX)
        {
            if(!isShootVFX)
            {
                GameObject ShootVFX;
                ShootVFX = (GameObject)Instantiate(m_ShootVFX, transform.position, transform.localRotation);                
                ShootVFX.transform.Rotate(new Vector3(-90, 0, 0));
                isShootVFX = true;
                if (initspeed < 0.1f)
                    Destroy(ShootVFX, m_HealDestroyTimer);
            }
        }
    }

    private void HealVFX()
    {
        if (m_HealVFX)
        {
            if (!isHealVFX)
            {
                GameObject FizzyHeal;
                FizzyHeal = (GameObject)Instantiate(m_HealVFX, transform.position, Quaternion.Euler(transform.rotation.x + 270, transform.rotation.y, transform.rotation.z));
                isHealVFX = true;
                if(speed < m_HealDestroyTimer - 1f)
                    Destroy(FizzyHeal, m_HealDestroyTimer);
            }
        }
    } 
       
    private void DestroyVFX()
    { 
        if (m_DestroyHealVFX != null)
        {
            if(!isDestroyHealVFX)
            {
                GameObject DestroyVFX;
                DestroyVFX = (GameObject)Instantiate(m_DestroyHealVFX, transform.position, Quaternion.Euler(transform.rotation.x + 270, transform.rotation.y, transform.rotation.z));
                isDestroyHealVFX = true;
                Destroy(DestroyVFX, m_HealDestroyTimer);
            }
        }
    }
}
