using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AttackBehaviour : AIBehaviour
{
	[SerializeField] private GameObject _playerReference;
	[SerializeField] private GameObject[] _weapons;
	private Player _playerData;
	private NavMeshAgent _agent;
	private bool _hasWeapon;
	private bool _hasAttacked;
	private Animator _animator;

	private void Awake()
	{
		_playerData = _playerReference.GetComponent<Player>();
		_animator = GetComponentInChildren<Animator>();
		_agent = GetComponent<NavMeshAgent>();
		_hasWeapon = false;
	}

	public override void Execute()
	{
		_agent.speed = 2;

		if (!_hasWeapon) 
		{ 
			_weapons = _weapons.ToList().OrderBy(x => (x.transform.position).sqrMagnitude).ToArray();
			_agent.SetDestination(_weapons.First().transform.position); 
		}
		else if (_hasWeapon && _playerReference.activeSelf) 
		{ 
			_agent.SetDestination(_playerReference.transform.position); 
		}

		if (_agent.hasPath && _agent.remainingDistance <= 0.5f)
		{
			if (!_hasWeapon)
			{
				_hasWeapon = true;
				_weapons.First().SetActive(false);
			}
			else if (_playerReference.activeSelf && Vector3.Distance(transform.position, _playerReference.transform.position) < 0.5) //Extra position check to avoid false positives
			{
				if (!_hasAttacked)
				{
					_playerData.TakeDamage(this.gameObject, 10);
					_hasAttacked = true;
					StartCoroutine(RegenerateAttack());
				}
			}
		}
	}

	private IEnumerator RegenerateAttack()
	{
		yield return new WaitForSeconds(2f);

		_hasAttacked = false;

		yield return null;
	}
}
