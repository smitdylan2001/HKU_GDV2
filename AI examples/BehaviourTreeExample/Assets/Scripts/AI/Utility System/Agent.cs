using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour, IDamageable
{
    public AIBehaviourSelector AISelector { get; private set; }
    public BlackBoard BlackBoard { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        OnInitialize();
    }

    public void OnInitialize()
    {
        AISelector = GetComponent<AIBehaviourSelector>();
        BlackBoard = GetComponent<BlackBoard>();
        BlackBoard.OnInitialize();
        AISelector.OnInitialize(BlackBoard);
    }

    // Update is called once per frame
    void Update()
    {
        AISelector.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(this.gameObject, 10);
        }
        var distance = BlackBoard.GetFloatVariableValue(VariableType.Distance);
        distance.Value = transform.position.magnitude;
    }

	public void TakeDamage(GameObject attacker, int damage)
	{
        FloatValue res = BlackBoard.GetFloatVariableValue(VariableType.Health);
        if (res)
        {
            res.Value -= damage;
        }
        AISelector.EvaluateBehaviours();
    }
}