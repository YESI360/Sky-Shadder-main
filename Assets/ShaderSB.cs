using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSB : MonoBehaviour
{
    Renderer rend;
    public float manosLim;
    public float luzLimt;

    public float MaxContribution;
    public float MinContribution;
    public float MinContributionNew;//start in black
    public int MinContributionCount;
    public float VelContribution;

    public float currentValue;
    public float currentValueNew = 0.210f;
    [Range(0.210f, 1)] public float lerpedValue;

    public float targetValueUp;
    public float targetValueDown;

    public float lerpSpeedUp;
    public float lerpSpeedDown;
    private float lerpSpeedUpNew;
    private float lerpSpeedUpCount;
    public float lerpSpeedUpVel;
    bool manos;

    void Start()
    {
        rend = GetComponent<Renderer>();
        currentValue = rend.material.GetFloat("_ZeroValue");

        MaxContribution = 1;
        MinContribution = 0;
        targetValueUp = 1;
        targetValueDown = 0;
        MinContributionNew = 0.181f;//0.210f; //=1 resp
        lerpSpeedUpCount = 0;
        MinContributionCount = 0;
    }

    void Update()
    {
        if (Input.GetKey("6")) // (Input.GetKeyDown("1"))//
        {
            MinContributionNew = 0.500f;
            Debug.Log("ver");
        }
    }

    public void LuzUp()
    {
        currentValueNew = rend.material.GetFloat("_ZeroValue");

        lerpedValue = Mathf.Lerp(currentValueNew, targetValueUp, Time.deltaTime * lerpSpeedUpNew);

        rend.material.SetFloat("_ZeroValue", Mathf.Clamp(lerpedValue, MinContributionNew, MaxContribution));

        currentValueNew = rend.material.GetFloat("_ZeroValue");

        lerpSpeedUpCount = lerpSpeedUpCount + 1; // So you add to the count everytime you go into this function, if it executes once when you have a new breath
        lerpSpeedUpNew = lerpSpeedUpCount * lerpSpeedUpVel; // So this increases by 0.2f everytime this function is called

        MinContributionCount = MinContributionCount + 1;
        MinContributionNew = MinContributionCount * VelContribution;




        if (MinContributionNew >= manosLim)
        {
            Debug.Log("manos");
        }

        if (manos == true)
        {      
            Debug.Log("espejoReal");
        }

        if (MinContributionNew >= luzLimt)
        {
            Debug.Log("luzAmb");
        }

        /*
         si me pongo mano en cintura (4) = veo avatar = mirror.mirrorFake();
        empiezo instrucciones respirarar belly
        luz belly
         */
    }


    public void LuzDown()
    {

        lerpedValue = Mathf.Lerp(currentValue, targetValueDown, Time.deltaTime * lerpSpeedDown);

        rend.material.SetFloat("_ZeroValue", Mathf.Clamp(lerpedValue, MinContributionNew, MaxContribution));

        currentValue = rend.material.GetFloat("_ZeroValue");
    }

}
