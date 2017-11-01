// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Behaviour/Decisions/DestinationReached")]
public class DestinationReached : Decision
{
	public override bool Decide(StateController controller) 
	{
		return controller.navigator.CheckDestinationReached();
	}
}
