// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour
{
    public virtual void OnShakeBegin(float magnitude) {}
    public virtual void OnShake(float magnitude) {}
    public virtual void OnShakeEnd() {}
}
