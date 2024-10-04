using System;
using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.AI;

namespace DealDamage
{
    public class MainHealthScript : MonoBehaviour
    {
        private PuppetMaster _ragdoll;
        private NavMeshAgent _navMeshAgent;
        private LimbHealth[] _limbs;
        
        public float Health => _health;

        private float _health = 100f;
        private bool _isDead;
        
        
        private void Start()
        {
            _ragdoll = GetComponentInChildren<PuppetMaster>();
            _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            
            _limbs = GetComponentsInChildren<LimbHealth>();
            foreach (LimbHealth limb in _limbs)
            {
                limb.LimbHit += OnLimbHit;
            }
        }

        private void OnLimbHit(object sender, HitEventArgs e)
        {
            if (_isDead) return;
            
            _health -= e.Damage;
            _health = Mathf.Max(0, _health);
            
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            _ragdoll.state = PuppetMaster.State.Dead;
            _navMeshAgent.enabled = false;

            foreach (LimbHealth limb in _limbs)
            {
                limb.zombieIsDead = true;
            }
        }
    }
}