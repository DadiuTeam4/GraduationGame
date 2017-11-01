// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Navigator : MonoBehaviour 
{
	[HideInInspector] public Transform currentWaypoint; 

	public Transform waypoint;
	public bool autoRepath;

	private NavMeshAgent navMeshAgent;
	private bool destinationReached;

	private void Awake() 
	{
		navMeshAgent = GetComponent<NavMeshAgent>();	
	}

	private void Start()
	{
		navMeshAgent.autoRepath = autoRepath;
	}

	public void SetRandomDestination(StateController controller)
	{
		
	}

	public void Move(Vector3 direction)
	{
		navMeshAgent.Move(direction * navMeshAgent.speed * Time.deltaTime);
	}

	public void SetDestination()
	{
		SetDestination(waypoint);
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
					return true;
				}
			}
		}
		return false;
	}
}
