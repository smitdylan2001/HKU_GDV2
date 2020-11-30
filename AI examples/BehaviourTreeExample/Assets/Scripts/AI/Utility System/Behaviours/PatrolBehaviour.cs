﻿using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : AIBehaviour
{
    [SerializeField] private Transform[] _patrolPath;
	private NavMeshAgent _agent;
	private Animator _animator;
	private int _currentSpot;

	private void Start()
	{
		_animator = GetComponentInChildren<Animator>();
		_agent = GetComponent<NavMeshAgent>();
		_currentSpot = 0;
	}

	public override void Execute()
    {
		_agent.speed = 1;
        if (_agent.remainingDistance <= 1f)
		{
			GoToNextSpot(_patrolPath[_currentSpot]);
			_currentSpot = (_currentSpot + 1) % 6;
		}
    }

	private void GoToNextSpot(Transform place)
	{
		_agent.SetDestination(place.position);
	}
}
