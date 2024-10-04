using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Sequence : Node
    {
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;

            foreach (Node child in Children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failure:
                        State = NodeState.Failure;
                        return State;
                    case NodeState.Running:
                        isAnyChildRunning = true;
                        continue;
                    case NodeState.Success:
                        continue;
                    default:
                        State = NodeState.Success;
                        return State;
                }
            }

            return State = isAnyChildRunning ? NodeState.Running : NodeState.Success;
        }
    }
}