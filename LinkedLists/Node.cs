using System;

namespace CsBasics.LinkedList {

    public class Node {

        public int Val {get; set;}
        public Node Next {get; set;}
        public Node Prev {get; set;}  //Note: *only* used in doubly linked list

    }

}