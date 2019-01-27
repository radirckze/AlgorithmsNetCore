using CsBasics.Common;
using System;
using System.Collections.Generic;


namespace CsBasics.Graphs {

    //An adjacency-list based graph implementation
    public class AdjListGraph {

        private SortedList<int, Node> nodes;
        public SortedList<int, Node> Nodes {
            get {
                return nodes;
            }
        }

        public AdjListGraph() {
            nodes = new SortedList<int, Node>();
        }

        public Node AddPerson(Person person) {
            Node newPn = new Node(person);
            this.nodes.Add(person.Id, newPn );
            return newPn;
        }

        public bool RemovePerson(Node Node) {
            Node thePerson = null;
            if (this.nodes.TryGetValue(Node.Person.Id, out thePerson)) {
                // remove all refrences to person
                foreach(Node pn in thePerson.Connections.Values) {
                    pn.Connections.Remove(thePerson.Person.Id);
                }

                this.nodes.Remove(thePerson.Person.Id);
                return true;
            }
            return false;
        }

        public void AddConnection(Node pn1, Node pn2)  {
            //check that pn1 an pn2 exist
            pn1.Connections.Add(pn2.Person.Id, pn2);
            pn2.Connections.Add(pn1.Person.Id, pn1);
        }

        //A convenience method to add multiple connectons
        public void AddConnections(Node pn1, List<Node> newCons) {
            //check null etc
            foreach(Node pn in newCons) {
                AddConnection(pn1, pn);
            }
        }

        public void DeleteConnection(Node pn1, Node pn2)  {
            if(pn1.Connections.ContainsKey(pn2.Person.Id) ) {
                pn1.Connections.Remove(pn2.Person.Id);
                pn2.Connections.Remove(pn1.Person.Id);
            }
        }

        //Depth first travesal for a single node
        public void PrintDepthFirst4Node(Node node, List<int> visited=null, int distance=0) {
            if (node == null) {return;}
            if (visited == null) {
                visited = new List<int>();
            }

            if (visited.Contains(node.Person.Id)) {
                return; 
            }
            else {
                visited.Add(node.Person.Id);
                Console.WriteLine("{0}, Ditance={1}", node.Person, distance);
                distance++;
                foreach (Node pn in node.Connections.Values) {
                    PrintDepthFirst4Node(pn, visited, distance);
                }
            }
        }

        //Depth first travesal of full graph
        public void PrintGraphDepthFirst() {
            List<int> visited = new List<int>();
            foreach(Node pn in this.nodes.Values) {
                this.PrintDepthFirst4Node(pn, visited, 0);
            }
        }

        //Breadth first search (for single node)
        //This method will print level as well so it does some extra stuff 
        public void PrintBreadthFirst4Node(Node node) {
            if (node == null) {return;}

            Queue<Node> nodesQ = new Queue<Node>();
            List<int> queued = new List<int>();
            int level = 0;
            bool levelReset = false;
            nodesQ.Enqueue(node);
            nodesQ.Enqueue(null);
            queued.Add(node.Person.Id);
            
            
            while (nodesQ.Count > 0) {
                Node pn = nodesQ.Dequeue();
                if (pn != null) {
                    if (levelReset) {
                        nodesQ.Enqueue(null);
                        levelReset = false;
                    }

                    Console.WriteLine("Level: {0}: {1}", level, pn.Person);
                    foreach (Node connection in pn.Connections.Values) {
                        if (!queued.Contains(connection.Person.Id)) {
                            nodesQ.Enqueue(connection);
                            queued.Add(connection.Person.Id);
                        }
                    }
                }
                else {
                    level++;
                    levelReset = true;
                }
            }
        }
        
        public void TopologicalSort() {
            throw new NotImplementedException();
            //find the nodes with no incoming edges and and to queue. Do BFS.
            // for each of its children, if it has a incoming edg that has not been processed,
            // ignore it. Else, add it to the queue.
        }

