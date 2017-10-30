using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour {
    private Camera cam;
    private bool isWalking = false;
    Vector3 destination;
    
    // Use this for initialization
    void Start () {
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100)&& Input.GetMouseButtonDown(0))
        {
            destination = hit.point+new Vector3(0,0.5f,0);
            isWalking = true;
        }

        if (isWalking) {
            transform.position = Vector3.MoveTowards(transform.position, destination, 0.2f);
            Vector3 distance = transform.position - destination;
            if (distance.magnitude < 0.1) {
                isWalking = false;
            }
        }

    }
    
}