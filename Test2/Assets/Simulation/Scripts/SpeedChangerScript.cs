using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangerScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float oldScale = Time.timeScale;
        if(Input.GetKeyDown(KeyCode.KeypadPlus) && (oldScale < 10))
        {
            Time.timeScale = oldScale + 1;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) && (oldScale > 0))
        {
            Time.timeScale = oldScale - 1;
        }
    }
}
