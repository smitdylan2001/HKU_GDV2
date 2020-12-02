using UnityEngine;

public class BTIsTargetClose : BTBaseNode
{
	private GameObject _target;
	private GameObject _user;

	public BTIsTargetClose(GameObject user,GameObject target)
	{
		_target = target;
		_user = user;
	}

	public override TaskStatus Run()
	{
		if(Vector3.Distance(_target.transform.position, _user.transform.position) > 4)
		{
			return TaskStatus.Failed;
		}
		return TaskStatus.Success;
	}
}
