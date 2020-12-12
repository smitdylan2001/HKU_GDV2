using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class BTHide : BTBaseNode
{
	private GameObject _user;
	private NavMeshAgent _agent;
	private VariableFloat _runSpeed;
	private Transform[] _hidingSpots;

	public BTHide(VariableFloat runSpeed, Transform[] hidingSpots, NavMeshAgent agent, GameObject user)
	{
		_user = user;
		_agent = agent;
		_runSpeed = runSpeed;
		_hidingSpots = hidingSpots;
	}

	public override TaskStatus Run()
	{
		_agent.speed = _runSpeed.Value;

		_hidingSpots = _hidingSpots.OrderBy(x => (x.position).sqrMagnitude).ToArray();
		_agent.SetDestination(_hidingSpots.Last().position);

		if (_agent.remainingDistance > _agent.stoppingDistance)
		{
			return TaskStatus.Running;
		}
		return TaskStatus.Success;
	}
}
