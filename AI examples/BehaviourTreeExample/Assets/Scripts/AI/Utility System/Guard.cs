using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour, IDamageable
{
    private NavMeshAgent _agent;
    private Animator _animator;
    [SerializeField] private FloatValue _health;
    [SerializeField] private VariableFloat _walkSpeed;
    [SerializeField] private VariableFloat _runSpeed;
    [SerializeField] private VariableFloat _sightDegree;
    [SerializeField] private VariableFloat _sightDistance;
    [SerializeField] private VariableFloat _attackRange;

    [SerializeField] private GameObject _playerReference;

    public AISelector AISelector { get; private set; }
    public BlackBoard BlackBoard { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    void Start()
    {
        _agent.stoppingDistance = 1f;
        OnInitialize();
    }

    public void OnInitialize()
    {
        AISelector = GetComponent<AISelector>();
        BlackBoard = GetComponent<BlackBoard>();
        BlackBoard.OnInitialize();
        AISelector.OnInitialize(BlackBoard);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(null, 5);
        }
        var distance = BlackBoard.GetFloatVariableValue(VariableType.Distance);
        distance.Value = Vector3.Distance(transform.position, _playerReference.transform.position);

        AISelector.OnUpdate();
    }

	public void TakeDamage(GameObject attacker, int damage)
	{
        _health.Value -= damage;
    }
}
