using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{
	[SerializeField] private VariableFloat _runSpeed;
	[SerializeField] private VariableFloat _walkSpeed;
	[SerializeField] private Transform[] _hidingSpots;
	[SerializeField] private GameObject _infoTextObject;

	private string _currentBehaviour;
	private TextMesh _infoText;
	private GameObject _playerReference;
	private GameObject _enemyReference;
	private GameObject _smoke;
	private NavMeshAgent _agent;
	private Animator _animator;
	private BTBaseNode _tree;
	private BTBaseNode _followBehaviour;
	private BTBaseNode _rescueBehaviour;


	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponentInChildren<Animator>();
		_infoText = _infoTextObject.GetComponent<TextMesh>();
		_playerReference = GameObject.Find("Player");
		_enemyReference = GameObject.Find("AI_Guard");
		_smoke = GameObject.Find("Smoke");
		_smoke.SetActive(false);

		BlackBoard.HasBeenAttacked = false;
	}

	private void Start()
	{
		_agent.stoppingDistance = 1f;
		//TODO: Create your Behaviour tree here

		_followBehaviour =
			new BTSequence(
					new BTInvert(new BTIsTargetClose(this.gameObject, _playerReference)),
					new BTGoToPlayer(_walkSpeed, _playerReference, _agent, this.gameObject),
					new BTWait(3f)
				);

		_rescueBehaviour =
			new BTSequence(
					new BTCheckPlayerAttacked(),
					new BTHide(_runSpeed, _hidingSpots, _agent, this.gameObject),
					new BTWait(1f),
					new BTThrowSmoke(_smoke, _enemyReference)
				 );

		_tree =
			new BTSequence(
					new BTInvert(_rescueBehaviour),
					_followBehaviour
				);

		EventManager<string>.AddListener(EventType.OnRogueTextUpdate, UpdateText);
	}

	private void FixedUpdate()
	{
		_tree?.Run();
		EventManager<string>.InvokeEvent(EventType.OnRogueTextUpdate, _currentBehaviour);
		_infoTextObject.transform.LookAt(Camera.main.transform);
	}

	private void UpdateText(string behaviourText)
	{
		_currentBehaviour = behaviourText;
		_infoText.text = "Action: " + _currentBehaviour;
	}
}
