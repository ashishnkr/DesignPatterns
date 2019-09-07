using System;
using System.Collections.Generic;
using System.Threading;

namespace BuilderInheritance
{
    public class Person
    {
        public string Name;

        public string Position;

        // The below line needs to be changed if PersonJobBuilder is being inherited by other generic class
        // This actually violate Open Close principle. Can be only used in internal APIs
        public class Builder : PersonJobBuilder<Builder> { /* degenerate */ }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";   
        }
    }

    public abstract class PersonBuilder<SELF>
      where SELF : PersonBuilder<SELF>
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder<PersonInfoBuilder<SELF>>
      where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
      where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF)this;
        }
    }

    public class BuilderInheritanceDemo
    {
        static void Main(string[] args)
        {
            var person = Person.New.Called("Ashish").WorksAsA("Software Engineer").Build();
            Console.WriteLine(person);
            Console.ReadLine();
        }
    }
}