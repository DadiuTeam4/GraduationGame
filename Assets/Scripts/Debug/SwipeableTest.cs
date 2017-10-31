// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeableTest : Swipeable 
{
	public override void OnSwipe(RaycastHit raycastHit, Vector3 direction)
	{
		print("Swiped at " + raycastHit.point + " with force: " + direction.magnitude);
	}
}
