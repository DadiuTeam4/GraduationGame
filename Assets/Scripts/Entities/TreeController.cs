// Author: You Wu
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : Shakeable

{
    public float thresholdForAppleFallDown = 800f;
    private Rigidbody treeRd;

	private bool isTreeFall;

    void Awake()
    {
        treeRd = GetComponent<Rigidbody>();
    }

	void Start()
	{
		isTreeFall = false;
	}

    public override void OnShakeBegin(float magnitude)
    {
        checkTreeFallDown(magnitude);
    }

    public override void OnShake(float magnitude)
    {
		checkTreeFallDown(magnitude);
    }

    private void checkTreeFallDown(float magnitude)
    {
        if (magnitude > thresholdForAppleFallDown && isTreeFall == false)
        {
			Vector3 randomForce = GetShakeForceOnShakebleObject(magnitude);
            treeRd.AddForce(new Vector3(randomForce.x, 0f, randomForce.y));
			isTreeFall = true;
        }

    }

}
