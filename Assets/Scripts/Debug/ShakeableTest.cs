// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeableTest : Shakeable 
{
	public override void OnShakeBegin(float magnitude)
	{
		print("Shake begun with magnitude: " + magnitude);
	}

	public override void OnShake(float magnitude)
	{
		print("Shaking with magnitude: " + magnitude);
	}

	public override void OnShakeEnd()
	{
		print("Shaking stopped");
	}
}
