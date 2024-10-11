using UnityEngine;

namespace BehaviourTrees.Zombie
{
    public class AttackCharacterBehaviour : Node
    {
        private readonly Animator _animator;

        public AttackCharacterBehaviour(Animator animator)
        {
            _animator = animator;
        }
        
        public override NodeState Evaluate()
        {
            _animator.SetTrigger(Constants.AttackTrigger);
            return State =  NodeState.Success;
        }
    }
}