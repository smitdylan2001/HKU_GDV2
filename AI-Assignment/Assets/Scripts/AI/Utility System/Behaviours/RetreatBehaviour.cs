﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class RetreatBehaviour : AIBehaviour
{
	[SerializeField] private Transform[] _hidingSpots;
	[SerializeField] private FloatValue _health;
	private NavMeshAgent _agent;
	private Animator _animator;

	private void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponentInChildren<Animator>();
	}
	public override void Execute()
	{
		_agent.speed = 4;
		_hidingSpots = _hidingSpots.OrderBy(x => (x.position).sqrMagnitude).ToArray();
		_agent.SetDestination(_hidingSpots.Last().position);

		if (_agent.remainingDistance <= 1) _health.Value += Time.deltaTime*2;
	}
}
