using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public static AudioManager m_Instance;
    private AudioSource m_AudioSource;
    private AudioClip m_CurMusic;
    private float m_pitch;
    private List<AudioClip> m_RandomMusicList;
    public float m_DelayTime;

    private bool m_IsPlaying;
    private float m_DelayCounter;
    // Use this for initialization
    void Start () {
        if (m_Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            m_Instance = this;
            m_RandomMusicList = new List<AudioClip>();
            m_AudioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_DelayCounter -= Time.deltaTime;
        if (m_DelayCounter <= 0)
        {
            m_IsPlaying = false;
        }
        else
        {
            m_IsPlaying = true;
        }
    }

    IEnumerator WaitListReset()
    {
        yield return new WaitForSeconds(m_DelayTime);
        ResetMusicList();
    }

    public void PushMusic(AudioClip ac)
    {
        if (m_IsPlaying == false)
        {
            //m_RandomMusicList.Add(ac);
            //m_AudioSource.clip = ac;
            //m_AudioSource.pitch = m_pitch;
            m_AudioSource.PlayOneShot(ac, 1);
            m_IsPlaying = true;
            m_DelayCounter = m_DelayTime;
        }
    }

    void ResetMusicList()
    {
        m_RandomMusicList.Clear();
    }


}
