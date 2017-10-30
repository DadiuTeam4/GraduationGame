using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    Vector3 offset;

	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActiveAndEnabled)
        {
            transform.position = player.transform.position + offset;
        }
    }

}
