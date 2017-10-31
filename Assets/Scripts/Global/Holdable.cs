// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour 
{
	[HideInInspector]
	public float timeHeld;
	
	public virtual void OnTouchBegin(RaycastHit hit) {}
	public virtual void OnTouchHold(RaycastHit hit) {}
	public virtual void OnTouchReleased() {}
}
