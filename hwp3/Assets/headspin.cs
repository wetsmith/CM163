using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headspin : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float accelx = 0.2f;
        float accely = 0.2f;
        float accelz = 0.2f;

        transform.Rotate(accelx * Mathf.Cos(Time.time), accely * Mathf.Cos(Time.time), accelz * Mathf.Cos(Time.time));
    }
}
