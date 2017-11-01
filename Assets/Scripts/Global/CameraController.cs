// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Shakeable
{

    //Variable For following the player
    public GameObject player;
    public bool isCameraFollwingPlayer;
    Vector3 offset;

    //Variables For Shaking
    public float shakeDuration = 2f;
    private float shakeIntensity = 0.4f;

    private const float COEFFICIENT_FROM_MAGNITUDE_TO_INTENSITY = 0.00005f; 
    public bool isShaking = false;
    private Transform cameraTransform;
    public float CameraSpeed = 0.1f;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();

    }

    void Start()
    {
        offset = transform.position - player.transform.position;
        //If is shaking, should not follow the player
        isCameraFollwingPlayer = false;
        isShaking = false;
    }

     public override void OnShakeBegin(float magnitude) 
     {
         shakeIntensity = magnitude * COEFFICIENT_FROM_MAGNITUDE_TO_INTENSITY;
         isShaking = true;
     }

    public override void OnShake(float magnitude) 
     {
         shakeDuration = 2f;
         float newShakeIntensity = magnitude * COEFFICIENT_FROM_MAGNITUDE_TO_INTENSITY;
         if(newShakeIntensity > shakeIntensity)
         {
             shakeIntensity = newShakeIntensity;
         } 
     }

    // Update is called once per frame
    void Update()
    {
        UpdateFollowingPlayer();
        UpdateShaking();
    }

    void UpdateFollowingPlayer()
    {
        if (isCameraFollwingPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset, CameraSpeed);
        }
    }

    void UpdateShaking()
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
                    ShakeCamera();
                }
                else
                {
                    StopShakingCamera();
                }
            }
        }

    }

    void ShakeCamera()
    {
        Vector3 intensity = Random.insideUnitSphere * shakeIntensity;
        cameraTransform.position = cameraTransform.position + intensity;
        shakeIntensity -= Time.deltaTime * shakeIntensity / shakeDuration;
        shakeDuration -= Time.deltaTime;
    }

    void StopShakingCamera()
    {
        shakeDuration = 0f;
        isShaking = false;
        isCameraFollwingPlayer = true;
    }

}
