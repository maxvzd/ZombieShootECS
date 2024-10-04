namespace BehaviourTrees.Zombie
{
    public class DeadZombieBehaviour : Node
    {
        private readonly ZombieBehaviourTree _tree;

        public DeadZombieBehaviour(ZombieBehaviourTree tree)
        {
            _tree = tree;
        }
        
        public override NodeState Evaluate()
        {
            _tree.isTreeEnabled = false;
            return State = NodeState.Success;
        }
    }
}