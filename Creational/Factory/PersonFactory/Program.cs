using System;

namespace PersonFactory
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private  Person()
        {
            
        }

        public class PersonFactory
        {
            private static int Id;
            public static Person CreatePerson(string name)
            {
                var person = new Person { Name = name , Id = Id};
                Id++;
                return person;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person1 = Person.PersonFactory.CreatePerson("Bob");
            var person2 = Person.PersonFactory.CreatePerson("Paul");
        }   
    }
}
