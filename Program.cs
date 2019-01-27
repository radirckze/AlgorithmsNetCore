using CsBasics.ArrayList;
using CsBasics.BinaryTrees;
using CsBasics.Graphs;
using CsBasics.LinkedList;
using System;

namespace CSBasics {

    class Program {

        static void Main(string[] args) {
            Console.WriteLine("Running CS Basics test programs ...");

            bool runLinkedListTest = false;
            bool runStackTest = false;
            bool runArrayListTest = false;
            bool runBinaryTreeTest = true;
            bool runAvlTreeTest = false;
            bool runAdjListGraphTest = false;

            if (runLinkedListTest) {
                LinkedList.TestLinkedList();
            }

            if (runStackTest) {
                Stack.TestStack();
            }

            if (runArrayListTest) {
                MyArrayList.TestArrayList();
            }

            if (runBinaryTreeTest) {
                BinaryTree.TestBinaryTree();
            }

            if (runAvlTreeTest) {
                AvlTree.TestAvlTree();
            }

            if (runAdjListGraphTest) {
                AdjListGraph.TestAdjListGraph();
            }

        }
    }

}
