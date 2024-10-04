using System.Collections.Generic;
using DealDamage;
using UnityEngine.AI;

namespace BehaviourTrees.Zombie
{
    public class ZombieBehaviourTree : Tree
    {
        protected override Node SetupTree()
        {
            NavMeshAgent navmeshAgent = GetComponent<NavMeshAgent>();
            MainHealthScript mainHealthScript = GetComponentInParent<MainHealthScript>();
            
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                   new IsZombieDeadCheck(mainHealthScript),
                   new DeadZombieBehaviour(this)
                }),
                
                new Sequence(new List<Node>
                {
                    new IsEnemyInRangeCheck(transform),
                    new GoToTargetBehaviour(navmeshAgent)
                }),
                
                new IdleBehaviour()
            });
            return root;
        }
    }
}