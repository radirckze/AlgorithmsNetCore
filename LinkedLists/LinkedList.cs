using System;

namespace CsBasics.LinkedList {

    public class LinkedList : AbstractList {

        public LinkedList() : base() {}

        public bool Add(int intVal ) {

            Node temp = new Node() {Val = intVal};
            temp.Next = this.Head;
            this.Head = temp;
            return true;
        }

        public bool AddAfter(int intVal, int afterVal) {
            if (this.Head == null ) {
                return false;
            }
            else {
                Node temp = this.Head;
                while (temp != null && temp.Val != afterVal) {
                    temp = temp.Next;
                }
                if (temp == null) {
                    return false;
                }
                else {
                    Node newNode = new Node() {Val = intVal};
                    newNode.Next = temp.Next;
                    temp.Next = newNode;
                    return true;
                }
            }
        }

        public bool Remove(int intVal) {
            Node prev = null;
            Node current = this.Head;
            while (current != null && current.Val != intVal) {
                prev = current;
                current = current.Next;
            }
            if (current == null) {
                return false;
            }
            else if (prev == null) {
                this.Head = this.Head.Next;
            }
            else {
                prev.Next = current.Next;
            }
            return true;
        }

        // a utility method to initialize the list ...
        public  void InitList(int[] initValues) {
            if (initValues==null || initValues.Length <1) {return;}
            
            foreach(int val in initValues) {
                this.Add(val);
            }
        }

        // return the mth element from the end.
        public Node MthFromEnd(int m) {
            Node current = this.Head;
            Node mthNode = null;
            int count = 0;
            while(current != null && count < m) {
                current = current.Next;
                count++;
            }

            if (current != null){
                mthNode = this.Head;
                while (current.Next != null) {
                    current = current.Next;
                    mthNode = mthNode.Next;
                }
            }

            return mthNode;    
        }

        //reverse the list (recursive)
        public void ReverseRecursive() {
            this.Head = ReverseRecursiveInt(Head);
        }


        // Reverse linked list using recurision
        private Node ReverseRecursiveInt(Node current, Node prev=null) {
            Node newRoot = null;

            if (current == null) {
                return null;  // trying to reverse null tree
            }
            else if (current.Next == null) {
                newRoot = current;
            }
            else {
                newRoot = ReverseRecursiveInt(current.Next, current);
            }
            
            current.Next = prev;
            return newRoot;
        }

        //Reverse linked list without using recursion.
        public void ReverseNonRecursive() {

          Node prev = null;
          Node current = this.Head;
          Node curNext = null; 

          while(current != null) {
              curNext = current.Next;
              current.Next = prev;
              prev = current;
              current = curNext;
          }

          this.Head = prev;
        }

        // test cases. Note this is not an exhaustive search
        public static void TestLinkedList() {

            Console.WriteLine("Testing Linked List implementation ...");

            try {
                Console.WriteLine("Testing the empty list case");
                LinkedList theList = new LinkedList();
                // test empty list
                theList.PrintMe();
                theList.AddAfter(3, 5);
                theList.Remove(3);
                theList.ReverseRecursive();
                theList.ReverseNonRecursive();
                theList.MthFromEnd(3);
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            // test list with 1 element 
            try {
                Console.WriteLine("Testing Linked List with 1 element.");
                LinkedList theList = new LinkedList();
                theList.Add(5);
                theList.Remove(5);
                theList.Add(5);
                theList.ReverseRecursive();
                theList.ReverseNonRecursive();
                theList.AddAfter(3, 5);
                theList.MthFromEnd(3);
                theList.Remove(3); 
                theList.PrintMe();
                theList.Remove(5); //remiving the last element.
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            // test list with 7 elements
            try {
                Console.WriteLine("Testing Linked List with many elements");
                LinkedList theList = new LinkedList();
                theList.InitList(new int[] { 2, 4, 6, 8, 10, 12, 14 });
                theList.PrintMe();
                Console.WriteLine("Calling ReverseRecursive");
                theList.ReverseRecursive();
                theList.PrintMe();
                Console.WriteLine("Calling ReverseNonRecursive");
                theList.ReverseNonRecursive();
                theList.PrintMe();
                Console.WriteLine("Remving 2, 8, 14 and adding 11");
                theList.Remove(2);  //removing last (list reversed twice so ...)
                theList.Remove(8);  // removing first
                theList.Remove(14); // removing first.
                theList.AddAfter(11, 10);
                theList.PrintMe();

                Node m4th = theList.MthFromEnd(3);
                Console.WriteLine("The 3rd from the end is: {0}", m4th == null ? "null" : m4th.Val.ToString());
                m4th = theList.MthFromEnd(12);
                Console.WriteLine("The 12th from the end is: {0}", m4th == null ? "NA" : m4th.Val.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            Console.WriteLine("Testing completed!");

       }

    } //end linked list

}