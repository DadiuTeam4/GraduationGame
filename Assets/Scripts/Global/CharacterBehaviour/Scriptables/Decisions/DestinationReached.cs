// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HugoAI 
{
	[CreateAssetMenu(menuName = "HugoAI/Decisions/DestinationReached")]
	public class DestinationReached : Decision 
	{
		public override bool Decide(StateController controller) 
		{
			return controller.navigator.CheckDestinationReached();
		}
	}
}