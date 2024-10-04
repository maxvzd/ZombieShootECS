using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees.Zombie
{
    public class GoToTargetBehaviour : Node
    {
        private readonly NavMeshAgent _navmeshAgent;

        public GoToTargetBehaviour(NavMeshAgent navmeshAgent)
        {
            _navmeshAgent = navmeshAgent;
        }

        public override NodeState Evaluate()
        {
            Vector3 target = PlayerReference.Player.position;
            _navmeshAgent.destination = target;
                
            if (_navmeshAgent.remainingDistance <= _navmeshAgent.stoppingDistance)
            {
                return State = NodeState.Success;
            }
            return State = NodeState.Running;
        }
    }
}