// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableTest : Holdable 
{
	public override void OnTouchBegin(RaycastHit hit)
	{
		print("Touch began at " + hit.point);
	}

	public override void OnTouchHold(RaycastHit hit)
	{
		print("Touch held at " + hit.point);
	}

	public override void OnTouchReleased()
	{
		print("Touch released");
	}
}
