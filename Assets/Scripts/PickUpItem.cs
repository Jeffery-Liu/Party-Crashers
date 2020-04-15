using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour
{ 
	//VfX
	public GameObject getpickupVFX;
	//VFX end
	 
    void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player") && other.GetComponent<Player>().m_State == Player.State.Alive)
        {
			//VfX
			if (getpickupVFX != null) 
			{
				GameObject getfood;
				getfood = (GameObject)Instantiate (getpickupVFX, transform.position, transform.rotation);
				Destroy (getfood, 0.5f);
			}
			//VFX end
            gameObject.SetActive(false);
        }
    }
}
