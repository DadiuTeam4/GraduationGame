// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Variable For following the player
    public GameObject player;
    public bool isCameraFollwingPlayer;
    Vector3 offset;

    //Variables For Shaking
    public float shakeDuration = 0f;
    public float shakeIntensity = 0.5f;
    public bool isShaking;
    private Transform cameraTransform;
    private Rigidbody playerRd;
    public float CameraSpeed = 0.1f;

    void Awake()
    {
        cameraTransform = GetComponent<Transform>();

        playerRd = player.GetComponent<Rigidbody>();

    }

    void Start()
    {
        offset = transform.position - player.transform.position;
        //If is shaking, should not follow the player
        isCameraFollwingPlayer = false;
        isShaking = true;
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
        playerRd.AddForce(intensity * 100);
        //Decrease the shake intensity with time elapsing
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
