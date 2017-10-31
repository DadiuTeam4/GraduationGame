// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Use this for initialization
    public GameObject player;
    public bool isFollwingTheHuner;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
        isFollwingTheHuner = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollwingTheHuner)
        {
            transform.position = player.transform.position + offset;
        }

    }

}
