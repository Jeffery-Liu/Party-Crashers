using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsNeonFrame : MonoBehaviour {

    Image creditsNeonFrame;
    bool isOn;
    Color32 on = new Color32(255, 255, 255, 255);
    Color32 flicker = new Color32(235, 235, 235, 255);

    //START() + FLICKER() = Flickering effect as long as Animator is disabled on Frames
    void Start()
    {
        creditsNeonFrame = GameObject.Find("Credits Neon Frame").GetComponent<Image>();
        InvokeRepeating("Flicker", 0.0f, 0.1f);
    }
    void Flicker()
    {
        if (!isOn)
        {
            creditsNeonFrame.color = flicker;
            isOn = true;
        }
        else
        {
            creditsNeonFrame.color = on;
            isOn = false;
        }
    }
}
