using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManipulator : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ParticleSystem ps;

        // --------------------------------------------------------
        // ------- animate the cube size based on spectrum data.

        // consolidate spectral data to 8 partitions (1 partition for each rotating cube)
        int numPartitions = 8;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

        for (int i = 0; i < numDisplayedBins; i++)
        {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
                aveMag[(int)partitionIndx] += AudioPeer.spectrumData[i] / (512 / numPartitions);
            }
            else
            {
                partitionIndx++;
                i--;
            }
        }

        // scale and bound the average magnitude.
        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = (float)0.5 + aveMag[i] * 100;
            if (aveMag[i] > 100)
            {
                aveMag[i] = 100;
            }
        }

        ps = GetComponent<ParticleSystem>();
        var emission = ps.emission;
        // Map the magnitude to the cubes based on the cube name.
        if (gameObject.name == "topParticle")
        {
            emission.rateOverTime = aveMag[0] * aveMag[0] * 10;
        }

        if (gameObject.name == "orbitParticle")
        {
            if(aveMag[0] > 1.3)
            {
                emission.rateOverTime = aveMag[0] * aveMag[0] * 4;
                ps.startColor = new Vector4(40f * aveMag[0], 40f * aveMag[0], 40f * aveMag[0], 1);
            }
            else
            {
                emission.rateOverTime = 0;
            }

        } 





        // --------- End animating cube via spectral data
        // --------------------------------------------------------



    }


}

