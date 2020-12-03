using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour, IDamageable
{
    public AISelector AISelector { get; private set; }
    public BlackBoard BlackBoard { get; private set; }

    private Animator        _animator;
    private NavMeshAgent    _agent;
    [SerializeField] private LayerMask      _obstacleMask;
    [SerializeField] private FloatValue     _health;
    [SerializeField] private VariableFloat  _walkSpeed;
    [SerializeField] private VariableFloat  _runSpeed;
    [SerializeField] private VariableFloat  _sightDegree;
    [SerializeField] private VariableFloat  _sightDistance;
    [SerializeField] private VariableFloat  _attackRange;
    [SerializeField] private GameObject     _playerReference;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void Start()
    {
        OnInitialize();
        _agent.stoppingDistance = 0.5f;
    }

    public void OnInitialize()
    {
        AISelector = GetComponent<AISelector>();
        BlackBoard = GetComponent<BlackBoard>();
        BlackBoard.OnInitialize();
        AISelector.OnInitialize(BlackBoard);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(null, 5);
        }
        var distance = BlackBoard.GetFloatVariableValue(VariableType.Distance);
        distance.Value = Vector3.Distance(transform.position, _playerReference.transform.position);

        AISelector.OnUpdate();
    }

    private void FixedUpdate()
	{
        FindPlayer();
	}

	public void TakeDamage(GameObject attacker, int damage)
	{
        _health.Value -= damage;
        AISelector.EvaluateBehaviours();
    }

    private void FindPlayer()
	{
        Collider[] targets = Physics.OverlapSphere(transform.position, _sightDistance.Value);

        foreach (Collider c in targets)
        {
            Transform target = c.transform;
            Vector3 direction = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, direction) < _sightDegree.Value / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, direction, distance, _obstacleMask))
                {
                    
                    if (c.name == "Player") BlackBoard.PlayerSeen = true; Debug.Log("player raycasted");
                    StartCoroutine(UnseePlayer());
                }
            }
        }
        Debug.Log(BlackBoard.PlayerSeen);
	}

    private IEnumerator UnseePlayer()
	{
        yield return new WaitForSeconds(3f);
        BlackBoard.PlayerSeen = false;
        yield return null;
	}

    private Vector3 DirFromAngle()
	{
        return new Vector3(Mathf.Sin(_sightDegree.Value * Mathf.Deg2Rad), 0, Mathf.Cos(_sightDegree.Value * Mathf.Deg2Rad));
	}

}
