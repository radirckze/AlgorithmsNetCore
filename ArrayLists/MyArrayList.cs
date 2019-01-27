using CsBasics.Common;
using System;
using System.Collections.Generic;


// Note: This is used to for multiple purposes, including working sorting and searching .
namespace CsBasics.ArrayList {

    //This is a self expanding array. It expands by "bufferSize" each time.
    public class MyArrayList {

        private static readonly int buffSize = 3;  //keeping it small for testing

        //size of the *array-buffer*, NOT the # of elements in the array.
        private int size = buffSize;

        // # of elements in the array
        private int count = 0;
        public int Count {
            get {
                return count;
            }
        }

        private Person[] personArrary;
        public Person[] PersonArray {
            get {
                return personArrary;
            }
        }

        // Constructors

        public MyArrayList() {
            this.personArrary = new Person[size];
        }
        
        // copy constructor -- does not clone
        public MyArrayList(MyArrayList other) {
            Person[] otherPersonArray = other.PersonArray;
            this.personArrary = new Person[other.size];
            for(int i =0; i<other.count; i++) {
                personArrary[i] = otherPersonArray[i];
            }
            this.size = other.size;
            this.count = other.count;
        }

        // MyArrayList methods ...

        public void Add(Person person) {
            //need null checks 
            if (this.count == size) {
                //need to increase size of array
                Person[] tempArrary = new Person[size + buffSize];
                personArrary.CopyTo(tempArrary, 0);
                personArrary = tempArrary;
                size += buffSize;
            }

            personArrary[count] = person;
            this.count += 1;
        }

        public void AddPersons(List<Person> persons) {
            if (persons != null && persons.Count > 0 ) {
                foreach(Person person in persons) {
                    this.Add(person);
                }
            }

            return;   
        }

        public void Remove(Person person) {
            throw new NotImplementedException("need to add Remove at some point)");
        }

        //linear search
        public Person FindPersonLinearSearch(int id) {
            Person thePerson = null;
            if (count > 0) {
                for (int i = 0; i < count; i++) {
                    if (personArrary[i].Id == id) {
                        thePerson = personArrary[i];
                        break;
                    }
                }
            }

            return thePerson;
        }

        // Binary search. *Expects* array to be sorted beforehand!
        public Person FindPersonBinarySearch(int theId) {
            int startPos = 0, endPos = count - 1;
            while (startPos <= endPos)  {
                int midPos = (startPos + endPos) / 2;
                if (personArrary[midPos].Id == theId) {
                    return personArrary[midPos];
                }
                else if (personArrary[midPos].Id < theId) {
                    startPos = midPos + 1;
                }
                else {
                    endPos = midPos - 1;
                }
            }

            return null;
        }

        // Bubblesort array ---------------------------------------------------
        public void BubbleSortArray() {

           if (count < 2) {
               return;
           }

           bool swapped = true;
           int unsorted = count;
           Person tempPerson = null;

           while(swapped) {
               swapped = false;
               for(int i=0; i<unsorted-1; i++) {
                   if (personArrary[i].Id > personArrary[i+1].Id) {
                       tempPerson = personArrary[i];
                       personArrary[i] = personArrary[i+1];
                       personArrary[i+1] = tempPerson;
                       swapped = true;
                   }
               }

               unsorted = unsorted-1;
           }
        }

        // Insertion sort -----------------------------------------------------
        public void InsertionSortArray() {
            if (count < 2) { //null is taken care of by count in this case.
                return;
            }

            for(int i=1; i<count; i++) {
                int j = 0;
                while (personArrary[j].Id < personArrary[i].Id) { 
                    j++;
                }

                if ( j < i) { //we need to shift and swap
                    Person temp = personArrary[i];
                    for (int k=i; k>j; k--) {
                        PersonArray[k] = personArrary[k-1];
                    }
                    personArrary[j] = temp;
                }
            }
        }

        // Quicksort (using the standard partitioning algorithm) --------------
         public void QuicksortBasic() {
             this.QuicksortBasicInt(this.PersonArray, 0, this.Count-1);
         }

