using System;
using System.Collections.Generic;

namespace CsBasics.Common {

   // Class intended to use in other examples, although examples may be better
   // served with a simpler version of this class. 
    public class Person: IComparable<Person> {

        public int Id { get; set; }

        public string Name { get; set; }

        // ctors, methods, ...
        public Person(int id, string name) {

            if (String.IsNullOrEmpty(name))  {
                throw new ArgumentException("Name cannot be null of empty");
            }
            this.Id = id;
            this.Name = name;
        }

        public override bool Equals(object obj) {

            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            // TODO: write your implement
            return (Name != null && (Name.Equals(((Person)obj).Name)));
        }

        // override object.GetHashCode
        public override int GetHashCode()  {
            return base.GetHashCode();
        }

        public override string ToString() {
            return String.Format("[{0}, {1}]", this.Id, this.Name);
        }

        //using name coparison just for kicks
        public int CompareTo(Person comparePerson) {

            if (comparePerson == null) {
                return 1;  // This object is greater
            }
            else {
                return this.Name.CompareTo(comparePerson.Name);
            }
        }

        // utility function that returns a bunch of people
        public static List<Person> GetSomePeople() {

            List<Person> people = new List<Person>();

            people.Add(new Person(1001, "Mia"));
            people.Add(new Person(1020, "Vincent"));
            people.Add(new Person(1002, "Jules"));
            people.Add(new Person(1019, "Marsellus"));
            people.Add(new Person(1003, "Winston"));
            people.Add(new Person(1018, "Butch"));
            people.Add(new Person(1004, "Jimmie"));
            people.Add(new Person(1017, "Honey"));
            people.Add(new Person(1005, "Pumpkin"));
            people.Add(new Person(1016, "Koons"));
            people.Add(new Person(1006, "Esmarelda"));
            people.Add(new Person(1015, "Zed"));
            people.Add(new Person(1007, "Fabienne"));
            people.Add(new Person(1014, "Bonnie"));
            people.Add(new Person(1008, "Paul"));
            people.Add(new Person(1013, "Buddy"));
            people.Add(new Person(1009, "Lance"));
            people.Add(new Person(1012, "Jody"));
            people.Add(new Person(1010, "Maynard"));
            people.Add(new Person(1011, "Marilyn"));
            return people;
        }

    }
}