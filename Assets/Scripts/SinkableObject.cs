// Author: Itai Yavin
// Contributors:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkableObject : Holdable 
{

	private Vector3 startPosition;
	private Vector3 endPosition;
	
	[Header("Sinking Variables")]
	[Tooltip("The speed of which the object will sink")]
	[Range(0.01f, 1.0f)]
	public float sinkSpeed = 0.10f;
	[Tooltip("The amount of seconds pressed before sinking")]
	public float sinkDelay = 1.0f;
	[Tooltip("The depth in unity units the object will sink")]
	public float depth = 1.0f;

	[Header("Rising Variables")]
	[Tooltip("The speed of which the object will rise")]
	[Range(0.01f, 1.0f)]
	public float riseSpeed = 0.1f;
	[Tooltip("The amount of seconds before object rises again")]
	public float riseDelay = 1.0f;

	private float timeTillRise;
	private bool rising;

	private RaycastHit hit;
	private float t = 0.0f;

	void Start()
	{
		startPosition = transform.position;
		endPosition = startPosition;
		endPosition.y = startPosition.y - depth;
	}

	void Update()
	{
		if (rising && Time.time > timeTillRise)
		{
			Rise();
		}
	}

	public override void OnTouchBegin(RaycastHit hit)
	{
	}

	public override void OnTouchHold(RaycastHit hit)
	{
		if (timeHeld >= sinkDelay)
		{
			if (rising)
			{
				rising = false;
			}

			t += sinkSpeed;

			if (t >= 1.0f)
			{
				t = 1.0f;
			}

			transform.position = Vector3.Lerp(startPosition, endPosition, t);
		}
	}

	public override void OnTouchReleased()
	{
		rising = true;
		timeTillRise = Time.time + riseDelay;
	}

	public void Rise()
	{
		t -= riseSpeed;

		if (t < 0.0f)
		{
			t = 0.0f;
			rising = false;
		}

		transform.position = Vector3.Lerp(startPosition, endPosition, t);
	}

	private RaycastHit TestRayCasting()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

		return hit;
	}
}