        private void QuicksortBasicInt(Person[] theArray, int start, int end) {

            //do null check and that count >= end, etc.
            if (start < end ) {
                int p = PartitionBasic(theArray, start, end);
                QuicksortBasicInt(theArray, start, p-1);
                QuicksortBasicInt(theArray, p+1, end);
            }
        }

        private int PartitionBasic(Person[] theArray, int start, int end) {
            Person pivot = theArray[end];
            int ltPivot = start-1;
            Person temp;
            for(int j=start; j<end; j++) {
                if (theArray[j].Id < pivot.Id)  {
                    ltPivot = ltPivot+1;
                    temp = theArray[ltPivot];
                    theArray[ltPivot] = theArray[j];
                    theArray[j] = temp;
                }
            }

            int pivotPos = ltPivot+1;
            temp = theArray[pivotPos];
            theArray[pivotPos] = theArray[end];
            theArray[end] = temp;

            return pivotPos; 
        }

        // Quicksort using Hoares partitioning algorithm ----------------------
        public void QuicksortHoares() {
            this.QuicksortHoaresInt(this.PersonArray, 0, this.Count-1);
        }

        private void QuicksortHoaresInt(Person[] theArray, int start, int end) {

            //do null check and that lenght>= end, etc.
            if (start < end ) {
                int p = PartitionHoares(theArray, start, end);
                QuicksortHoaresInt(theArray, start, p);
                QuicksortHoaresInt(theArray, p+1, end);
            }
        }

        private int PartitionHoares(Person[] theArray, int start, int end) {
            Person pivot = theArray[start]; //use rando index for better performance
            int i = start-1;
            int j = end+1;

            while(true) {
                do {
                    i = i+1;
                } while (theArray[i].Id < pivot.Id);

                do {
                    j = j-1;
                } while(theArray[j].Id > pivot.Id);

                if (i >= j) {
                    return j;
                }

                Person temp = theArray[i];
                theArray[i] = theArray[j];
                theArray[j] = temp;
            }
        }

        // A fun and *efficient* function that prints a range within a 
        // *sorted* list. Its efficient for large lists as it uses binary
        // search to find the starting element. 
        public void PrintRangeInSortedList(int minId, int maxId) {
           
            int startPos = 0, endPos = this.Count-1;
            int startIndex = -1;
            bool startFound = false;
            if (this.Count > 0) {
                while (startPos <= endPos && !startFound) {
                    int mid = (startPos + endPos) / 2;
                    if (personArrary[mid].Id < minId) {
                        startPos = mid+1;
                    }
                    else if ((mid==0 || personArrary[mid-1].Id < minId) 
                        && personArrary[mid].Id <= maxId) {
                            startFound = true;
                            startIndex = mid;
                    }
                    else {
                        endPos = mid-1;
                    }
                    
                }
            }

            Console.WriteLine("The people within the range of {0} and {1} are:", 
                 minId, maxId);
            if (startFound) {
                int currIndex = startIndex;
                while(currIndex < this.Count && personArrary[currIndex].Id <= maxId) {
                    Console.Write("[{0}, {1}], ", personArrary[currIndex].Id, 
                        personArrary[currIndex].Name);
                    currIndex++;        
                }
                Console.WriteLine();
            }
            else {
                Console.WriteLine("None found!");
            }

        }

        // Utility functions --------------------------------------------------

