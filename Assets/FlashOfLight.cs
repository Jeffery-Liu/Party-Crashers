using UnityEngine;
using System.Collections;

public class FlashOfLight : MonoBehaviour {

    public float lightLifetime = 0.75f;
    public float lightIntensityRate = 0.1f;

    private Light flashOfLight;

    void Start()
    {
        flashOfLight = GetComponent<Light>();
        flashOfLight.enabled = true;
        StartCoroutine(WaitToDisable());

    }

    void Update()
    {
        if(flashOfLight.intensity > 0)
        {
            flashOfLight.intensity -= lightIntensityRate;
        }
    }

    IEnumerator WaitToDisable()
    {
        yield return new WaitForSeconds(lightLifetime);
        flashOfLight.enabled = false;
    }


}
