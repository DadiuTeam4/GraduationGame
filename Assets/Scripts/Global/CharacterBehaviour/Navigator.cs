// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour 
{
	[HideInInspector] public Transform currentWaypoint; 

	private NavMeshAgent navMeshAgent;
	private bool destinationReached;
	private bool randomSet;
	private int randomWayPoint;

	private void Awake() 
	{
		navMeshAgent = GetComponent<NavMeshAgent>();	
	}

	public void SetRandomDestination(StateController controller)
	{
		
	}

	public void SetDestination(Transform destination) 
	{
		currentWaypoint = destination;
		navMeshAgent.SetDestination(destination.position);
	}

	public Transform GetDestination()
	{
		return currentWaypoint;
	}

	public float GetSpeed()
	{
		return navMeshAgent.velocity.magnitude;
	}

	public bool CheckDestinationReached() 
	{
		if (!navMeshAgent.pathPending)
		{
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					randomSet = false;
					return true;
				}
			}
		}
		return false;
	}
}
