using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DealDamage
{
    public class LimbHealth : MonoBehaviour
    {
        public float Health => _health;
        
        [SerializeField] private bool isLethal;
        [SerializeField] private float limbDamageModifier = 1f;
        [SerializeField] private LimbDeathEffect deathEffect;
        [SerializeField] private string limbName;

        private bool _deathEffectApplied;
        private float _health = 100f;

        public delegate void LimbHitEventHandler(object sender, HitEventArgs e);

        public event LimbHitEventHandler LimbHit;

        public void Receive(float damage, Vector3 hitLocation)
        {
            float damageWithModifier = damage * limbDamageModifier;
            _health -= damageWithModifier;
            _health = Mathf.Max(0, _health);
            Debug.Log($"You hit my {limbName} for {damageWithModifier}");

            if (deathEffect is not null && _health <= 0 && !_deathEffectApplied)
            {
                deathEffect.Apply();
                _deathEffectApplied = true;
            }
            
            if (isLethal)
            {
                LimbHit?.Invoke(this, new HitEventArgs(damageWithModifier, hitLocation, limbName));
            }
        }
    }

    public class HitEventArgs : EventArgs
    {
        public float Damage { get; }
        public Vector3 HitLocation { get; }
        
        public string LimbName { get; }

        public HitEventArgs(float damage, Vector3 hitLocation, string limbName)
        {
            Damage = damage;
            HitLocation = hitLocation;
            LimbName = limbName;
        }
    }
}