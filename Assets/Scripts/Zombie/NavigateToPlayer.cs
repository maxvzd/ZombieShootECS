using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class NavigateToPlayer : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private NavMeshAgent _navMeshAgent;
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _navMeshAgent.destination = player.position;
        }
    }
}