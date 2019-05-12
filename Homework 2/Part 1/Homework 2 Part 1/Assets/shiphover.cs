using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiphover : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float accelx = 0.2f;
        float accely = 0.02f;
        float accelz = .5f;
        transform.Rotate(accelx * Mathf.Cos(Time.time), 0, accelz * Mathf.Cos(Time.time));
        transform.Translate(accelx * 0.3f * Mathf.Cos(Time.time), accely * Mathf.Cos(Time.time * 5),0);
    }
}
