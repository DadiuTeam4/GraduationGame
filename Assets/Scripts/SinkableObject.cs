using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkableObject : MonoBehaviour {

	private Vector3 startPosition;
	private Vector3 endPosition;
	
	[Tooltip("The depth in unity units the object will sink")]
	public float depth = 1.0f;
	[Tooltip("The speed of which the object will sink")]
	[Range(0.01f, 1.0f)]
	public float sinkSpeed = 0.10f;
	[Tooltip("The amount of seconds pressed before sinking")]
	public float sinkDelay = 1.0f;
	[Tooltip("The speed of which the object will rise")]
	[Range(0.01f, 1.0f)]
	public float riseSpeed = 0.1f;
	[Tooltip("The amount of seconds before object rises again")]
	public float riseDelay = 1.0f;

	private float timeTillRise;
	private bool reachedDepth = false;
	private bool startedRising;

	private RaycastHit hit;
	private float timeSinceMouseButtonDown = 0.0f;
	private float t = 0.0f;

	void Start()
	{
		startPosition = transform.position;
		endPosition = startPosition;
		endPosition.y = startPosition.y - depth;
	}

	void Update()
	{
		if (reachedDepth && !startedRising)
		{
			timeTillRise = Time.time + riseDelay;
			startedRising = true;
			Debug.Log("SETTING TIME");
		}

		if(startedRising && Time.time > timeTillRise)
		{
			Debug.Log("STARTING COROUTINE");
			StartCoroutine("Rise");
			startedRising = false;
		}

		if (Input.GetMouseButton(0))
		{
			hit = TestRayCasting();
			if (hit.collider.gameObject == gameObject)
			{
				timeSinceMouseButtonDown += Time.deltaTime;
				Sink(timeSinceMouseButtonDown);
			}
		}
		else
		{
			timeSinceMouseButtonDown = 0.0f;
		}
	}

	public void Sink(float time)
	{
		if (time >= sinkDelay)
		{
			t += sinkSpeed;

			if (t >= 1.0f)
			{
				t = 1.0f;
				reachedDepth = true;
			}

			transform.position = Vector3.Lerp(startPosition, endPosition, t);
		}
	}

	public void Rise()
	{
		t -= riseSpeed;

		if (t < 0.0f)
		{
			t = 0.0f;
			reachedDepth = false;
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
