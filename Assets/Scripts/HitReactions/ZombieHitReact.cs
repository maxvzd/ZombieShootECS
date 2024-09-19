using System;
using UnityEngine;

namespace HitReactions
{
    public class ZombieHitReact : ReactToHit
    {
        private Rigidbody _rigidBody;

        public void Start()
        {
            
            _rigidBody = GetComponent<Rigidbody>();
        }

        public override void React(RaycastHit hit)
        {
            Debug.DrawRay(hit.point, hit.normal * 0.1f, Color.green, 1f);
            _rigidBody.AddForce(-hit.normal * 100, ForceMode.Impulse);
        } 
    }
}