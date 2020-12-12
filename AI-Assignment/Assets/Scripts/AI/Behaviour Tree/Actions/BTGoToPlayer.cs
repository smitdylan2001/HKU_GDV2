using UnityEngine;
using UnityEngine.AI;

public class BTGoToPlayer : BTBaseNode
{
	private VariableFloat _walkSpeed;
	private GameObject _player;
	private NavMeshAgent _agent;
	private GameObject _user;

	public BTGoToPlayer(VariableFloat walkSpeed, GameObject player, NavMeshAgent agent, GameObject user)
	{
		_walkSpeed = walkSpeed;
		_player = player;
		_agent = agent;
		_user = user;
	}

	public override TaskStatus Run()
	{
		_agent.speed = _walkSpeed.Value;
		_agent.SetDestination(_player.transform.position);
		if (_agent.remainingDistance > _agent.stoppingDistance)
		{
			return TaskStatus.Running;
		}
		return TaskStatus.Success;
	}
}
