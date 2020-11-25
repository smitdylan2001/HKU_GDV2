using UnityEngine;

public class BTWait : BTBaseNode
{
	private float _waitSeconds;
	private float _currentDuration = 0;

	public BTWait(float seconds)
	{
		_waitSeconds = seconds;
	}

	public override TaskStatus Run()
	{
		_currentDuration += Time.fixedDeltaTime;
		if (_currentDuration < _waitSeconds)
		{
			return TaskStatus.Running;
		}
		_currentDuration = 0;
		Debug.Log("Done Waiting");
		return TaskStatus.Success;
	}
}
