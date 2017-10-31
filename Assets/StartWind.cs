using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWind : MonoBehaviour {

public GameObject windZoneLeft; //the prefab you want to spawn
public GameObject windZoneRight; //the prefab you want to spawn

private static float waitTime = 3;
public float timer = waitTime; 

private GameObject newWind;
	void Update ()
	{

		timer -= Time.deltaTime;

		if (Input.GetKeyDown("e"))
		{
			Destroy(newWind);
  	       	newWind = Instantiate(windZoneLeft) as GameObject;
			timer = waitTime;
		}

		if (Input.GetKeyDown("q"))
		{
			Destroy(newWind);
  	       	newWind = Instantiate(windZoneRight) as GameObject;
			timer = waitTime;
		}

		if(timer < 0)
		{
			Destroy(newWind);
			timer = waitTime;
		}


    }
	
}
