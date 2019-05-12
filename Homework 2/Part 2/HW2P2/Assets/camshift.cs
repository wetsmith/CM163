using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camshift : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float accelx = 0.02f;
        float accely = 0.2f;
        float accelz = 0.2f;

        transform.Translate(accelx * Mathf.Cos(Time.time),0, 0);
    }
}
