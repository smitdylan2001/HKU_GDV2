using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Player : MonoBehaviour, IDamageable
{
    public Transform Camera;

    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _deathForce = 1000;
    [SerializeField] private GameObject _ragdoll;
    [SerializeField] private Guard _guardData;
    [SerializeField] private GameObject _infoTextObject;
    [SerializeField] private float _health;
    public float Health
    {
        get{ return _health; }
        private set {
            if (value > 100){ _health = 100; }
            if (value < 0) { _health = 0; }
        }
    }

    private TextMesh _infoText;
    private Rigidbody _rb;
    private Animator _animator;
    private float _vert = 0;
    private float _hor = 0;
    private Vector3 _moveDirection;
    private Collider _mainCollider;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _mainCollider = GetComponent<Collider>();
        _guardData = GameObject.Find("AI_Guard").GetComponent<Guard>();
        _infoText = _infoTextObject.GetComponent<TextMesh>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rib in rigidBodies)
        {
            rib.isKinematic = true;
            rib.useGravity = false;
        }

        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            if (col.isTrigger) { continue; }
            col.enabled = false;
        }

        _mainCollider.enabled = true;
        _rb.isKinematic = false;
    }

    void Update()
    {
        _vert = Input.GetAxis("Vertical");
        _hor = Input.GetAxis("Horizontal");

        Vector3 forwardDirection = Vector3.Scale(new Vector3(1, 0, 1), Camera.transform.forward);
        Vector3 rightDirection = Vector3.Cross(Vector3.up, forwardDirection.normalized);

        _moveDirection = forwardDirection.normalized * _vert + rightDirection.normalized * _hor;
        if (_moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_moveDirection.normalized, Vector3.up), _rotationSpeed * Time.deltaTime);
        }
        transform.position += _moveDirection.normalized * _moveSpeed * Time.deltaTime;

        bool isMoving = _hor != 0 || _vert != 0;
        ChangeAnimation(isMoving ? "Walk Crouch" : "Crouch Idle", isMoving ? 0.05f : 0.15f);

        if (Input.GetKeyDown(KeyCode.Space)) _guardData.TakeDamage(this.gameObject, 10);

        _infoTextObject.transform.position = transform.position + 2 * transform.up;
    }

    private void FixedUpdate()
    {
        UpdateText();
        _infoTextObject.transform.LookAt(UnityEngine.Camera.main.transform);
    }

    public void TakeDamage(GameObject attacker, int damage)
	{
		Health -= damage;
        BlackBoard.HasBeenAttacked = true;

        //RAGDOLL CODE
		//_animator.enabled = false;
		//var cols = GetComponentsInChildren<Collider>();
		//foreach (Collider col in cols)
		//{
		//	col.enabled = true;
		//}
		//_mainCollider.enabled = false;

		//var rigidBodies = GetComponentsInChildren<Rigidbody>();
		//foreach (Rigidbody rib in rigidBodies)
		//{
		//	rib.isKinematic = false;
		//	rib.useGravity = true;
		//	rib.AddForce(Vector3.Scale(new Vector3(1, 0.5f, 1), (transform.position - attacker.transform.position).normalized * _deathForce));
		//}
		//_ragdoll.transform.SetParent(null);
		//gameObject.SetActive(false);
    }

    private void GetComponentsRecursively<T>(GameObject obj, ref List<T> components)
    {
        T component = obj.GetComponent<T>();
        if(component != null)
        {
            components.Add(component);
        }

        foreach(Transform t in obj.transform)
        {
            if(t.gameObject == obj) { continue; }
            GetComponentsRecursively<T>(t.gameObject, ref components);
        }
    }

    private void ChangeAnimation(string animationName, float fadeTime)
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && !_animator.IsInTransition(0))
        {
            _animator.CrossFade(animationName, fadeTime);
        }
    }

    private void UpdateText()
    {
        _infoText.text = "Health: " + Health + "\n IsHit: " + BlackBoard.HasBeenAttacked.ToString();
    }
}
