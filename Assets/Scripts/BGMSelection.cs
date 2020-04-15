using UnityEngine;
using System.Collections;

public class BGMSelection : MonoBehaviour {

    public AudioSource audio;
    private AudioClip currentMusic;
    public AudioClip[] BGMList;
    public bool swaptest;

    private GameObject boss;
    private EnemyHealth bossHealth;

    // Use this for initialization
    void Start()
    {
        
        audio.Play();

        boss = GameObject.Find("Boss");
        bossHealth = boss.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swaptest)
        {
            swaptest = false;
            playRandomMusic();
        }
        if (!audio.isPlaying)
        {
            swaptest = false;
            playRandomMusic();
        }


        boss = GameObject.Find("Boss");
        
        if (boss != null)
        {
            bossHealth = boss.GetComponent<EnemyHealth>();
            if (bossHealth.m_EnemyHealth < 50)
            {
                if (currentMusic != BGMList[1])
                {
                    currentMusic = BGMList[1];
                    audio.clip = currentMusic;
                    audio.Play();
                }
            }
            else
            {
                if(currentMusic != BGMList[0])
                {
                    audio.clip = currentMusic;
                    currentMusic = BGMList[0];
                    audio.Play();
                }
                
            }
        }else
        {
            boss = GameObject.Find("Boss");
            if (currentMusic != BGMList[0])
            {
                audio.clip = currentMusic;
                currentMusic = BGMList[0];
                audio.Play();
            }
        }

    }
    void playRandomMusic()
    {
        
        if(GameObject.Find("BossManagerObject") == null)
        {
            currentMusic = BGMList[Random.Range(0, BGMList.Length)];
            audio.clip = currentMusic;
            audio.Play();

        }else
        {
            currentMusic = BGMList[0];
            audio.clip = currentMusic;
            audio.Play();
        }

    }
}
