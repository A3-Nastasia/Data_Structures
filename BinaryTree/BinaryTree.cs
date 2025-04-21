using System;
using System.Collections.Generic;

namespace BinaryTree
{
    class BinaryTreeNode
    {
        public BinaryTreeNode left;
        public BinaryTreeNode right;
        public BinaryTreeNode parent;
        public int nodeValue;

        public BinaryTreeNode(BinaryTreeNode parent, int nodeValue)
        {
            this.parent = parent;
            this.nodeValue = nodeValue;
        }

        public BinaryTreeNode findNode(int nodeValue)
        {
            BinaryTreeNode node = this;

            while (node!=null)
            {
                if (nodeValue == node.nodeValue) return node;
                if (nodeValue < node.nodeValue) node = node.left;
                if (nodeValue > node.nodeValue) node = node.right;
            }

            return null;
        }

        public void insertNode(int nodeValue) => this.insertNode(this, nodeValue);

        private void insertNode(BinaryTreeNode parentNode, int nodeValue)
        {
            if(nodeValue < parentNode.nodeValue)
            {
                if (parentNode.left == null)
                    parentNode.left = new BinaryTreeNode(parentNode, nodeValue);
                else
                    this.insertNode(parentNode.left, nodeValue);
            }

            if(nodeValue > parentNode.nodeValue)
            {
                if (parentNode.right == null)
                    parentNode.right = new BinaryTreeNode(parentNode, nodeValue);
                else
                    this.insertNode(parentNode.right, nodeValue);
            }
        }

        public void removeNode(int nodeValue) =>this.removeNode(nodeValue);

        public BinaryTreeNode removeNode(BinaryTreeNode node, int nodeValue)
        {
            if (node == null) return null;

            if (nodeValue < node.nodeValue)
                node.left = this.removeNode(node.left, nodeValue);
            else if (nodeValue > node.nodeValue)
                node.right = this.removeNode(node.right, nodeValue);
            else
                if (node.right == null) return node.right;
                if (node.left == null) return node.left;


            var original = node;
            node = node.left;

            while (node.left != null)
                node = node.left;

            node.right = this.removeMin(original.right);
            node.left = original.left;

            return node;
        }

        private BinaryTreeNode removeMin(BinaryTreeNode node)
        {
            if (node.left == null) return node.right;

            node.left = removeMin(node.left);
            return node;
        }

        #region BFS
        public void traversalRecursive(BinaryTreeNode node)
        {
            if(node != null)
            {
                Console.WriteLine($"node = {node.nodeValue}");
                traversalRecursive(node.left);
                traversalRecursive(node.right);
            }
        }

        public void traversalRecursive(BinaryTreeNode node, int level = 0)
        {
            if(node != null)
            {
                Console.WriteLine($"{new string(' ', level*4)}node = {node.nodeValue}");
                traversalRecursive(node.left, level+1);
                traversalRecursive(node.right, level+1);
            }
        }

        public void traversalRecursive(BinaryTreeNode node, string indent = " ", bool isLeft = true)
        {
            if(node != null)
            {

                Console.WriteLine($"{indent}{(isLeft ? "L--" : "R--")} node = {node.nodeValue}");
                string newIndent = indent + (isLeft ? "|   " : "    ");
                traversalRecursive(node.left, newIndent, true);
                traversalRecursive(node.right, newIndent, false);
            }
        }

        public void traversalStack()
        {
            Stack<BinaryTreeNode> stack = new Stack<BinaryTreeNode>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();

                Console.WriteLine($"node = {currentNode.nodeValue}");

                if (currentNode.right != null)
                    stack.Push(currentNode.right);
                if (currentNode.left != null)
                    stack.Push(currentNode.left);
            }
        }

