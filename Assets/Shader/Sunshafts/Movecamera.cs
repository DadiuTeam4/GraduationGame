using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movecamera : MonoBehaviour {

public Vector3 move;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= move;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += move;
        }
    }
}
