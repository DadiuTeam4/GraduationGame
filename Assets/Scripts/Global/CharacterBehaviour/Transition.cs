// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition 
{
	public Decision decision;
	public bool returnToPreviousState;
	[Header("Ignored if Return To Previous State is true")]
	public State trueState;
	public State falseState;
}
