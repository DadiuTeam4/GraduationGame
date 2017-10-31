// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour
{
    public virtual void OnShakeBegin() {}
    public virtual void OnShake() {}
    public virtual void OnShakeEnd() {}
}
