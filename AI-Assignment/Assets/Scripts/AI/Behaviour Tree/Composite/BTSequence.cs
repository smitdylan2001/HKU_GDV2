using UnityEngine;
public class BTSequence : BTBaseNode
{
	private BTBaseNode[] nodes;

	public BTSequence(params BTBaseNode[] inputNodes)
	{
		nodes = inputNodes;
	}

	public override TaskStatus Run()
	{
		foreach (BTBaseNode node in nodes)
		{
			TaskStatus result = node.Run();
			if (node.GetType().Name != "BTSequence" && node.GetType().Name != "BTInvert") EventManager<string>.InvokeEvent(EventType.OnRogueTextUpdate, node.GetType().Name);

			switch (result)
			{
				case TaskStatus.Failed: return TaskStatus.Failed;
				case TaskStatus.Success: continue;
				case TaskStatus.Running: return TaskStatus.Running;
			}
		}
		return TaskStatus.Success;
	}
}

