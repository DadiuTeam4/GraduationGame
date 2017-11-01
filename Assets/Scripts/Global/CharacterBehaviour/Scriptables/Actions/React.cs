// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Character Behaviour/Actions/React")]
public class React : Action
{
	public Animation reactionAnimation;

	private Vector3 pointToLookAt;

	public override void Act(StateController controller)
	{
		ReactToEvent(controller);
	}

	private void ReactToEvent(StateController controller) 
	{
		// TODO: Look at something
	}
}