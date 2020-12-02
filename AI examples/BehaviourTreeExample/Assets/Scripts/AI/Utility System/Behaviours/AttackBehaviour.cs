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

	private void Awake()
	{
		_playerData = _playerReference.GetComponent<Player>();
		_animator = GetComponentInChildren<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}

	public override void Execute()
	{
		_agent.speed = 2;
		_agent.SetDestination(_playerReference.transform.position);
		Debug.Log(_agent.remainingDistance);
		if (_agent.hasPath && _agent.remainingDistance <= 1f && _playerReference.activeSelf)
		{
			Debug.Log("AATTACK");
			_playerData.TakeDamage(this.gameObject, 10);
		}
	}
}
