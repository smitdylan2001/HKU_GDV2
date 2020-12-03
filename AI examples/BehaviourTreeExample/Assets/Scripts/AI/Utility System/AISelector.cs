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
        if (!BlackBoard.PlayerSeen) _attackBehaviour.IncreasedAmount = -20;
        else if (_attackBehaviour.IncreasedAmount < 0) _attackBehaviour.IncreasedAmount = 0;

        if (BlackBoard.GuardBlinded) _blindedBehaviour.IncreasedAmount = -20;
        else _blindedBehaviour.IncreasedAmount = 0;

        _behaviours = _behaviours.ToList().OrderByDescending(x => x.GetNormalizedScore()).ToArray();
        AIBehaviour newBehaviour = _behaviours.First();

        if (newBehaviour != _currentBehaviour)
		{
            newBehaviour = GetComponent<BlindedBehaviour>();
            Debug.Log(newBehaviour.GetType().Name);
            _currentBehaviour?.OnExit();
            _currentBehaviour = newBehaviour;
            _currentBehaviour.OnEnter();
            //_currentBehaviour.IncreasedAmount += 5;
		}
        Debug.Log(newBehaviour.GetType().Name);

    }

    public void OnUpdate()
    {
        StartCoroutine(asyncUpdate());
        EvaluateBehaviours();
        StartCoroutine(KeepBehaviourLonger(_currentBehaviour));
        _currentBehaviour?.Execute();
    }

    private IEnumerator asyncUpdate()
	{
        while (true)
		{
            
            yield return new WaitForSeconds(3f);
		}
    }

    private IEnumerator KeepBehaviourLonger(AIBehaviour behaviour)
    {
        if (behaviour.IncreasedAmount > 0) behaviour.IncreasedAmount -= Time.deltaTime /10;
        yield return null;
    }
}
