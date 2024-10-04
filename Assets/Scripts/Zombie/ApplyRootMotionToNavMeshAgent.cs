using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class ApplyRootMotionToNavMeshAgent : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private const float WALK_SPEED = 1f;
        private const float RUN_SPEED = 2f;

        private float _t;
        private float _speed;
        
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
            SynchroniseAnimatorAndAgent();
        }

        private void SynchroniseAnimatorAndAgent()
        {
            if (!_navMeshAgent.enabled) return;
            
            if (_navMeshAgent.remainingDistance >= _navMeshAgent.stoppingDistance)
            {
                _t += Time.deltaTime;
                _t = Mathf.Clamp(_t, 0, 1);
            }
            else if (_t > 0f)
            {
                _t -= Time.deltaTime;
                _t = Mathf.Clamp(_t, 0, 1);
            }
            _speed = Mathf.Lerp(0f, WALK_SPEED, _t);

            bool isMoving = _speed > 0.01f;
            _animator.SetFloat(Constants.Speed, _speed);
            _animator.SetBool(Constants.IsMoving, isMoving);
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