using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckPlayerAttacked : BTBaseNode
{
	public BTCheckPlayerAttacked()
	{

	}

	public override TaskStatus Run()
	{
		if (BlackBoard.HasBeenAttacked) return TaskStatus.Success;
		return TaskStatus.Failed;
	}
}
