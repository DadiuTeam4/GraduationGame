// Author: Mathias Dam Hedelund
// Contributors:

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : Singleton<InputSystem>
{
	[TextArea(0, 10)]
	public string header = "Handles all touch input and accelerometer input. All objects inheriting from the Holdable, Swipable and Shakable classes are called accordingly from this class.";
	#region TOUCH_INPUT
	public static List<Vector3> swipeDirections;

	private static readonly int maxNumberTouches = 20;
	private Holdable[] heldLastFrame = new Holdable[maxNumberTouches];
	private Holdable[] heldThisFrame = new Holdable[maxNumberTouches];
	private RaycastHit?[] raycastHits = new RaycastHit?[maxNumberTouches];
	private Dictionary<int, List<Vector3>> touchPositions = new Dictionary<int, List<Vector3>>();
	#endregion

	#region ACCELEROMETER_INPUT
	[Header("Shake input")]
	[Tooltip("Force multiplication to compensate for delta time.")]
	public float forceMultiplier = 100f;
	[Tooltip("Shakes under this threshold are ignored by the input system.")]
	public float lowerShakeTreshold = 3.5f;
	[Tooltip("This number represents how fast the cumulative magnitude drops when the tablet is not shaken.")]
	public float magnitudeDropRate = 0.2f;
	[Tooltip("The limit for how fast the cumulative magnitude can rise and fall.")]
	public float terminalVelocity = 50f;
	[Tooltip("A cumulative magnitude under this threshold is ignored.")]
	public float lowerMagnitudeThreshold = 200f;
	[Tooltip("The upper limit for the cumulative magnitude.")]
	public float maxCumulativeMagnitude = 10000f;

	private float magnitudeVelocity;
	private float cumulativeMagnitude;

	private bool shookLastFrame = false;
	private bool shookThisFrame = false;

	private float compensatedDeltaTime;

	private Shakeable[] shakeables;
	#endregion

	#region DEBUG
	#if UNITY_EDITOR
	// MOUSE DEBUGGING
	// NOT COMPILED IN BUILDS
	bool mouseDownLastFrame;
	#endif
	#endregion

	private void Start()
	{
		// Initialize touch positions
		for (int i = 0; i < maxNumberTouches; i++)
		{
			touchPositions.Add(i, new List<Vector3>());
		}

		// Initialize swipe list
		swipeDirections = new List<Vector3>();

		// Initialize shake values
		magnitudeVelocity = 0;
		cumulativeMagnitude = 0;

		// Get all shakeable objects
		shakeables = GetAllShakeables();
	}

	#region UPDATE_LOOP
	private void Update()
	{
		HandleAccelerometerInput();
		HandleTouchInput();
	}
	#endregion

	#region ACCELEROMETER_INPUT
	private void HandleAccelerometerInput()
	{
		compensatedDeltaTime = Time.deltaTime * forceMultiplier;
		UpdateMagnitudeVelocity();
		UpdateCumulativeMagnitude();
		CallShakeables();
		shookLastFrame = shookThisFrame;
		shookThisFrame = false;
	}

	private void UpdateMagnitudeVelocity()
	{
		Vector3 accelerationInput = Input.acceleration;
		float magnitude = accelerationInput.magnitude * compensatedDeltaTime;
		if (magnitude > lowerShakeTreshold)
		{
			magnitudeVelocity += magnitude;
		}
		magnitudeVelocity -= magnitudeDropRate * compensatedDeltaTime;
		magnitudeVelocity = Mathf.Clamp(magnitudeVelocity, -terminalVelocity, terminalVelocity);
	}

	private void UpdateCumulativeMagnitude()
	{
		cumulativeMagnitude = Mathf.Clamp(cumulativeMagnitude + magnitudeVelocity, 0.0f, maxCumulativeMagnitude);
		shookThisFrame = cumulativeMagnitude > lowerMagnitudeThreshold;
	}

	private void CallShakeables()
	{
		if (shookThisFrame || shookLastFrame)
		{
			foreach (Shakeable shakeable in shakeables)
			{
				if (shookThisFrame)
				{
					if(!shookLastFrame)
					{
						shakeable.OnShakeBegin(cumulativeMagnitude);
					}
					shakeable.OnShake(cumulativeMagnitude);
				}
				else
				{
					shakeable.OnShakeEnd();
				}
			}
		}
	}

	private Shakeable[] GetAllShakeables()
	{
		return FindObjectsOfType<Shakeable>();
	}
	#endregion

	#region TOUCH_INPUT
	private void HandleTouchInput()
	{
		swipeDirections.Clear();
		// Resolve all touches
		Touch[] touches = GetTouches();
		foreach (Touch touch in touches) 
		{
			switch (touch.phase) 
			{
				case (TouchPhase.Began):
				{
					TouchBegan(touch);
					break;
				}

				case (TouchPhase.Stationary):
				{
					TouchStationary(touch);
					break;
				}

				case (TouchPhase.Moved):
				{
					TouchMoved(touch);
					break;
				}

				case (TouchPhase.Ended):
				{
					TouchEnded(touch);
					break;
				}

				case (TouchPhase.Canceled):
				{
					TouchCanceled(touch);
					break;
				}
			}
		}
		
		#region DEBUG
		#if UNITY_EDITOR
		// MOUSE DEBUGGING
		// NOT COMPILED IN BUILDS
		if (touches.Length == 0)
		{
			if (Input.GetMouseButton(0)) 
			{
				OnMouse();
			}

			else if (Input.GetMouseButtonUp(0))
			{
				OnMouseUp();
			}
		}

		CallMouseHeldObjects();
		#endif
		#endregion

        CallHeldObjects(touches);
	}

	private Touch[] GetTouches() 
	{
		Touch[] touches = new Touch[Input.touchCount];
		for (int i = 0; i < Input.touchCount; i++) 
		{
			touches[i] = Input.GetTouch(i);
		}
		return touches;
	}

	private void TouchBegan(Touch touch) 
	{
		if (CastRayFromTouch(touch))
		{
			// Check if the touch hit a holdable
			Holdable holdable = GetHoldable(raycastHits[touch.fingerId].Value);
			if (holdable)
			{
				holdable.OnTouchBegin(raycastHits[touch.fingerId].Value);
				heldThisFrame[touch.fingerId] = holdable;
			}

			
			CheckSwipe(touch);
			
		}
	}

	private void TouchStationary(Touch touch)
	{
		heldThisFrame[touch.fingerId] = heldLastFrame[touch.fingerId];
    }

	private void TouchMoved(Touch touch) 
	{
		if (CastRayFromTouch(touch))
		{
			// Check if the touch hit a holdable
			Holdable holdable = GetHoldable(raycastHits[touch.fingerId].Value);
			if (holdable)
			{
				heldThisFrame[touch.fingerId] = holdable;
				if (heldThisFrame[touch.fingerId] != heldLastFrame[touch.fingerId])
				{
					heldThisFrame[touch.fingerId].OnTouchBegin(raycastHits[touch.fingerId].Value);
				}
			}

			
			CheckSwipe(touch);
			
		}
		if (heldLastFrame[touch.fingerId] && heldLastFrame[touch.fingerId] != heldThisFrame[touch.fingerId])
		{
			heldLastFrame[touch.fingerId].OnTouchReleased();
		}
	}

	private void TouchEnded(Touch touch) 
	{
		if (heldLastFrame[touch.fingerId])
		{
			heldLastFrame[touch.fingerId].OnTouchReleased();
		}
		touchPositions[touch.fingerId].Clear();
		raycastHits[touch.fingerId] = null;
	}

	private void TouchCanceled(Touch touch) 
	{
		if (heldLastFrame[touch.fingerId])
		{
			heldLastFrame[touch.fingerId].OnTouchReleased();
		}
		touchPositions[touch.fingerId].Clear();
		raycastHits[touch.fingerId] = null;
	}

	private bool CastRayFromTouch(Touch touch)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		if (Physics.Raycast(ray, out hit)) 
		{
			raycastHits[touch.fingerId] = hit;
			return true;
		}
		raycastHits[touch.fingerId] = null;
		return false;
	}

	private void CheckSwipe(Touch touch)
	{
		touchPositions[touch.fingerId].Add(touch.position);

		Vector3 firstPosition = touchPositions[touch.fingerId][0];
		Vector3 lastPosition = touchPositions[touch.fingerId][touchPositions[touch.fingerId].Count-1];

		Vector3 firstPoint = Camera.main.ScreenToWorldPoint(new Vector3(firstPosition.x, firstPosition.y, Camera.main.nearClipPlane));
		Vector3 lastPoint = Camera.main.ScreenToWorldPoint(new Vector3(lastPosition.x, lastPosition.y, Camera.main.nearClipPlane));

		Vector3 direction = lastPoint - firstPoint;
		
		swipeDirections.Add(direction);
		// Check if the touch hit a swipable
		Swipeable swipeable = GetSwipeable(raycastHits[touch.fingerId].Value);
			
		if (swipeable)
		{
			swipeable.OnSwipe(raycastHits[touch.fingerId].Value, direction);
			touchPositions[touch.fingerId].Clear();
			touchPositions[touch.fingerId].Add(touch.position);
		}
	}

	private Holdable GetHoldable(RaycastHit hit)
	{
		return hit.collider.GetComponent<Holdable>();
	}

	private Swipeable GetSwipeable(RaycastHit hit)
	{
		return hit.collider.GetComponent<Swipeable>();
	}

	private void CallHeldObjects(Touch[] touches)
	{
		// Check if holdables are held
		foreach (Touch touch in touches)
		{
			if (heldThisFrame[touch.fingerId])
			{
				if (heldThisFrame[touch.fingerId] == heldLastFrame[touch.fingerId])
				{
					heldThisFrame[touch.fingerId].timeHeld += Time.deltaTime;
					heldThisFrame[touch.fingerId].OnTouchHold(raycastHits[touch.fingerId].Value);
				}
				else
				{
					heldThisFrame[touch.fingerId].timeHeld = 0;
				}
			}
		}

		// Reset held holdables
		for (int i = 0; i < maxNumberTouches; i++)
		{
			heldLastFrame[i] = heldThisFrame[i];
			heldThisFrame[i] = null;
		}
	}
	#endregion

	#region DEBUG
	#if UNITY_EDITOR
	// MOUSE DEBUGGING
	// NOT COMPILED IN BUILDS
	private void CallMouseHeldObjects()
	{
		if (heldThisFrame[0])
		{
			if (heldLastFrame[0] == heldLastFrame[0])
			{
				heldThisFrame[0].timeHeld += Time.deltaTime;
				heldThisFrame[0].OnTouchHold(raycastHits[0].Value);
			}
			else
			{
				heldThisFrame[0].timeHeld = 0;
			}
		}
	}

	private void OnMouse()
	{
		Vector2 mousePos = Input.mousePosition;
		if (CastRayFromMousePos(mousePos))
		{
			// Check if the ray hit a holdable
			Holdable holdable = GetHoldable(raycastHits[0].Value);
			if (holdable)
			{
				heldThisFrame[0] = holdable;
				if (Input.GetMouseButtonDown(0))
				{
					holdable.OnTouchBegin(raycastHits[0].Value);
				}

				foreach (Holdable lastFrameHoldable in heldLastFrame)
				{
					if (lastFrameHoldable && !lastFrameHoldable.Equals(holdable))
					{
						lastFrameHoldable.OnTouchReleased();
					}
				}
			}
		}		
		else
		{
			foreach (Holdable lastFrameHoldable in heldLastFrame)
			{
				if (lastFrameHoldable)
				{
					lastFrameHoldable.OnTouchReleased();
				}
			}
		}
	}

	private void OnMouseUp()
	{
		Vector2 mousePos = Input.mousePosition;
		if (CastRayFromMousePos(mousePos))
		{
			// Check if the ray hit a holdable
			Holdable holdable = GetHoldable(raycastHits[0].Value);
			if (holdable)
			{
				holdable.OnTouchReleased();
			}
		}
	}

	private bool CastRayFromMousePos(Vector2 pos) 
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast(ray, out hit))
		{
			raycastHits[0] = hit;
			return true;
		}
		raycastHits[0] = null;
		return false;
	}
	#endif
	#endregion
}
