using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviourTrees
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        private Node _root;
        [FormerlySerializedAs("isTreeEnabled")] public bool isAiEnabled = true;

        protected virtual void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root is null || !isAiEnabled) return;
            
            _root.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}