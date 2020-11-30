using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackBehaviour : AIBehaviour
{
	[SerializeField] private GameObject _playerReference;
	private Player _playerData;
	private NavMeshAgent _agent;
	private Animator _animator;

	private void Start()
	{
		_playerData = _playerReference.GetComponent<Player>();
		_animator = GetComponentInChildren<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}

	public override void Execute()
	{
		_agent.speed = 3;
		_agent.SetDestination(_playerReference.transform.position);

		if (_agent.remainingDistance <= 1f && _playerReference.activeSelf)
		{
			_playerData.TakeDamage(this.gameObject, 10);
		}
	}
}
