using System.Collections.Generic;
using DealDamage;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourTrees.Zombie
{
    public class ZombieBehaviourTree : BehaviourTree
    {
        [SerializeField] private float sightRadius;
        [SerializeField] private float fov;
        [SerializeField] private Transform eyesTransform;
        
        protected override Node SetupTree()
        {
            NavMeshAgent navmeshAgent = GetComponent<NavMeshAgent>();
            MainHealthScript mainHealthScript = GetComponentInParent<MainHealthScript>();

            Transform currentTransform = transform;
            
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                   new IsZombieDeadCheck(mainHealthScript),
                   new DeadZombieBehaviour(this)
                }),
                
                new Sequence(new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new DoesZombieHaveTarget(),
                            new CanZombieSeeTarget(sightRadius, fov, eyesTransform, currentTransform)
                        }),
                        
                        new CanZombieSeeAnyTargets(sightRadius, fov, eyesTransform, currentTransform),
                        
                    }),
                    new GoToTargetBehaviour(navmeshAgent)
                }),
                
                new IdleBehaviour()
            });
            return root;
        }
    }
}