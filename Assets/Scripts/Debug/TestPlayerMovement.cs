// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : Shakeable
{
    private Camera cam;
    private bool isWalking = false;
    Vector3 destination;

    private Rigidbody playerRd;

    void Awake()
    {
        playerRd = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100) && Input.GetMouseButtonDown(0))
        {
            destination = hit.point + new Vector3(0, 0.5f, 0);
            isWalking = true;
        }

        if (isWalking)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, 0.2f);
            Vector3 distance = transform.position - destination;
            if (distance.magnitude < 0.1)
            {
                isWalking = false;
            }
        }

    }

    public override void OnShakeBegin(float magnitude)
    {
        playerRd.AddForce(GetShakeForceOnShakebleObject(magnitude));
    }

    public override void OnShake(float magnitude)
    {
        playerRd.AddForce(GetShakeForceOnShakebleObject(magnitude));
    }

}