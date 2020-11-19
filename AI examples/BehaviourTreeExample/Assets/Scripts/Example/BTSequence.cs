using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTExample
{
    public enum BTNodeStatus { Success, Running, Failed }

    public class BTSequence : BTBaseNode
    {
        private BTBaseNode[] nodes;
        public BTSequence(params BTBaseNode[] inputNodes)
        {
            nodes = inputNodes;
        }

        public override BTNodeStatus Run()
        {
            foreach(BTBaseNode node in nodes)
            {
                BTNodeStatus result = node.Run();
                switch (result)
                {
                    case BTNodeStatus.Failed: return BTNodeStatus.Failed;
                    case BTNodeStatus.Success: continue;
                    case BTNodeStatus.Running: return BTNodeStatus.Running;
                }
            }
            return BTNodeStatus.Success;
        }
    }

    public class BTMove : BTBaseNode
    {
        private VariableFloat moveSpeed;
        public BTMove(VariableFloat moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }

        public override BTNodeStatus Run()
        {
            moveSpeed.Value = 3;
            return BTNodeStatus.Success;
        }
    }
    public class BTAnimate : BTBaseNode
    {
        public BTAnimate() { }

        public override BTNodeStatus Run()
        {
            return BTNodeStatus.Success;
        }
    }

    public abstract class BTBaseNode
    {
        public abstract BTNodeStatus Run();
    }

    public class SomeAI : MonoBehaviour
    {

        [SerializeField] private VariableFloat moveSpeed;

        private BTBaseNode behaviourTree;
        private void Start()
        {
            moveSpeed = Instantiate(moveSpeed);


            behaviourTree = 
                new BTSequence(
                    new BTAnimate(),
                    new BTMove(moveSpeed),
                    new BTAnimate(),
                    new BTSequence(
                        new BTAnimate(),
                        new BTMove(moveSpeed),
                        new BTAnimate()
                    )
                );
        }

    }
}
