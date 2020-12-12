using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : AIBehaviour
{
    [SerializeField] private Transform[] _patrolPath;
	[SerializeField] private FloatValue _health;
	private NavMeshAgent _agent;
	private Animator _animator;
	private int _currentSpot;

	private void Awake()
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

		_health.Value += Time.deltaTime / 10;
	}

	private void GoToNextSpot(Transform place)
	{
		_agent.SetDestination(place.position);
	}
}
