using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlindedBehaviour : AIBehaviour
{
	[SerializeField] private Transform[] _hidingSpots;
	private NavMeshAgent _agent;
	private GameObject _smoke;
	private Animator _animator;
	private bool _hasSelectedLocation;

	private void Awake()
	{
		_animator = GetComponentInChildren<Animator>();
		_agent = GetComponent<NavMeshAgent>();
		_smoke = GameObject.Find("Smoke");
		_hasSelectedLocation = false;
	}

	public override void Execute()
    {
		BlackBoard.PlayerSeen = false;
		_agent.speed = 0.5f;
		StartCoroutine(Hide());
		if (_agent.remainingDistance <= _agent.stoppingDistance)
		{
			BlackBoard.GuardBlinded = false;
			_hasSelectedLocation = false;
			_smoke.SetActive(false);
		}
    }

	public IEnumerator Hide()
	{
		yield return new WaitForSeconds(3f);
		if (!_hasSelectedLocation)
		{
			_agent.SetDestination(_hidingSpots[Random.Range(0, _hidingSpots.Length)].position);
			_hasSelectedLocation = true;
		}
		BlackBoard.HasBeenAttacked = false;
		
		yield return null;
	}
}
