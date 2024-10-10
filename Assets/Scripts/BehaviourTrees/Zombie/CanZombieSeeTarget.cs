using AIDetection;
using UnityEngine;

namespace BehaviourTrees.Zombie
{
    public class CanZombieSeeTarget : Node
    {
        private readonly float _halfFov;
        private readonly Transform _eyesTransform;
        private readonly Transform _currentTransform;
        private readonly Vector3 _forwardDir;
        private readonly float _sightRadius;
        
        public CanZombieSeeTarget(float sightRadius, float fov, Transform eyesTransform, Transform currentTransform)
        {
            _sightRadius = sightRadius;
            _halfFov = fov / 2;
            _eyesTransform = eyesTransform;
            _currentTransform = currentTransform;
        }
        
        public override NodeState Evaluate()
        {
            object target = GetData(Constants.TargetData);
            if (target is not Detectee currentDetectee) return NodeState.Failure;

            if (!VisionDetector.Detect(currentDetectee, _eyesTransform, _sightRadius, _halfFov, _currentTransform))
            {
                RemoveData(Constants.TargetData);
                return NodeState.Success;
            }
            
            EditData(Constants.TargetPositionData, currentDetectee);
            return NodeState.Success;
        }
        
        
    }
}