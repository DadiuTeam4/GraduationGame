// Author: Mathias Dam Hedelund
// Contributors:

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : Singleton<InputSystem>
{
	private readonly List<Holdable> heldLastFrame = new List<Holdable>();
	private readonly List<Holdable> heldThisFrame = new List<Holdable>();

	#region DEBUG
	#if UNITY_EDITOR
	// MOUSE DEBUGGING
	// NOT COMPILED IN BUILDS
	bool mouseDownLastFrame;
	#endif
	#endregion

	#region UPDATE_LOOP
	private void Update()
	{
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
					TouchEnded();
					break;
				}

				case (TouchPhase.Canceled):
				{
					TouchCanceled();
					break;
				}
			}
		}
		
		#region DEBUG
		#if UNITY_EDITOR
		// MOUSE DEBUGGING
		// NOT COMPILED IN BUILDS
		if (Input.GetMouseButton(0)) 
		{
			Vector2 mousePos = Input.mousePosition;
			Holdable holdable = CastRayFromMousePos(mousePos);
			if (holdable)
			{
				heldThisFrame.Add(holdable);
				if (Input.GetMouseButtonDown(0)) 
				{
					if (!mouseDownLastFrame)
					{
						holdable.OnTouchBegin();
					}
				}

				foreach (Holdable lastFrameHoldable in heldLastFrame)
				{
					if (!lastFrameHoldable.Equals(holdable))
					{
						lastFrameHoldable.OnTouchReleased();
					}
				}
			}		
			else
			{
				foreach (Holdable lastFrameHoldable in heldLastFrame)
				{
					lastFrameHoldable.OnTouchReleased();
				}
			}
			mouseDownLastFrame = true;
		}

		else if (Input.GetMouseButtonUp(0))
		{
			Vector2 mousePos = Input.mousePosition;
			Holdable holdable = CastRayFromMousePos(mousePos);
			if (holdable)
			{
				holdable.OnTouchReleased();
				mouseDownLastFrame = false;


			}
		}
		#endif
		#endregion

        CallHeldObjects();
	}
	#endregion

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
		Holdable holdable = CastRayFromTouch(touch);
		if (holdable)
		{
			holdable.OnTouchBegin();
			holdable.GiveTouchFeedback();
		}
	}

	private void TouchStationary(Touch touch)
	{
        Holdable holdable = CastRayFromTouch(touch);
        if (holdable)
        {
            heldThisFrame.Add(holdable);
        }
    }

	private void TouchMoved(Touch touch) 
	{
		Holdable holdable = CastRayFromTouch(touch);
		if (holdable) 
		{
			heldThisFrame.Add(holdable);

			foreach (Holdable lastFrameHoldable in heldLastFrame)
			{
				if (!lastFrameHoldable.Equals(holdable))
				{
					lastFrameHoldable.OnTouchReleased();
				}
			}
		}
	}

	private void TouchEnded() 
	{
		foreach (Holdable holdable in heldLastFrame) 
		{
			holdable.OnTouchReleased();
		}
	}

	private void TouchCanceled() 
	{
		foreach (Holdable holdable in heldLastFrame) 
		{
			holdable.OnTouchReleased();
		}
	}

	private Tuple<Holdable, RaycastHit> CastRayFromTouch(Touch touch)
	{
		Holdable holdableHit = null;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(touch.position);
		if (Physics.Raycast(ray, out hit)) 
		{
			holdableHit = hit.collider.GetComponent<Holdable>();
		}
		return holdableHit;
	}

	private void CallHeldObjects()
	{
		// Check if holdables are held
		foreach (Holdable holdable in heldThisFrame)
		{
			if (heldLastFrame.Contains(holdable))
			{
				holdable.timeHeld += Time.deltaTime;
				holdable.OnTouchHold();
			}
			else
			{
				holdable.timeHeld = 0;
			}
		}

		// Reset held holdables
		heldLastFrame.Clear();
		foreach (Holdable holdable in heldThisFrame) 
		{
			heldLastFrame.Add(holdable);
		}

		heldThisFrame.Clear();
	}

	#region DEBUG
	#if UNITY_EDITOR
	// MOUSE DEBUGGING
	// NOT COMPILED IN BUILDS
	private Holdable CastRayFromMousePos(Vector2 pos) 
	{
		Holdable holdableHit = null;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(pos);
		if (Physics.Raycast(ray, out hit))
		{
			lastRaycastHit = hit.point;
			holdableHit = hit.collider.GetComponent<Holdable>();
		}
		return holdableHit;
	}
	#endif
	#endregion
}