        public bool isGraphAcyclic() {
            List<int> visited = new List<int>();
            bool result = true;
            foreach(Node pn in this.nodes.Values) {
                if(!visited.Contains(pn.Person.Id)) {
                    result = DetectCyclesDF(pn, visited, pn.Person.Id);
                    if (!result) {
                        return false;
                    }
                }
            }
            return true;
        }

        //Detect a cycle for a node using depth first search
        public bool DetectCyclesDF(Node pn, List<int> visited, int parentId) {
            bool result = true;
            if (visited.Contains(pn.Person.Id)) {
                return false;
            }
            else {
                visited.Add(pn.Person.Id);
                foreach(Node connection in pn.Connections.Values) {
                    if (connection.Person.Id != parentId) {
                        result = result && DetectCyclesDF(connection, visited, pn.Person.Id);
                    }
                }
            }
            return result;
        }

        //Print graph 
        public void PrintGraphNodes() {
            Console.WriteLine("\nThe Graph ...");
            foreach(Node pn in this.nodes.Values) {
                Console.Write("{0}: ",pn.Person);
                foreach(Node connection in pn.Connections.Values) {
                    Console.Write("{0}, ", connection.Person.Id);
                }
                Console.WriteLine();
            }
        }

        public static void TestAdjListGraph() {

            AdjListGraph adjListGraph = null;

            Console.WriteLine("Testing adjacency list based graph ...");
            //Testing the empty graph
            try {
                Console.WriteLine("Testing an empty graph");
                adjListGraph = new AdjListGraph();
                adjListGraph.PrintGraphNodes();
                adjListGraph.PrintGraphDepthFirst();
                adjListGraph.isGraphAcyclic();
                Console.WriteLine("Testing emplty list completed without errors");
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

           // Testing grapth with nodes
            try {
                Console.WriteLine("\nTesting graph with multiple nodes");
                adjListGraph = new AdjListGraph();
                Node kenickie = adjListGraph.AddPerson(new Person(100, "Kenickie"));
                Node frenchie = adjListGraph.AddPerson(new Person(101, "Frenchie"));
                Node danny = adjListGraph.AddPerson(new Person(102, "Danny"));
                Node betty = adjListGraph.AddPerson(new Person(103, "Betty"));
                Node sandy = adjListGraph.AddPerson(new Person(104, "Sandy"));
                Node patty = adjListGraph.AddPerson(new Person(105, "Patty"));
                Node doody = adjListGraph.AddPerson(new Person(106, "Doody"));
                Node blanche = adjListGraph.AddPerson(new Person(107, "Blanche"));
                Node chacha = adjListGraph.AddPerson(new Person(108, "Cha Cha"));
                Node leo = adjListGraph.AddPerson(new Person(109, "Leo"));

                //Add some connections 
                adjListGraph.AddConnections(danny, new List<Node> { kenickie, sandy, doody });
                adjListGraph.AddConnections(blanche, new List<Node> { betty, sandy, patty });
                adjListGraph.AddConnections(frenchie, new List<Node> { sandy });
                adjListGraph.AddConnections(betty, new List<Node> { patty });
                //disconnected
                adjListGraph.AddConnection(chacha, leo);

                adjListGraph.PrintGraphNodes();

                Console.WriteLine("\nPrinting depth first  for Danny");
                adjListGraph.PrintDepthFirst4Node(danny);

                Console.WriteLine("\nPrinting breadth first for Blanche");
                adjListGraph.PrintBreadthFirst4Node(blanche);

                Console.WriteLine("\nPrinting full graph");
                adjListGraph.PrintGraphDepthFirst();

                Console.WriteLine("\nIs graph acyclic? {0}", adjListGraph.isGraphAcyclic());
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            Console.WriteLine("\nAdjacency list graph testing completed.");

        }

    } //end Adjacency List Graph

}