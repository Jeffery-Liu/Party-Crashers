using UnityEngine;
using System.Collections;

public class InteractImageRotation : MonoBehaviour
{

    public Quaternion fixedRotation;

    void Awake()
    {
        fixedRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = fixedRotation;
    }
}
