using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AISelector : MonoBehaviour
{
    private AIBehaviour[] _behaviours;
    private AIBehaviour _currentBehaviour;

    public void OnInitialize(BlackBoard bb)
	{
        _behaviours = GetComponents<AIBehaviour>();

        foreach(AIBehaviour bhv in _behaviours)
		{
            bhv.OnInitialize(bb);
		}
	}

    public void EvaluateBehaviours()
	{
        AIBehaviour newBehaviour = _behaviours.ToList().OrderByDescending(x => x.GetNormalizedScore()).First();

        if (BlackBoard.GuardBlinded)
		{
            newBehaviour = GetComponent<BlindedBehaviour>();
            Debug.Log(newBehaviour.GetType().Name);
            _currentBehaviour?.OnExit();
            _currentBehaviour = newBehaviour;
            _currentBehaviour.OnEnter();
        }
        else if (newBehaviour != _currentBehaviour)
		{
            Debug.Log(newBehaviour.GetType().Name);
            _currentBehaviour?.OnExit();
            _currentBehaviour = newBehaviour;
            _currentBehaviour.OnEnter();
		}
	}

    public void OnUpdate()
    {
        EvaluateBehaviours();
        _currentBehaviour?.Execute();
    }
}
