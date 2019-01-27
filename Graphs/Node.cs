using CsBasics.Common;
using System;
using System.Collections.Generic;

namespace CsBasics.Graphs{

    //The graph node
     public class Node {

        public Person Person {get; set;}
        public SortedList<int, Node> Connections;

        public Node(Person person) {
            if (person == null) {
                throw new ArgumentException();
            }
            this.Person = person;
            this.Connections = new SortedList<int, Node>();
        }
     }

}
    