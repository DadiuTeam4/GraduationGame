// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform cameraTransform;

    public float shakeDuration = 0f;
    public float shakeIntensity = 0.5f;

    public Rigidbody playerRd;

    public bool isShaking;

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }

        if(playerRd == null)
        {
            Debug.LogError("playerRd in Camera Shake Class is NUll!");
        }
    }

    void Start()
    {
        isShaking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            if (cameraTransform == null)
            {
                Debug.LogError("Transform of Camera is NUll!");
            }
            else
            {
                if (shakeDuration > 0)
                {
                    Vector3 intensity = Random.insideUnitSphere * shakeIntensity;
                    cameraTransform.position = cameraTransform.position + intensity;
                    playerRd.AddForce(intensity * 100);
                    //Decrease the shake intensity with time elapsing
                    shakeIntensity -= Time.deltaTime * shakeIntensity / shakeDuration; 
                    shakeDuration -= Time.deltaTime;
                }
                else
                {
                    shakeDuration = 0f;
                    isShaking = false;
                }
            }
        }


    }
}
