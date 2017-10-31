// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkableObjectGenerator : MonoBehaviour 
{
	public int width = 100;
	public int height = 100;
	public GameObject sinkableCubePrefab;

	void Start () 
	{
		Vector3 objectPosition;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				objectPosition = new Vector3(i, 0, j);
				Object.Instantiate(sinkableCubePrefab, objectPosition, Quaternion.identity);
			}
		}	
	}
}
