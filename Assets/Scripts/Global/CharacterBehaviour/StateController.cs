
// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

[RequireComponent(typeof(Navigator))]
public class StateController : MonoBehaviour
{
	public CustomEvent[] events;
	public State currentState;
	public bool active = true;

	[HideInInspector] public Navigator navigator;
	[HideInInspector] public static Animator animator;
	[HideInInspector] public Dictionary<CustomEvent, bool> triggeredEvents;

	private Dictionary<int, CustomEvent> eventIndexes;
	private State previousState;
	private float stateTimeElapsed;
	private int eventNumber;
	private EventDelegate[] eventOccurredCallbacks;

	#region DEBUG
	#if UNITY_EDITOR
	public bool aiDebugging = false;
	[HideInInspector] public string debugInfo;
	#endif
	#endregion

	private void Awake()
	{ 
		animator = GetComponent<Animator>();
		navigator = GetComponent<Navigator>();
	}

	private void Start()
	{
		triggeredEvents = new Dictionary<CustomEvent, bool>();
		eventIndexes = new Dictionary<int, CustomEvent>();
		eventOccurredCallbacks = new EventDelegate[events.Length];
		for (int i = 0; i < events.Length; i++) 
		{
			triggeredEvents.Add(events[i], false);
			eventIndexes.Add(i, events[i]);

			EventDelegate action = EventCallback;
			eventOccurredCallbacks[i] = action;
			EventManager.GetInstance().AddListener(events[i], action);
		}
	}

	public bool CheckEventOccured(CustomEvent eventName) 
	{
		bool eventOccured = triggeredEvents[eventName];
		triggeredEvents[eventName] = false; 
		return eventOccured;
	}

	private void EventCallback(EventArgument eventArgument)
	{
		CustomEvent triggeredEvent;
		if (eventIndexes.TryGetValue(eventArgument.intComponent, out triggeredEvent))
		{
			bool eventValue;
			if (triggeredEvents.TryGetValue(triggeredEvent, out eventValue)) 
			{
				triggeredEvents[eventIndexes[eventArgument.intComponent]] = true;
			}
			else 
			{
				print("Event " + triggeredEvent + " does not exist");
			}
		}
		else 
		{
			print("Event number " + eventArgument + " does not exist");
		}
	}

	public void TransitionToState(State nextState)
	{
		if (nextState != currentState) 
		{
			previousState = currentState;
			currentState = nextState;
			OnExitState();
		}
	}

	public void ReturnToPreviousState() 
	{
		TransitionToState(previousState);
	}

	private void Update()
	{
		if (!active) 
		{
			return;
		}
		#region DEBUG
		#if UNITY_EDITOR
		if (aiDebugging)
		{
			debugInfo = "";
			UpdateDebugInfo();
		}
		#endif
		#endregion
		//animator.SetFloat("speed", navigator.GetSpeed());
		currentState.UpdateState(this);
	}

	public bool CheckIfCountDownElapsed(float duration)
	{
		stateTimeElapsed += Time.deltaTime;
		return stateTimeElapsed >= duration;
	}

	public void ResetStateTimer() 
	{
		stateTimeElapsed = 0;
	}

	private void OnExitState()
	{
		ResetStateTimer();
	}

	public Transform GetDestination()
	{
		return navigator.GetDestination();
	}

	#region DEBUG
	#if UNITY_EDITOR
	private void UpdateDebugInfo()
	{
		debugInfo += ("State time elapsed:\t" + stateTimeElapsed + "\n");
		debugInfo += ("Current state:\t" + currentState.name + "\n");
		debugInfo += ("Current waypoint:\t" + navigator.GetDestination() + "\n");
		debugInfo += ("\nTriggered events:\n");
		foreach (CustomEvent eventName in events)
		{
			debugInfo += eventName + ": " + triggeredEvents[eventName] + "\n";
		}
	}

	void OnGUI()
	{
		if (aiDebugging)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), debugInfo, "");
		}
	}
	#endif
	#endregion
}
