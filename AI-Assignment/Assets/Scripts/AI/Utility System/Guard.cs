using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour, IDamageable
{
    public AISelector AISelector { get; private set; }
    public BlackBoard BlackBoard { get; private set; }

    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private GameObject _playerReference;
    [SerializeField] private FloatValue _health;
    [SerializeField] private VariableFloat _walkSpeed;
    [SerializeField] private VariableFloat _runSpeed;
    [SerializeField] private VariableFloat _sightDegree;
    [SerializeField] private VariableFloat _sightDistance;
    [SerializeField] private VariableFloat _attackRange;
    [SerializeField] private GameObject _infoTextObject;

    private TextMesh _infoText;
    private NavMeshAgent _agent;
    private Animator _animator;
    private string _currentBehaviour;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    void Start()
    {
        OnInitialize();
        BlackBoard.GuardBlinded = false;
        _agent.stoppingDistance = 0.5f;
        _infoText = _infoTextObject.GetComponent<TextMesh>();
        EventManager<string>.AddListener(EventType.OnGuardTextUpdate, UpdateText);
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
        FindPlayer();
    }

	private void FixedUpdate()
	{
        _infoTextObject.transform.LookAt(Camera.main.transform);
    }

    public void TakeDamage(GameObject attacker, int damage)
	{
        _health.Value -= damage;
        AISelector.EvaluateBehaviours();
    }

    private void FindPlayer()
	{
        if (!BlackBoard.GuardBlinded)
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
                        if (c.name == "Player")
                        {
                            BlackBoard.PlayerSeen = true;
                            StartCoroutine(UnseePlayer());
                            EventManager<string>.InvokeEvent(EventType.OnGuardTextUpdate, _currentBehaviour);
                        }
                    }
                }
            }
            if (Vector3.Distance(transform.position, _playerReference.transform.position) < 0.5) 
			{
                BlackBoard.PlayerSeen = true;
                StartCoroutine(UnseePlayer());
                EventManager<string>.InvokeEvent(EventType.OnGuardTextUpdate, _currentBehaviour);
            }
		}
	}

    private IEnumerator UnseePlayer()
	{
        yield return new WaitForSeconds(5f);
        BlackBoard.PlayerSeen = false;
        yield return null;
	}

    private void UpdateText(string behaviourText)
	{
        _currentBehaviour = behaviourText;
        _infoText.text = "Health: " + (int)_health.Value + "\n Action: " + _currentBehaviour + "\n IsBlinded: " + BlackBoard.GuardBlinded.ToString() + "\n SeesPlayer: " + BlackBoard.PlayerSeen.ToString();
	}
}
