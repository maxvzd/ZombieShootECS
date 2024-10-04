using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class NavigateToPlayer : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!_navMeshAgent.enabled) return;
            
            _navMeshAgent.destination = PlayerReference.Player.position;
        }
    }
}