using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlindedBehaviour : AIBehaviour
{
	[SerializeField] private Transform[] _hidingSpots;
	private NavMeshAgent _agent;
	private Animator _animator;

	private void Start()
	{
		_animator = GetComponentInChildren<Animator>();
		_agent = GetComponent<NavMeshAgent>();
	}

	public override void Execute()
    {
		_agent.speed = 0.5f;

		_agent.SetDestination(_hidingSpots[Random.Range(0, _hidingSpots.Length)].position);
    }
}
