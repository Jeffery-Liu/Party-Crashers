using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{

    public Animator animator;
    public GameObject[] prefab;
    private GameObject ins;
    public bool isOpen;
    public bool alreadyOpen = false;
    public GameObject m_effect;
    public GameObject m_Endeffect;
    GameObject endeffect;
    bool firstEffectFinish;

    //SFX Start
    public AudioSource audioSource;
    public AudioClip[] SFX;
    private AudioClip SFXtoPlay;
    //SFX END
    // Use this for initialization
    void Start()
    {
        if(animator != null)
            animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen == true && alreadyOpen == false && (Input.GetButtonDown("Interact_P1") || Input.GetButtonDown("Interact_Keyboard")))
        {
            if (m_effect != null)
            {
                animator.enabled = true;
                GameObject effect;
                effect = (GameObject)Instantiate(m_effect, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(effect, 3f);
                StartCoroutine(WaitChestExplosion(2f));
                Destroy(transform.parent.gameObject, 2f);
                Destroy(gameObject, 2f);
            }
            //sfx begin
            if (audioSource != null)
            {
                SFXtoPlay = SFX[Random.Range(0, SFX.Length)];
                audioSource.clip = SFXtoPlay;
                audioSource.Play();
            }
            //sfx end

            for (int i = 0; i < prefab.Length; i++)
            {
                GameObject weapon;
                weapon = (GameObject)Instantiate(prefab[i], gameObject.transform.position, gameObject.transform.rotation);
                weapon.name = prefab[i].name;
                alreadyOpen = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOpen = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOpen = false;
        }
    }

    IEnumerator WaitChestExplosion(float time)
    {
        yield return new WaitForSeconds(time);
        endeffect = (GameObject)Instantiate(m_Endeffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(endeffect, time);

    }
}
