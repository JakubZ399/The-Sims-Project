using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Light fridgeLight;

    public void LightOn()
    {
        fridgeLight.intensity = 20;
    }

    public void LightOff()
    {
        fridgeLight.intensity = 0;
    }
}
