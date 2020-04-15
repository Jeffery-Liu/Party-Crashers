using UnityEngine;
using System.Collections;

public class FizzPopPickup : MonoBehaviour {


    //sound
    public GameObject SFXPlayer;
    public AudioClip[] SFX;
    private AudioClip SFXtoPlay;

    public AudioClip[] BadBoyHealthSFX;
    public AudioClip[] GothHealthSFX;
    public AudioClip[] NerdHealthSFX;
    public AudioClip[] MascotHealthSFX;
    public AudioClip SFXtoPlay2;

    //VfX
    public GameObject usepickupVFX;
	//VFX end

    public int m_HealValue = 0;
    private HeartSystem m_HeartSystem;
    // Use this for initialization

    private float timer;

    void Start ()
    {
	}

    private void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && other.GetComponent<Player>().m_State == Player.State.Alive)
        {
            if (other.GetComponent<Player>().m_Model == Player.Model.Badboy)
            {
                SFXtoPlay2 = BadBoyHealthSFX[Random.Range(0, BadBoyHealthSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay2);
            }

            if (other.GetComponent<Player>().m_Model == Player.Model.Goth)
            {
                SFXtoPlay2 = GothHealthSFX[Random.Range(0, GothHealthSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay2);
            }

            if (other.GetComponent<Player>().m_Model == Player.Model.Mascot)
            {
                SFXtoPlay2 = MascotHealthSFX[Random.Range(0, MascotHealthSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay2);
            }

            if (other.GetComponent<Player>().m_Model == Player.Model.Nerd)
            {
                SFXtoPlay2 = NerdHealthSFX[Random.Range(0, NerdHealthSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay2);
            }
        }

        if (other.GetComponent<HeartSystem>() != null && other.GetComponent<Player>().m_State == Player.State.Alive)
        {
            m_HeartSystem = other.GetComponent<HeartSystem>();
            m_HeartSystem.Heal(m_HealValue);
            m_HeartSystem.UpdateHearts();

            //sound
            SFXtoPlay = SFX[Random.Range(0, SFX.Length)];

			//VfX
			if (usepickupVFX != null) 
			{
				GameObject getHeal;
				getHeal = (GameObject)Instantiate (usepickupVFX, other.transform.position, transform.rotation);
				Destroy (getHeal, 0.5f);
			}
			//VFX end

            if (SFXPlayer != null)
            {
                AudioSource source = SFXPlayer.GetComponent<AudioSource>();
                source.clip = SFXtoPlay;
            }
            GameObject SFXtest = Instantiate(SFXPlayer, transform.position, transform.rotation) as GameObject;
            //sound end

            gameObject.SetActive(false);
        }
    }



    //void OnTriggerExit(Collider other)
    //{
    //    for (int i = 0; i < m_player.Length; i++)
    //    {
    //        if (other.GetComponent<Player>() != null)
    //        {
    //            if (other.GetComponent<Player>().m_Player == m_player[i].GetComponent<Player>().m_Player)
    //            {
    //                is_touched[i] = false;
    //            }
    //        }

    //    }
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    for (int i = 0; i < m_player.Length; i++)
    //    {
    //        if (other.GetComponent<Player>() != null)
    //        {
    //            if (other.GetComponent<Player>().m_Player == m_player[i].GetComponent<Player>().m_Player)
    //            {
    //                is_touched[i] = true;
    //            }
    //        }
    //    }
    //}

}
