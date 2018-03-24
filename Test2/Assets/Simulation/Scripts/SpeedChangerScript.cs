using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangerScript : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float oldScale = Time.timeScale;
        if(Input.GetKeyDown(KeyCode.KeypadPlus) && (oldScale < 100))
        {
            Time.timeScale = oldScale + 1;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) && (oldScale > 0))
        {
            Time.timeScale = oldScale - 1;
        }
    }

    //void FixedUpdate()
    //{
    //    Debug.Log(Time.deltaTime);
    //}
}
