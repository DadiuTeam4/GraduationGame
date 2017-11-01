// Author: Mathias Dam Hedelund
// Contributors: You Wu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour
{
    protected const float COEFFICIENT_FROM_MAGNITUDE_TO_FORCE = 0.005f;
    public virtual void OnShakeBegin(float magnitude) { }
    public virtual void OnShake(float magnitude) { }
    public virtual void OnShakeEnd() { }

    protected Vector3 GetShakeForceOnShakebleObject(float magnitude)
    {
        {
            return Random.insideUnitSphere * magnitude * COEFFICIENT_FROM_MAGNITUDE_TO_FORCE;
        }
    }

}
