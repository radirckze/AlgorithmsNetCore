using System;
//using System.Collections;

namespace CsBasics.LinkedList {

    // Last-in, first-out stack implemenation
    public class Stack : AbstractList {

        public Stack() : base() {}


        public void Push(int theNodeVal) {
            Node temp = Head;
            this.Head = new Node() {Val= theNodeVal};
            this.Head.Next = temp;
        }

        public int Pop () {
            if (this.Head == null) {
                throw new InvalidOperationException();
            }
            Node temp = this.Head;
            this.Head = Head.Next;
            return temp.Val;
        }


        public static void TestStack() {
            Console.WriteLine("Testing Stack implementation ...");
            Stack theStack = null;

            try {
                Console.WriteLine("Testing an empty stack");
                theStack = new Stack();
                theStack.PrintMe();
                try {
                    theStack.Pop();
                }
                catch (InvalidOperationException ioe) {
                    //expcted
                }
                theStack.Push(3);
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            try {
                Console.WriteLine("Testing stack with 1 element");
                theStack = new Stack();
                theStack.Push(3);
                theStack.PrintMe();
                theStack.Pop();
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            try {
                Console.WriteLine("Testing stack with multiple elements");
                theStack = new Stack();
                theStack.Push(1); theStack.Push(3); theStack.Push(5); theStack.Push(7);
                theStack.PrintMe();
                int poppedVal = theStack.Pop();
                if (poppedVal == 7) {
                    Console.WriteLine("Poped the top whis is: {0}", poppedVal);
                }
                else {
                    Console.WriteLine("The popped value is not the last one pushed");
                }

                Console.WriteLine("Popped evething and pused 2, 4, 6");
                theStack.Pop(); theStack.Pop(); theStack.Pop();
                theStack.Push(2); theStack.Push(4); theStack.Push(6);
                theStack.PrintMe();
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }
     
       }

    }

}