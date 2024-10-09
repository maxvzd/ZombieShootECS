using System;
using RootMotion.Dynamics;
using UnityEngine;

namespace DealDamage.DeadLimbEffects
{
    [CreateAssetMenu]
    public class DeadLeg : LimbDeathEffect
    {
        public override void Apply(BehaviourPuppet puppet, Animator animator)
        {
            puppet.onGetUpProne.animations = Array.Empty<BehaviourBase.AnimatorEvent>();
            puppet.onGetUpSupine.animations = Array.Empty<BehaviourBase.AnimatorEvent>();
            puppet.Unpin();
            
            animator.SetBool(Constants.IsCrippled, true);
        }
    }
}