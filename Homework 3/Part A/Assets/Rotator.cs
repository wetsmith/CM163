using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		// --------------------------------------------------------
		// ------- animate the cube size based on spectrum data.

		// consolidate spectral data to 8 partitions (1 partition for each rotating cube)
		int numPartitions = 8;
		float[] aveMag = new float[numPartitions];
		float partitionIndx = 0;
		int numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

		for (int i = 0; i < numDisplayedBins; i++) 
		{
			if(i < numDisplayedBins * (partitionIndx + 1) / numPartitions){
				aveMag[(int)partitionIndx] += AudioPeer.spectrumData [i] / (512/numPartitions);
			}
			else{
				partitionIndx++;
				i--;
			}
		}

		// scale and bound the average magnitude.
		for(int i = 0; i < numPartitions; i++)
		{
			aveMag[i] = (float)0.5 + aveMag[i]*100;
			if (aveMag[i] > 100) {
				aveMag[i] = 100;
			}
		}

		// Map the magnitude to the cubes based on the cube name.
		if (gameObject.name == "lowBox")
        {
            transform.Rotate(new Vector3(aveMag[1] * 10f, aveMag[0] * 10f, aveMag[4] * 10f) * Time.deltaTime);
        }
        else if (gameObject.name == "midBox")
        {
            transform.Rotate(new Vector3(aveMag[5] * 10f , -aveMag[2] * 10f, aveMag[0] * 10f) * Time.deltaTime );
        }
        else if (gameObject.name == "highBox")
        {
            transform.Rotate(new Vector3(-aveMag[3] * 10f, aveMag[6] * 10f, -aveMag[0] * 10f) * Time.deltaTime);
        }

        for (int i = 0; i < 7; i++) 
		{
			int index = i;
			if (gameObject.name == "Box (" + index + ")") {
				transform.localScale = new Vector3 (aveMag[index], aveMag[index], aveMag[index]);
			}
		}

		// --------- End animating cube via spectral data
		// --------------------------------------------------------



	}


}

