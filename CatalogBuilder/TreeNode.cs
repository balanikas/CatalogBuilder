﻿using System.Collections;
using System.Collections.Generic;

namespace CatalogBuilder
{
    public class TreeNode : IEnumerable<TreeNode>
    {
        private static int _nodeCounter;

        public static string NodeNamingPattern = "node";

        private readonly Dictionary<string, TreeNode> _children =
            new Dictionary<string, TreeNode>();

        public string Id;
        public TreeNode Parent { get; private set; }
        
        public TreeNode(NodeConfiguration config)
        {
            Id = NodeNamingPattern + _nodeCounter++;
            Config = config;
        }

        public NodeConfiguration Config { get;  }

        public TreeNode GetChild(string id)
        {
            return _children[id];
        }

        public void Add(TreeNode item)
        {
            item.Parent?._children.Remove(item.Id);

            item.Parent = this;
            _children.Add(item.Id, item);
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            return _children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _children.Count;
    }
}