        public void traversalQueue()
        {
            Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                Console.WriteLine($"node = {currentNode.nodeValue}");

                if (currentNode.right != null)
                    queue.Enqueue(currentNode.right);
                if (currentNode.left != null)
                    queue.Enqueue(currentNode.left);
            }
        }
        #endregion 

        private bool IsSymmetric(BinaryTreeNode root)
        {
            return isMirror(root, root);
        }

        public bool isMirror(BinaryTreeNode tree1, BinaryTreeNode tree2)
        {
            if (tree1 == null && tree2 == null)
                return true;
            
            if (tree1 == null || tree2 == null)
                return false;

            return (tree1.nodeValue == tree2.nodeValue)
                && isMirror(tree1.left, tree2.right)
                && isMirror(tree2.left, tree1.right);
        }


        public bool AreNodesSame(BinaryTreeNode tree1, BinaryTreeNode tree2)
        {
            if (tree1 == null && tree2 == null)
                return true;
            
            if (tree1 == null || tree2 == null)
                return false;

            return (tree1.nodeValue == tree2.nodeValue)
                && isMirror(tree1.left, tree2.left)
                && isMirror(tree1.right, tree2.right);
        }

        private void InorderHelper(BinaryTreeNode node, IList<int> list)
        {
            if (node == null)
                return;

            InorderHelper(node.left, list);

            list.Add(node.nodeValue);

            InorderHelper(node.right, list);
        }

        public IList<int> InorderTraversal(BinaryTreeNode root)
        {
            IList<int> list = new List<int>();

            if (root == null)
                return list;

            InorderHelper(root, list);

            return list;
        }

        public int MinDepth(BinaryTreeNode root)
        {
            if (root == null)
                return 0;

            Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(root);

            int depth = 1;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    BinaryTreeNode current = queue.Dequeue();

                    if (current.left == null && current.right == null)
                        return depth;

                    if (current.left != null) queue.Enqueue(current.left);
                    if (current.right != null) queue.Enqueue(current.right);
                }
                depth++;
            }

            return depth;
        }

        public int MaxDepth(BinaryTreeNode root)
        {
            if (root == null)
                return 0;

            Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(root);
            int depth = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                depth++;

                for (int i = 0; i < levelSize; i++)
                {
                    BinaryTreeNode current = queue.Dequeue();

                    if (current.left != null) queue.Enqueue(current.left);
                    if (current.right != null) queue.Enqueue(current.right);
                }
            }
            return depth;
        }


        public bool HasPathSum(BinaryTreeNode root, int targetSum)
        {
            if (root == null)
                return false;

            targetSum -= root.nodeValue;

            if (root.left == null && root.right == null)
                return targetSum == 0;

            return HasPathSum(root.left, targetSum) || HasPathSum(root.right, targetSum);
        }

        public IList<int> PreorderTraversal(BinaryTreeNode root)
        {
            IList<int> list = new List<int>();

            if (root != null)
            {
                list.Add(root.nodeValue);
                PreorderTraversal(root.left);
                PreorderTraversal(root.right);
            }
            return list;
        }

        private void CountNodesHelper(BinaryTreeNode root, int count)
        {
            if (root == null)
                return;

            count++;
            CountNodesHelper(root.left, count);
            CountNodesHelper(root.right, count);
        }

        public int CountNodes(BinaryTreeNode root)
        {
            int nodesAmount = 0;
            if (root == null)
                return nodesAmount;

            CountNodesHelper(root, nodesAmount);

            return nodesAmount;
        }

        private int FindTiltHelper(BinaryTreeNode node)
        {
            int sum = 0;
            Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.left != null && current.right != null)
                    sum += current.nodeValue;
            }

            return sum;
        }

        public int FindTilt(BinaryTreeNode root)
        {
            if (root == null)
                return 0;

            int sum = FindTiltHelper(root);
            return sum;
        }

        private int SumOfLeftLeavesHelper(BinaryTreeNode node)
        {
            int sum = 0;
            if (node == null)
                return sum;
            if (node.left != null)
            {
                if (node.left.left == null && node.left.right == null)
                    sum += node.left.nodeValue;
                else
                {
                    sum += SumOfLeftLeavesHelper(node.left);
                }
            }

            sum += SumOfLeftLeavesHelper(node.right);

            return sum;
        }

        public int SumOfLeftLeaves(BinaryTreeNode root)
        {
            return root == null ? 0 : SumOfLeftLeavesHelper(root);
        }


        private BinaryTreeNode InvertTreeHelper(BinaryTreeNode node)
        {
            if (node == null)
                return null;

            BinaryTreeNode temp = node.left;
            node.left = node.right;
            node.right = temp;

            InvertTreeHelper(node.right);
            InvertTreeHelper(node.left);
            return node;
        }

        public BinaryTreeNode InvertTree(BinaryTreeNode root)
        {
            return InvertTreeHelper(root);
        }

        public int RangeSumBST(BinaryTreeNode root, int low, int high)
        {
            if (root == null)
                return 0;

            int sum = 0;

            Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                BinaryTreeNode current = queue.Dequeue();
                Console.WriteLine(current.nodeValue);
                if (current.nodeValue >= low && current.nodeValue <= high)
                    sum += current.nodeValue;

                if (current.left != null)
                    queue.Enqueue(current.left);
                if (current.right!= null)
                    queue.Enqueue(current.right);
            }
            return sum;
        }

        public bool IsValidBST(BinaryTreeNode root)
        {
            return IsValidBSTHelper(root, null, null);
        }

        public bool IsValidBSTHelper(BinaryTreeNode root, int? min, int? max)
        {
            if (root == null)
                return true;

            if ((min.HasValue && root.nodeValue <= min.Value) || (max.HasValue && root.nodeValue >= max.Value))
                return false;

            return IsValidBSTHelper(root.left, min, root.nodeValue) &&
                   IsValidBSTHelper(root.right, root.nodeValue, max);

        }

        public BinaryTreeNode SearchBST(BinaryTreeNode root, int val)
        {
            if (root == null)
                return null;

            Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                BinaryTreeNode current = queue.Dequeue();

                if (current.nodeValue == val)
                    return current;

                if (current.left != null)
                    queue.Enqueue(current.left);

                if (current.right != null)
                    queue.Enqueue(current.right);
            }
            return null;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            BinaryTreeNode tree = new BinaryTreeNode(null, 10);
            tree.insertNode(5);
            tree.insertNode(15);
            tree.insertNode(3);
            tree.insertNode(7);
            tree.insertNode(18);
            Console.WriteLine(tree.RangeSumBST(tree, 7, 15));
            Console.WriteLine(tree.IsValidBST(tree));
            tree.traversalRecursive(tree, " ", true);

            Console.WriteLine(tree.FindTilt(tree));
        }
    }
}