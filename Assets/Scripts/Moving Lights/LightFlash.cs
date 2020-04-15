using UnityEngine;
using System.Collections;

public class LightFlash : MonoBehaviour
{
    public Light lt;
    public float colorSwapTime;
    public Color[] arrayOfColors;
    private int I;
    public AudioSource audioSource;
    public AudioClip[] SFX;
    private AudioClip SFXtoPlay;



    // Use this for initialization
    void Start()
    {
        I = -1;
        lt = GetComponent<Light>();
        changeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(I < arrayOfColors.Length)
            lt.color  = (arrayOfColors[I]);


    }

    void changeColor()
    {
        if (arrayOfColors.Length > (I+1))
        {
            I++;
        }
        else
        {
            I = 0;
        }
        //sound code
        if (SFX.Length > 0)
        {
            SFXtoPlay = SFX[Random.Range(0, SFX.Length)];
            audioSource.clip = SFXtoPlay;
            audioSource.Play();
        }
        else
        {
            //Debug.LogWarning("SFX audio not set for: " + gameObject.name);
        }
        //sound code
        Invoke("changeColor", colorSwapTime);

    }


}