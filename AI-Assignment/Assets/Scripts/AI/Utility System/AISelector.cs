using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AISelector : MonoBehaviour
{
    private AIBehaviour[] _behaviours;
    private AIBehaviour _currentBehaviour;
    private AIBehaviour _blindedBehaviour;
    private AIBehaviour _attackBehaviour;

    public void OnInitialize(BlackBoard bb)
	{
        _behaviours = GetComponents<AIBehaviour>();
        _attackBehaviour = GetComponent<AttackBehaviour>();
        _blindedBehaviour = GetComponent<BlindedBehaviour>();
        _currentBehaviour = GetComponent<PatrolBehaviour>();   //This is to avoid a null reference error when there is no behavior assigned and KeepBehaviourLonger runs
        foreach (AIBehaviour bhv in _behaviours)
		{
            bhv.OnInitialize(bb);
		}
	}

    public void EvaluateBehaviours()
	{
        if (!BlackBoard.PlayerSeen) _attackBehaviour.IncreasedAmount = -20;
        else if (_attackBehaviour.IncreasedAmount < 0) _attackBehaviour.IncreasedAmount = 0;

        if (BlackBoard.GuardBlinded) _blindedBehaviour.IncreasedAmount = 20;
        else _blindedBehaviour.IncreasedAmount = -20;

        _behaviours = _behaviours.ToList().OrderByDescending(x => x.GetNormalizedScore()).ToArray();
        AIBehaviour newBehaviour = _behaviours.First();

        if (newBehaviour != _currentBehaviour)
		{   
            Debug.Log(newBehaviour.GetType().Name);
            _currentBehaviour?.OnExit();
            _currentBehaviour = newBehaviour;
            _currentBehaviour.OnEnter();
            _currentBehaviour.IncreasedAmount += .5f;
		}
        EventManager<string>.InvokeEvent(EventType.OnGuardTextUpdate, _currentBehaviour.GetType().Name);
    }

    public void OnUpdate()
    {
        StartCoroutine(asyncUpdate());
        StartCoroutine(KeepBehaviourLonger(_currentBehaviour));
        _currentBehaviour?.Execute();
    }

    private IEnumerator asyncUpdate()
	{
        while (true)
		{
            EvaluateBehaviours();
            yield return new WaitForSeconds(3f);
		}
    }

    private IEnumerator KeepBehaviourLonger(AIBehaviour behaviour)
    {
        if (behaviour.IncreasedAmount > 0) behaviour.IncreasedAmount -= Time.deltaTime /10;
        yield return null;
    }
}
