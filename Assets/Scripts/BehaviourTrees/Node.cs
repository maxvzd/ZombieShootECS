using System.Collections.Generic;

namespace BehaviourTrees
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    
    public abstract class Node
    {
        protected NodeState State;

        public Node Parent;
        protected List<Node> Children;

        private Dictionary<string, object> _dataContext = new();

        protected Node()
        {
            Parent = null;
            Children = new List<Node>();
        }

        protected Node(List<Node> children)
        {
            Children = new List<Node>();
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        public abstract NodeState Evaluate();

        public void AddData(string key, object data)
        {
            _dataContext.Add(key, data);
        }

        protected object GetData(string key)
        {
            if (_dataContext.TryGetValue(key, out var data))
            {
                return data;
            }

            Node parent = Parent;
            while (parent is not null)
            {
                data = parent.GetData(key);
                if (data is not null)
                {
                    return data;
                }
                parent = parent.Parent;
            }

            return null;
        }
        
        protected bool RemoveData(string key)
        {
            if (_dataContext.TryGetValue(key, out var data))
            {
                return _dataContext.Remove(key);
            }

            Node parent = Parent;
            while (parent is not null)
            {
                data = parent.GetData(key);
                if (data is not null)
                {
                    return _dataContext.Remove(key);
                }
                parent = parent.Parent;
            }
            return false;
        }
        
        private void Attach(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }
    }
}