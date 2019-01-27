using System;
using System.Collections.Generic;

namespace CsBasics.BinaryTrees{

    public abstract class AbstractTree {

        protected Node root = null;
        public Node Root {
            get {
                return root;
            }
        }

        //ctor
        public AbstractTree() {
            this.root = null;
        }

        // clear the contents
        public void Clear() {
            this.root = null;
        }

        public void AddNode(int newVal) {
            this.root = AddNodeInt(this.root, newVal);
        }

        //Add a node. (Note: add and delete are tree specific.)
        protected virtual Node AddNodeInt(Node node, int newVal) {
             throw new NotImplementedException("need to override in concrete class");
         }

        //Delete a node. (Note: add and delete are tree specific.)
        public void DeleteNode(int value) {
            this.root = DeleteNodeInt(this.root, value);
        }

        protected virtual Node DeleteNodeInt(Node node, int value) {
             throw new NotImplementedException("need to override in concrete class");
        }

        // A convenience method to add a bunch of nodes ...
        public void AddListToTree(int[] ints) {
            if (ints != null) {
                foreach(int newInt in ints) {
                    this.AddNode(newInt);
                }
            }
        }

        // A convenience method to delete a bunch of nodes ...
        public void DeleteListFromTree(int[] ints) {
            if (ints != null) {
                foreach(int delInt in ints) {
                    this.DeleteNode(delInt);
                }
            }
        }

        // get the number of nodes
        public int NodeCount() {
            return this.NodeCountInt(root);
        }

        private int NodeCountInt(Node node) {

            if (node ==null) {
                return 0;
            }

            int count = NodeCountInt(node.Left);
            count = count + NodeCountInt(node.Right);
            return count+1;
        }

        public void InOrderRecurrsivePrint() {
            this.InOrderRecurrsivePrintInt(this.root);
        }


        // an in order (recursive) travesal which prints nodes ...
        private void InOrderRecurrsivePrintInt(Node node) {
            if (node != null) {
                InOrderRecurrsivePrintInt(node.Left);
                Console.WriteLine(node.ToString());
                InOrderRecurrsivePrintInt(node.Right);
            }
        }

        //A breadth first travesal of the tree
        public void BreadthFirstPrint() {
            this.BreadthFirstPrintInt(this.root);
        }

        private void BreadthFirstPrintInt(Node root) {

            Queue<Node> queue = new Queue<Node>();
            if (root == null) {
                return;
            }
            else {
                queue.Enqueue(null);
                queue.Enqueue(root);
            }

            int level=0;
            Node tn = null;
            bool newLevel = false;
            while (queue.Count > 0) {
                tn = queue.Dequeue();
                if (tn == null) {
                    newLevel = true;
                }
                else {
                    if (newLevel) {
                        queue.Enqueue(null); //new level marker
                        Console.WriteLine("\nLevel {0}", level);
                        level++;
                        newLevel = false;
                    }

                    Console.Write("{0}, ", tn.Val);
                    if (tn.Left != null) {queue.Enqueue(tn.Left); };
                    if (tn.Right != null) {queue.Enqueue(tn.Right); };
                }
            }
            Console.WriteLine("\n");
        }

        // find a node
        public Node Find(int val) {
            
            Node current = Root;
            while(current != null) {
                if (current.Val == val) {
                    return current;
                }
                else if (current.Val > val) {
                    current = current.Left;
                }
                else {
                    current = current.Right;
                }
            }
            
            return null;
        }

        // print all values within the given range
        public void PrintRange(int start, int end) {
            this.PrintRangeInt(root, start, end);
        }

        public void PrintRangeInt(Node node, int start, int end) {

            //Use travesel but stop traversing left if node.val < start and ...

            if (node == null ) {return;}

            if (node.Val > start) {
                PrintRangeInt(node.Left, start, end);
            }
            if (node.Val >= start && node.Val <= end) {
                Console.WriteLine(node.ToString());
            }
            if (node.Val < end) {
                PrintRangeInt(node.Right, start, end);
            }

        }

        // get depth of tree.
        public int GetDepth() {
            return GetDepthInt(this.root);
        }

        public int GetDepthInt(Node node) {
            
            if (node == null) {return 0;}

            int  leftDepth = GetDepthInt(node.Left);
            int rightDepth = GetDepthInt(node.Right);
            int depth = leftDepth > rightDepth? leftDepth+1: rightDepth+1;
            return depth; 
        }

        // Validate that each node is balanced, that is ABS(left-depth - right-depth) <= 1
        public bool IsTreeBalanced() {
            //Challenge is to develop an algorithm that is efficient in terms of both
            // performance (so Order-n) and resources (so avoid recursion).
            throw new NotImplementedException();
        }
    } 

}