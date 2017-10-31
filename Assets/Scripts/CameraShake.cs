// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform cameraTransform;

    public float shakeDuration = 0f;
    public float shakeIntensity = 0.1f;

    public bool isShaking;
    Vector3 originalPos;
    // Use this for initialization
    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
    }

    void Start()
    {
        originalPos = cameraTransform.position;
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
                    cameraTransform.position = originalPos + Random.insideUnitSphere * shakeIntensity;
                    //Decrease the shake intensity with time elapsing
                    shakeIntensity -= Time.deltaTime * shakeIntensity / shakeDuration; 
                    shakeDuration -= Time.deltaTime;
                }
                else
                {
                    shakeDuration = 0f;
                    cameraTransform.position = originalPos;
                    isShaking = false;
                }
            }
        }


    }
}
