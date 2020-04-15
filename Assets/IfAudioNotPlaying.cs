using UnityEngine;
using System.Collections;

public class IfAudioNotPlaying : MonoBehaviour
{

    public GameObject self;
    public AudioSource sfx;
    // Update is called once per frame
    void Update()
    {
        if (!sfx.isPlaying)
        {
            Destroy(self);
        }
            
    }
    
}
