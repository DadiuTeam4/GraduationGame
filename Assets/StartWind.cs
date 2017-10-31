using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWind : MonoBehaviour {

public GameObject windZoneLeft; //the prefab you want to spawn
public GameObject windZoneRight; //the prefab you want to spawn
public GameObject middleFog;
private static float waitTime = 3;
public float timer = waitTime; 
private float moveMiddleFogBy = 1;
private Vector3 originalMiddlePosition;

private GameObject newWind;
	void Start()
	{
		originalMiddlePosition = middleFog.transform.position;		
	}


	void Update ()
	{

		timer -= Time.deltaTime;

		if (Input.GetKeyDown("e"))
		{
			Destroy(newWind);
			middleFog.transform.position = new Vector3(middleFog.transform.position.x - moveMiddleFogBy,middleFog.transform.position.y,middleFog.transform.position.z);
  	       	newWind = Instantiate(windZoneLeft) as GameObject;
			timer = waitTime;
		}

		if (Input.GetKeyDown("q"))
		{
			Destroy(newWind);
			middleFog.transform.position = new Vector3(middleFog.transform.position.x + moveMiddleFogBy,middleFog.transform.position.y,middleFog.transform.position.z);			
  	       	newWind = Instantiate(windZoneRight) as GameObject;
			timer = waitTime;
		}

		if(timer < 0)
		{
			Destroy(newWind);
			middleFog.transform.position = originalMiddlePosition;
			timer = waitTime;
		}


    }
	
}
