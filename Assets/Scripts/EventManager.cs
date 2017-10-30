using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomEvent
{
	test
}

public class EventManager : MonoBehaviour 
{
	private Dictionary<CustomEvent, List<EventDelegate>> listeners = new Dictionary<CustomEvent, List<EventDelegate>>();

	public delegate void EventDelegate();

	public void AddListener(CustomEvent eventName, EventDelegate newListener)
	{
		List<EventDelegate> eventList;
		if(listeners.ContainsKey(eventName))
		{
			listeners.TryGetValue(eventName, out eventList);

			eventList.Add(newListener);
		}
		else
		{
			eventList = new List<EventDelegate>();
			listeners.Add(eventName, eventList);
		}
	}

	public bool CallEvent(CustomEvent eventName)
	{
		if(listeners.ContainsKey(eventName))
		{
			List<EventDelegate> eventList = new List<EventDelegate>();
			
			foreach(EventDelegate auto in eventList)
			{
				auto();
			}

			return true;
		}

		return false;
	}
}
