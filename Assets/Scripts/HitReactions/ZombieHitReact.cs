﻿using System;
using RootMotion.Dynamics;
using UnityEngine;

namespace HitReactions
{
    public class ZombieHitReact : ReactToHit
    {
        private Rigidbody _rigidBody;
        private MuscleCollisionBroadcaster _collisionBroadcaster;
  
        public void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _collisionBroadcaster = _rigidBody.GetComponent<MuscleCollisionBroadcaster>();
        }

        public override void React(RaycastHit hit, Vector3 damageDirection)
        {
            Debug.DrawRay(hit.point, hit.normal * 0.1f, Color.green, 1f);
            
            _collisionBroadcaster.Hit(10, damageDirection * 10, hit.point);
        } 
    }
}