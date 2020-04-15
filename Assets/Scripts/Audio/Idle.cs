using UnityEngine;
using System.Collections;

public class Idle : MonoBehaviour {


    // Use this for initialization

    //hurtsound
    public int maxChance;
    public int ChanceNumber;
    public AudioClip[] BadBoyIdleSFX;
    public AudioClip[] GothIdleSFX;
    public AudioClip[] NerdIdleSFX;
    public AudioClip[] MascotIdleSFX;
    public AudioClip SFXtoPlay;
    static private int Chance = 1;

    void Start () {
	
	}
	
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>().m_Model == Player.Model.Badboy)
        {
            ChanceNumber = Random.Range(0, maxChance);
            if (ChanceNumber == Chance)
            {
                SFXtoPlay = BadBoyIdleSFX[Random.Range(0, BadBoyIdleSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay);
            }
        }

        if (other.GetComponent<Player>().m_Model == Player.Model.Goth)
        {
            ChanceNumber = Random.Range(0, maxChance);
            if (ChanceNumber == Chance)
            {
                SFXtoPlay = GothIdleSFX[Random.Range(0, GothIdleSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay);
            }
        }

        if (other.GetComponent<Player>().m_Model == Player.Model.Mascot)
        {
            ChanceNumber = Random.Range(0, maxChance);
            if (ChanceNumber == Chance)
            {
                SFXtoPlay = MascotIdleSFX[Random.Range(0, MascotIdleSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay);
            }
        }

        if (other.GetComponent<Player>().m_Model == Player.Model.Nerd)
        {
            ChanceNumber = Random.Range(0, maxChance);
            if (ChanceNumber == Chance)
            {
                SFXtoPlay = NerdIdleSFX[Random.Range(0, NerdIdleSFX.Length)];
                AudioManager.m_Instance.PushMusic(SFXtoPlay);
            }
        }
    }
}
