using UnityEngine;

namespace BehaviourTrees
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root;
        public bool isTreeEnabled = true;

        protected virtual void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root is null || !isTreeEnabled) return;
            
            _root.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}