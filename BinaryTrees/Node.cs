using System;
using System.Collections.Generic;

namespace CsBasics.BinaryTrees{

public class Node {
        public int Val {get; set;}
        public int Height {get; set;}  //required for balancing of AVL and RB trees
        public Node Left {get; set;}
        public Node Right {get; set;}

        //ctor for node
        public Node(int val) {
            this.Val = val;
            this.Height = 0;
        } 

        public override string ToString() {
            return String.Format("Value={0}, Height={1}, Left={2}, Right={3}", 
            this.Val, this.Height, this.Left==null?"null":"not-null", this.Right==null?"null":"not-null");
        }
    }

}