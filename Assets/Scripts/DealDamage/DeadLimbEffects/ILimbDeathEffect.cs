using RootMotion.Dynamics;

namespace DealDamage.DeadLimbEffects
{
    public interface ILimbDeathEffect
    {
        public void Apply(BehaviourPuppet puppet);
    }
}