using System;
using System.Collections.Generic;

namespace CsBasics.BinaryTrees{

    public class BinaryTree : AbstractTree {

        //constructor
        public BinaryTree() : base() {
        }

        protected  override Node AddNodeInt(Node node, int newVal) {
            if (node == null) {
                return new Node(newVal);
            }
            else if (newVal < node.Val) {
                node.Left = AddNodeInt(node.Left, newVal);
            }
            else if (newVal > node.Val) {
                node.Right = AddNodeInt(node.Right, newVal);
            }
            // else duplicate. Do nothing.

            return node;
        }

        protected override Node DeleteNodeInt(Node node, int value) {
            if (node == null) {return null;} 

            if (node.Val > value) {
                node.Left = DeleteNodeInt(node.Left, value);
            }
            else if (node.Val < value) {
                node.Right = DeleteNodeInt(node.Right, value);
            }
            else {
                // found value to be deleted
                if (node.Right == null) {
                    return node.Left;
                }
                else if (node.Left == null) {
                    return node.Right;
                }
                else {
                    //node has two childrent, find the min value on the right and ...
                    Node minNode = node.Right;
                    while (minNode.Left != null) {
                        minNode = minNode.Left;
                    }
                    node.Val = minNode.Val;
                    node.Left = DeleteNodeInt(node.Left, minNode.Val);
                }
            }

            return node;
        }

        protected bool DeleteNodeIterative(int value) {
            
            Node current = this.root;
            Node parent = null;
            bool ltParent = false;
            while (current != null && current.Val != value) {
                parent = current;
                if ( value < current.Val) {
                    current = current.Left;
                    ltParent = true;
                }
                else {
                    current = current.Right;
                    ltParent = false;
                }
            }

            if (current == null) { //value not in tree
                return false;
            }

            //ok we founmd the node.
            if (current.Left == null) {
                if (parent == null) {
                    this.root = current.Right;
                }
                else if (ltParent) {
                    parent.Left = current.Right;
                }
                else {
                    parent.Right = current.Right;
                }
                return true;
            }
            else if (current.Right == null) {
                if (parent == null) {
                    this.root = current.Left;
                }
                else if (ltParent) {
                    parent.Left = current.Left;
                }
                else {
                    parent.Right = current.Left;
                }
                return true;
            }
            else { //both left and right are not null
                Node inOrderMin = current.Right;
                parent = current;
                ltParent = false;
                while (inOrderMin.Left != null) {
                    parent = inOrderMin;
                    inOrderMin = inOrderMin.Left;
                    ltParent = true;
                }

                current.Val = inOrderMin.Val;
                if (ltParent) {
                    parent.Left = null;
                }
                else {
                    parent.Right = null;
                }
            }

            return true;
            

        }


        public static void TestBinaryTree() {

            BinaryTree theTree = new BinaryTree();

            Console.WriteLine("Testing empty tree ...");
            try {
                theTree = new BinaryTree();
                theTree.InOrderRecurrsivePrint();
                theTree.BreadthFirstPrint();
                theTree.Find(5);
                theTree.GetDepth();
                theTree.NodeCount();
                theTree.PrintRange( 3, 7);
                theTree.DeleteNode(5);
                theTree.DeleteNodeIterative(5);
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

                Console.WriteLine("\nTesting DeleteNodeIterative with multiple node ...");
                theTree = new BinaryTree();
                theTree.AddListToTree(new int[] { 15, 5, 11, 21, 26, 7, 9, 24, 3, 18, 8 });
                beforeCount = theTree.NodeCount();
                theTree.DeleteNodeIterative(21);
                 afterCount = theTree.NodeCount(); 
                theTree.DeleteNodeIterative(24);
                 afterCount = theTree.NodeCount();
                theTree.DeleteNodeIterative(15); //delete the root check
                afterCount = theTree.NodeCount();
                Console.WriteLine("Deleting iterative : {0}", beforeCount == afterCount + 3 ? "pass" : "fail");

            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

        }
    }

}