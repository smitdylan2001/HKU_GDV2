using UnityEngine;
using UnityEngine.AI;

public class BTHide : BTBaseNode
{
	private GameObject _user;
	private VariableFloat _runSpeed;
	private NavMeshAgent _agent;
	private Transform[] _hidingspots;

	public BTHide(VariableFloat runSpeed, Transform[] hidingSpots, NavMeshAgent agent, GameObject user)
	{
		_runSpeed = runSpeed;
		_hidingspots = hidingSpots;
		_agent = agent;
		_user = user;
	}

	public override TaskStatus Run()
	{
		Transform bestSpot = GetClosestSpot(_hidingspots);
		_agent.SetDestination(bestSpot.position);
		if (_agent.pathStatus != NavMeshPathStatus.PathComplete && _agent.remainingDistance <= 0)
		{
			return TaskStatus.Running;
		}
		return TaskStatus.Success;
	}

	Transform GetClosestSpot(Transform[] hidingSpots)
	{
		Transform bestTarget = null;
		_agent.speed = _runSpeed.Value;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = _user.transform.position;
		foreach (Transform spot in hidingSpots)
		{
			Vector3 directionToTarget = spot.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if (dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = spot;
			}
		}

		return bestTarget;
	}

}
