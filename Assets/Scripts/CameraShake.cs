using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform cameraTransform;

    public float shakeDuration = 0f;
    public float shakeIntensity = 0.1f;

    Vector3 originalPos;
    // Use this for initialization
    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
		originalPos = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
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
                shakeDuration -= Time.deltaTime;
            }
            else
            {
                shakeDuration = 0f;
                cameraTransform.position = originalPos;
            }
        }

    }
}
