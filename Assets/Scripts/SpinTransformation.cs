using UnityEngine;
using System.Collections;

public class SpinTransformation : MonoBehaviour
{
    public Vector3 spin;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spin * Time.deltaTime);
    }

}
