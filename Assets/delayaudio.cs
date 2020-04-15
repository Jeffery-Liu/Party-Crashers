using UnityEngine;
using System.Collections;

public class delayaudio : MonoBehaviour {

    public AudioSource audiosource;
    public float delayTime;
	// Use this for initialization
	void Start ()
    {
        audiosource.PlayDelayed(delayTime);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
