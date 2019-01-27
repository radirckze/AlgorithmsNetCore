using System;

namespace CsBasics.LinkedList {

    public abstract class AbstractList {

          public Node Head {get; set;}

          public AbstractList() {
              this.Head = null;
          }

          public void PrintMe() {
              if (this.Head == null) {
                  Console.WriteLine("List is empty");
              }
              else {
                  Node current = this.Head;
                  while (current != null) {
                      Console.Write("{0}", current.Val); 
                      current = current.Next;
                      Console.Write("{0}", current==null?"":", ");
                  }
              }
              Console.WriteLine();
          }
    }

}