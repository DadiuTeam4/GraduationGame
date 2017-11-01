// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Character Behaviour/Actions/Walk")]
public class Walk : Action
{
	public bool walkToWaypoint;
	public bool walkInDirection;
	public Vector3 direction;

	public override void Act(StateController controller)
	{
		WalkTowards(controller);
	}

	private void WalkTowards(StateController controller) 
	{
		if (walkToWaypoint)
		{
			controller.navigator.SetDestination();
		}

		if (walkInDirection)
		{
			controller.navigator.Move(direction);
		}
	}
}