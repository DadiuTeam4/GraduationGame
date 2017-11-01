// Author: Itai Yavin
// Contributors: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Events;
public class TestEventManager : MonoBehaviour 
{	
	void Start () 
	{
		EventDelegate delegate1 = TestFunction;
		EventDelegate delegate2 = TestFunction2;
		EventDelegate delegate3 = TestFunction3;	

		EventManager.GetInstance().AddListener(CustomEvent.test, delegate1);
		EventManager.GetInstance().AddListener(CustomEvent.test, delegate2);
		EventManager.GetInstance().AddListener(CustomEvent.test, delegate3);

		EventManager.GetInstance().RemoveListener(CustomEvent.test, delegate2);

		EventArgument argument = new EventArgument();
		argument.stringComponent = "YOOO";

		EventManager.GetInstance().CallEvent(CustomEvent.test, argument);
	}

	public void TestFunction(EventArgument argument)
	{
		Debug.Log(argument.stringComponent);
	}

	public void TestFunction2(EventArgument argument)
	{
		Debug.Log("ALSO ME! " + argument.stringComponent);
	}

	public void TestFunction3(EventArgument argument)
	{
		Debug.Log("DONT FORGET ME! " + argument.stringComponent);
	}

}
