﻿using System;
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

        private float _health = 100f;
        public bool zombieIsDead;
        private bool _limbIsDead;

        public delegate void LimbHitEventHandler(object sender, HitEventArgs e);

        public event LimbHitEventHandler LimbHit;

        public void Receive(float damage, Vector3 hitLocation)
        {
            if (zombieIsDead) return;
            
            float damageWithModifier = damage * limbDamageModifier;
            
            if (isLethal)
            {
                LimbHit?.Invoke(this, new HitEventArgs(damageWithModifier, hitLocation, limbName));
            }

            if (_limbIsDead) return;
            
            _health -= damageWithModifier;
            _health = Mathf.Max(0, _health);

            if (deathEffect is not null && _health <= 0)
            {
                deathEffect.Apply();
                _limbIsDead = true;
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