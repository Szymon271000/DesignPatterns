using System;
using System.Collections.Generic;
using System.Linq;

namespace Dependency_Inversion
{
    class Program
    {
        public enum Relationship
        {
            Parent, Child, Sibling
        }

        public class Person
        {
            public string Name;
            // public DateTime DateOfBirth;
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        //low-level
        public class Relationships : IRelationshipBrowser
        {
            private List<(Person, Relationship, Person)> relations = new List<(Person, Program.Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }


            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                foreach (var r in relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent))
                {
                    yield return r.Item3;
                }
            }

            //public List<(Person, Relationship, Person)> Relations => relations;
        }

        public class Research
        {
            public  Research (IRelationshipBrowser browser)
            {
                foreach (var p in browser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {p.Name}");
                }
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationship = new Relationships();
            relationship.AddParentAndChild(parent, child1);
            relationship.AddParentAndChild(parent, child2);

            var research = new Research(relationship);
        }
    }
}
