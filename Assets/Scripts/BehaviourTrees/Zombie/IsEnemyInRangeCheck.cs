using UnityEngine;

namespace BehaviourTrees.Zombie
{
    public class IsEnemyInRangeCheck : Node
    {
        private readonly Transform _transform;

        public IsEnemyInRangeCheck(Transform myTransform)
        {
            _transform = myTransform;
        }
        
        public override NodeState Evaluate()
        {
            object target = GetData("target");
            if (target is null)
            {
                // TODO: improve detection
                float distance = Vector3.Distance(_transform.position, PlayerReference.Player.position);
                
                if (distance < 2)
                {
                    //TODO: Change parent.parent
                    Parent.Parent.AddData("target", PlayerReference.Player.transform.position);
                    return State = NodeState.Success;
                }

                return NodeState.Failure;
            }

            return State = NodeState.Success;
        }
    }
}