        public void PrintPersonArray()  {
            Console.WriteLine("Person Array is ....");
            if (count >= 1)
            {
                for(int i=0; i<count; i++)
                {
                    Console.Write("[{0}, {1}], ", personArrary[i].Id, personArrary[i].Name);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Empty");
            }
        }

        public bool IsArraySortedById() {
            bool isSorted = true;

            if (this.Count > 1) {
                for(int i=1; i<this.Count; i++ ) {
                    if (personArrary[i].Id < personArrary[i-1].Id) {
                        isSorted = false;
                        break;
                    }
                }
            }

            return isSorted;
        }

        public static void TestArrayList() {

            Console.WriteLine("Testing ArrayList ...");

            // testing for the null case
            try {
                Console.WriteLine("Testing an array with 0 elments");
                MyArrayList personArray = new MyArrayList();
                personArray.PrintPersonArray();
                Person thePerson = personArray.FindPersonLinearSearch(3);
                Console.WriteLine("The result for FindPersonLinearSearch(3) is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                personArray.BubbleSortArray();
                personArray.InsertionSortArray();
                personArray.QuicksortBasic();
                personArray.QuicksortHoares();
                 thePerson = personArray.FindPersonBinarySearch(3);
                Console.WriteLine("The result for FindPersonBinarySearch(3) is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                personArray.PrintRangeInSortedList(1, 5);
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            //testing array with 1 element
            try {
                Console.WriteLine("Testing an array with 1 elment");
                MyArrayList personArray = new MyArrayList();
                personArray.Add(new Person(100, "Mia"));
                personArray.PrintPersonArray();
                Person thePerson = personArray.FindPersonLinearSearch(100);
                Console.WriteLine("The result for FindPersonLinearSearch(100) is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                personArray.BubbleSortArray();
                personArray.InsertionSortArray();
                personArray.QuicksortBasic();
                personArray.QuicksortHoares();
                thePerson = personArray.FindPersonBinarySearch(100);
                Console.WriteLine("The result for FindPersonBinarySearch(100) is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                personArray.PrintRangeInSortedList(1, 100);
                personArray.PrintRangeInSortedList(1, 200);
            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }

            // testing array with multiple elements
            try {
                Console.WriteLine("Testing an array with multiple elments");
                MyArrayList personArray = new MyArrayList();
                personArray.AddPersons(Person.GetSomePeople());
                personArray.PrintPersonArray();
                Person thePerson = personArray.FindPersonLinearSearch(1010);
                Console.WriteLine("The LinearSearch result for 1010 is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                thePerson = personArray.FindPersonLinearSearch(10);
                Console.WriteLine("The inearSearch result for non existent person is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());

                personArray.BubbleSortArray();
                Console.WriteLine("After bubblesort, is array sorted: {0}", 
                    personArray.IsArraySortedById());

                personArray = new MyArrayList();
                personArray.AddPersons(Person.GetSomePeople());
                personArray.InsertionSortArray();
                Console.WriteLine("After insertion sort, is array sorted: {0}", 
                    personArray.IsArraySortedById());

                personArray = new MyArrayList();
                personArray.AddPersons(Person.GetSomePeople());
                personArray.QuicksortBasic();
                Console.WriteLine("After quicksort (basic), is array sorted: {0}", 
                    personArray.IsArraySortedById());

                personArray = new MyArrayList();
                personArray.AddPersons(Person.GetSomePeople());
                personArray.QuicksortHoares();
                Console.WriteLine("After quicksort (hoares), is array sorted: {0}", 
                    personArray.IsArraySortedById());

                 thePerson = personArray.FindPersonBinarySearch(1011);
                Console.WriteLine("The BinarySearch result for 1011 is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                thePerson = personArray.FindPersonBinarySearch(1002);
                Console.WriteLine("The BinarySearch result for 1002 is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                thePerson = personArray.FindPersonBinarySearch(10);
                Console.WriteLine("The BinarySearch result non-existent 10 is {0}", 
                    thePerson == null ? "null" : thePerson.ToString());
                personArray.PrintRangeInSortedList(500, 1000);
                personArray.PrintRangeInSortedList(1100, 1110);
                personArray.PrintRangeInSortedList(1000, 1001);
                personArray.PrintRangeInSortedList(1013, 1015);
                personArray.PrintRangeInSortedList(1020, 1020);
                personArray.PrintRangeInSortedList(1018, 1025);

            }
            catch (Exception ex) {
                Console.WriteLine("... followig exception caught {0}", ex.Message);
            }  

            Console.WriteLine("\nArrayList testing done!");         

        }
    }
}
