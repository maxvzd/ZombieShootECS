using RootMotion.Dynamics;
using UnityEngine;

namespace DealDamage.DeadLimbEffects
{
    [CreateAssetMenu]
    public class DeadLeg : LimbDeathEffect
    {
        public override void Apply(BehaviourPuppet puppet)
        {
            puppet.canGetUp = false;
            puppet.Unpin();
        }
    }
}