using RootMotion.Dynamics;
using UnityEngine;

namespace DealDamage.DeadLimbEffects
{
    [CreateAssetMenu]
    public class DeadArm : LimbDeathEffect
    {
        [SerializeField] private string armGroup;
        
        public override void Apply(BehaviourPuppet puppet)
        {
            for (int i = 0; i < puppet.puppetMaster.muscles.Length; i++)
            {
                Muscle muscle = puppet.puppetMaster.muscles[i];
                if (muscle.name.Contains(armGroup))
                {
                    muscle.props.pinWeight = 0f;
                    muscle.props.muscleWeight = 0f;
                }
            }
        }
    }
}