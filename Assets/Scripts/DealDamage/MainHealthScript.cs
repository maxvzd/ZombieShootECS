using System;
using UnityEngine;

namespace DealDamage
{
    public class MainHealthScript : MonoBehaviour
    {
        public float Health => _health;

        private float _health = 100f;
        
        
        private void Start()
        {
            LimbHealth[] limbs = GetComponentsInChildren<LimbHealth>();
            foreach (LimbHealth limb in limbs)
            {
                limb.LimbHit += OnLimbHit;
            }
        }

        private void OnLimbHit(object sender, HitEventArgs e)
        {
            _health -= e.Damage;
            _health = Mathf.Max(0, _health);
            
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("I died");
        }
    }
}