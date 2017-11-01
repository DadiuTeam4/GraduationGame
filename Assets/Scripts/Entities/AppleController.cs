// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleController : Shakeable
{

    private Rigidbody appleRd;
    private bool isGravityActived;

	public float thresholdForAppleFallDown = 800f;

    private const float COEFFICIENT_FROM_MAGNITUDE_TO_FORCE = 0.005f;

    void Awake()
    {
        appleRd = GetComponent<Rigidbody>();
    }

    void Start()
    {
        isGravityActived = false;
    }

    public override void OnShakeBegin(float magnitude)
    {
        if (!isGravityActived)
        {
            if (magnitude > thresholdForAppleFallDown)
            {
                appleRd.useGravity = true;
            }
        }
    }

    public override void OnShake(float magnitude)
    {
        if (!isGravityActived)
        {
            if (magnitude > thresholdForAppleFallDown)
            {
                appleRd.useGravity = true;
				isGravityActived = true;
            }
        }
        else
        {
            appleRd.AddForce(GetShakeForceOnShakebleObject(magnitude));
        }

    }

    Vector3 GetShakeForceOnShakebleObject(float magnitude)
    {
        return Random.insideUnitSphere * magnitude * COEFFICIENT_FROM_MAGNITUDE_TO_FORCE;
    }
}
