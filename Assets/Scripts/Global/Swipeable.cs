// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipeable : MonoBehaviour 
{
	public virtual void OnSwipe(RaycastHit raycastHit, Vector3 direction) {}
}
