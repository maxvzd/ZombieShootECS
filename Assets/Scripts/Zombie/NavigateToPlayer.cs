using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class NavigateToPlayer : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
    
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _navMeshAgent.destination = player.position;
            _animator.SetFloat(Constants.Speed, _navMeshAgent.velocity.magnitude);
        }
    }
}
