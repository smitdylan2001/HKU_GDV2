using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInvert : BTBaseNode
{
	private TaskStatus _status;
	private BTBaseNode _node;

	public BTInvert(BTBaseNode node)
	{
		_node = node;
	}

	public override TaskStatus Run()
	{
		_status = _node.Run();

		if (_status == TaskStatus.Failed) return TaskStatus.Success;
		else if (_status == TaskStatus.Success) return TaskStatus.Failed;
		return TaskStatus.Running;
	}
}
