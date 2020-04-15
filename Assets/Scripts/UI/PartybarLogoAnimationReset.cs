using UnityEngine;
using System.Collections;

public class PartybarLogoAnimationReset : MonoBehaviour
{
    public void ResetAnimatorBools()
    {
        GetComponent<Animator>().SetBool("Gain", false);
        GetComponent<Animator>().SetBool("Drain", false);
    }
}
