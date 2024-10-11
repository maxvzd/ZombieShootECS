namespace BehaviourTrees.Zombie
{
    public class DoesZombieHaveTarget : Node
    {
        public override NodeState Evaluate() => 
            GetData(Constants.TargetPositionData) is not null ? 
                State = NodeState.Success : 
                State = NodeState.Failure;
    }
}