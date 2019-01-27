using System;
using System.Collections.Generic;

namespace CsBasics.BinaryTrees{

    public class AvlTree : AbstractTree {

        //reading: http://www.geeksforgeeks.org/avl-tree-set-1-insertion/

        //constructot
        public AvlTree() : base() {
        }

        private int GetHeight(Node node) {
            if (node == null) { return 0; }
            return node.Height;
        }

        private int MaxVal(int A, int B) {
            return (A < B)? B:A;
        }

        private int BalanceDiff(Node node) {
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private Node RotateRight(Node node) {
            Node newRoot = node.Left;
            Node temp = newRoot.Right;

            newRoot.Right = node;
            node.Left = temp;

            //update the heights
            node.Height = 1 + MaxVal(GetHeight(node.Left), GetHeight(node.Right));
            newRoot.Height = 1 + MaxVal(GetHeight(newRoot.Left), GetHeight(newRoot.Right));

            return newRoot;
        }

        private Node RotateLeft(Node node) {
            Node newRoot = node.Right;
            Node temp = newRoot.Left;

            newRoot.Left = node;
            node.Right = temp;

            //update the heights
            node.Height = 1 + MaxVal(GetHeight(node.Left), GetHeight(node.Right));
            newRoot.Height = 1 + MaxVal(GetHeight(newRoot.Left), GetHeight(newRoot.Right));
            
            return newRoot;
        }

         
        protected override Node AddNodeInt(Node node, int newVal) {
            if (node == null) {
                return new Node(newVal);
            }
            else if(newVal < node.Val) {
                node.Left = AddNodeInt(node.Left, newVal);
            }
            else if (newVal > node.Val) {
                node.Right = AddNodeInt(node.Right, newVal);
            }
            else {
                return node;
            }

            // update node height
            node.Height = 1 + MaxVal(GetHeight(node.Left),  GetHeight(node.Right));

            // check and rotate.
            int balanceDiff = BalanceDiff(node);

            if (balanceDiff > 1) { 
                //tree needs balancing on the left
                if (newVal < node.Left.Val) {
                    return RotateRight(node);
                }
                else {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }
            else if (balanceDiff < -1) { 
                //treen needs balancing on the right
                 if (newVal > node.Right.Val) {
                    return RotateLeft(node);
                }
                else {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        protected override Node DeleteNodeInt(Node node, int deleteVal) {
            if (node == null) { return null;}

            if (node.Val > deleteVal) {
                node.Left = DeleteNodeInt(node.Left, deleteVal);
            }
            else if (node.Val < deleteVal) {
                node.Right = DeleteNodeInt(node.Right, deleteVal);
            }
            else {
                // we found it - delete
                if (node.Left == null) {
                    node = node.Right;
                }
                else if (node.Right == null) {
                    node = node.Left;
                }
                else {
                    //neither is null so replace with the smallest in the sub-tree and ...
                    Node temp = node.Right;
                    while (temp.Left != null) {
                        temp = temp.Left;
                    }
                    node.Val = temp.Val;
                    node.Right = DeleteNodeInt(node.Right, temp.Val);
                }
            }

            if (node == null) {
                return null;
            }

            // update node height
            node.Height = 1 + MaxVal(GetHeight(node.Left),  GetHeight(node.Right));

            // check and rotate.
            int balanceDiff = BalanceDiff(node);

            if (balanceDiff > 1) { 
                //tree needs balancing on the left
                if (BalanceDiff(node.Left) >= 0) {
                    return RotateRight(node);
                }
                else {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }
            else if (balanceDiff < -1) { 
                //treen needs balancing on the right
                 if (BalanceDiff(node.Right) <= 0) {
                    return RotateLeft(node);
                }
                else {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node; 
        }

        public static void TestAvlTree() {

            AvlTree theTree = new AvlTree();

            Console.WriteLine("Testing empty tree ...");
            try {
                theTree = new AvlTree();
                theTree.InOrderRecurrsivePrint();
                theTree.BreadthFirstPrint();
                theTree.Find(5);
                theTree.GetDepth();
                theTree.NodeCount();
                theTree.PrintRange( 3, 7);
                theTree.DeleteNode(5);
                Console.WriteLine("Testing empty tree completed with no errors");
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            Console.WriteLine("\nTesting tree with multiple node ...");
            try {
                theTree.AddListToTree(new int[] { 15, 5, 11, 21, 26, 7, 9, 24, 3, 18, 8 });
                Console.WriteLine("Print Tree In order travesal");
                theTree.InOrderRecurrsivePrint();
                Console.WriteLine("\nPrint Tree breadth first travesal");
                theTree.BreadthFirstPrint();
                Console.WriteLine("Search for node=5 returned {0}", theTree.Find(5).Val);
                Console.WriteLine("Search for node=15 returned {0}", theTree.Find(15).Val);
                Console.WriteLine("The depth of the tree is: {0}", theTree.GetDepth());
                Console.WriteLine("Test print range 1 - 9");
                theTree.PrintRange(1, 9);
                Console.WriteLine("Test print range 16 - 30");
                theTree.PrintRange(16, 30);

                Console.WriteLine("\nTesting detele ...");
                int beforeCount = theTree.NodeCount();
                theTree.DeleteListFromTree(new int[] { 6 });
                int afterCount = theTree.NodeCount();
                Console.WriteLine("Deleting non existing item: {0}", beforeCount == afterCount ? "pass" : "fail");

                beforeCount = theTree.NodeCount();
                theTree.DeleteListFromTree(new int[] { 11, 18, 3 });
                afterCount = theTree.NodeCount();
                Console.WriteLine("Deleting 3 items: {0}", beforeCount == afterCount + 3 ? "pass" : "fail");
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

        }


    }

}