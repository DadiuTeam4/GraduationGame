// Author: Itai Yavin
// Contributor:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> 
{
	private Dictionary<CustomEvent, List<EventDelegate>> listeners = new Dictionary<CustomEvent, List<EventDelegate>>();

	public delegate void EventDelegate(EventArgument argument);

	public void AddListener(CustomEvent eventName, EventDelegate newListener)
	{
		List<EventDelegate> eventList;
		if (listeners.ContainsKey(eventName))
		{
			listeners.TryGetValue(eventName, out eventList);

			eventList.Add(newListener);
		}
		else
		{
			eventList = new List<EventDelegate>();
			eventList.Add(newListener);
			listeners.Add(eventName, eventList);
		}
	}

	public bool RemoveListener(CustomEvent eventName, EventDelegate oldListener)
	{
		List<EventDelegate> eventList;
		if (listeners.ContainsKey(eventName))
		{
			listeners.TryGetValue(eventName, out eventList);

			foreach (EventDelegate eventDelegate in eventList)
			{
				if (eventDelegate == oldListener)
				{
					eventList.Remove(eventDelegate);
					
					return true;
				}
			}
		}

		return false;
	}

	public bool CallEvent(CustomEvent eventName, EventArgument argument)
	{
		if (listeners.ContainsKey(eventName))
		{
			List<EventDelegate> eventList;
			listeners.TryGetValue(eventName, out eventList);
			
			foreach(EventDelegate auto in eventList)
			{
				auto(argument);
			}

			return true;
		}

		return false;
	}

	public enum CustomEvent
	{
		test
	}

	public class EventArgument
	{
		public string stringComponent = "";
		public float floatComponent = 0.0f;
	}
}
