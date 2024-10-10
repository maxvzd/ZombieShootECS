using System.Collections.Generic;
using AIDetection;
using UnityEngine;

namespace BehaviourTrees.Zombie
{
    public class CanZombieSeeAnyTargets : Node
    {
        private readonly float _sightRadius;
        private readonly float _halfFov;
        private readonly Transform _eyesTransform;
        private readonly Transform _currentTransform;

        public CanZombieSeeAnyTargets(float sightRadius, float fov, Transform eyesTransform, Transform currentTransform)
        {
            _sightRadius = sightRadius;
            _halfFov = fov / 2;
            _eyesTransform = eyesTransform;
            _currentTransform = currentTransform;
        }
        
        public override NodeState Evaluate()
        {
            IList<Detectee> detectees = SightManager.Detectees;

            for (int i = 0; i < detectees.Count; i++)
            {
                Detectee currentDetectee = detectees[i];
                if (!VisionDetector.Detect(currentDetectee, _eyesTransform, _sightRadius, _halfFov, _currentTransform)) continue;

                AddData(Constants.TargetData, currentDetectee);
                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}