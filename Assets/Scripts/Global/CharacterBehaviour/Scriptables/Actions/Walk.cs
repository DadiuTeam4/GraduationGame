// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Character Behaviour/Actions/Walk")]
public class Walk : Action
{
	public override void Act(StateController controller)
	{
		SetWaypoint(controller);
	}

	private void SetWaypoint(StateController controller) 
	{
		
	}
}