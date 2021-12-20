using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCtri : MonoBehaviour
{
    [SerializeField] private float secondFerRealTimeSecond;

    private bool isNight = false;

    [SerializeField] private float nightFogDensity;
    private float dayFogDensity;
    [SerializeField] private float fogDensityCalc;
    private float curFogDensity;

    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
    }

    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondFerRealTimeSecond * Time.deltaTime);

        if(transform.eulerAngles.x >= 170)
        {
            isNight = true;
        }
        else if(transform.eulerAngles.x <= 10)
        {
            isNight = false;
        }

        if(isNight)
        {
            if(curFogDensity <= nightFogDensity)
            {
                curFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = curFogDensity;
            }
        }
        else
        {
            if(curFogDensity >= dayFogDensity)
            {
                curFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = curFogDensity;
            }
        }
    }
}
