// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTest : MonoBehaviour
{

    public Transform playerTransform;
    private Transform cameraTransform;
    Vector3 offset;
    public float CameraSpeed = 0.1f;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();
        if(playerTransform == null)
        {
            Debug.LogError("Player Transform in Camera Movement Class Should not be Null!");
        }
    }

    void Start()
    {
         offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion playerRotation = Quaternion.Euler(transform.eulerAngles.x, playerTransform.eulerAngles.y, 0);
        transform.rotation = playerRotation;
        transform.position = playerTransform.position - (playerRotation * Vector3.forward * offset.magnitude);
        //Debug.Log("Should update the position");
    }
}
