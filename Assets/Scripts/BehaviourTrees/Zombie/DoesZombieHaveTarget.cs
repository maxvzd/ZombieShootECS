namespace BehaviourTrees.Zombie
{
    public class DoesZombieHaveTarget : Node
    {
        public override NodeState Evaluate() => GetData(Constants.TargetData) is null ? NodeState.Failure : State = NodeState.Success;
    }
}