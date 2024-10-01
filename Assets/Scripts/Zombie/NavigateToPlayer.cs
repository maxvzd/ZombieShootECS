using System;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class NavigateToPlayer : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private Vector2 _velocity;
        private Vector2 _smoothDeltaPos;
    
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updatePosition = false;
            _navMeshAgent.updateRotation = true;
            
            _animator = GetComponent<Animator>();
            _animator.applyRootMotion = true;
        }

        private void Update()
        {
            _navMeshAgent.destination = player.position;

            SynchroniseAnimatorAndAgent();
        }

        private void SynchroniseAnimatorAndAgent()
        {
            Vector3 worldDeltaPos = _navMeshAgent.nextPosition - transform.position;
            worldDeltaPos.y = 0;

            float dx = Vector3.Dot(transform.right, worldDeltaPos);
            float dy = Vector3.Dot(transform.forward, worldDeltaPos);
            Vector2 deltaPos = new Vector2(dx, dy);

            float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
            _smoothDeltaPos = Vector2.Lerp(_smoothDeltaPos, deltaPos, smooth);
            _velocity = _smoothDeltaPos / Time.deltaTime;

            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _velocity = Vector2.Lerp(Vector2.zero, _velocity, _navMeshAgent.remainingDistance);
            }

            bool isMoving = _navMeshAgent.velocity.magnitude > 0f && _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance;
            
            _animator.SetFloat(Constants.Speed, _velocity.magnitude);
            _animator.SetBool(Constants.IsMoving, isMoving);
            
            // float deltaMagnitude = worldDeltaPos.magnitude;
            // if (deltaMagnitude > _navMeshAgent.radius / 2f)
            // {
            //     transform.position = Vector3.Lerp(
            //         _animator.rootPosition,
            //         _navMeshAgent.nextPosition,
            //         smooth);
            // }
        }

        private void OnAnimatorMove()
        {
            Vector3 rootPos = _animator.rootPosition;
            rootPos.y = _navMeshAgent.nextPosition.y;
            transform.position = rootPos;
            _navMeshAgent.nextPosition = rootPos;
        }
    }
}
