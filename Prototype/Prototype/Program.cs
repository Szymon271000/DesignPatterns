using System;

namespace Prototype
{
    class Program
    {
        public interface IPrototype<T>
        {
            T DeepCopy();
        }
        public class Person: IPrototype<Person>
        {
            public string[] Names;
            public Address Address;

            public Person(string[] names, Address address)
            {
                Names = names;
                Address = address;
            }

            public Person(Person other)
            {
                Names = other.Names;
                Address = new Address(other.Address);
            } //c++ constructor



            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }

            public Person DeepCopy()
            {
                return new Person(Names, Address.DeepCopy());
            }
        }
        public class Address: IPrototype<Address>
        {
            public string StreetName;
            public int HouseNumber;

            public Address(Address address)
            {
                StreetName = address.StreetName;
                HouseNumber = address.HouseNumber;
            }

            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName;
                HouseNumber = houseNumber;
            }

            public Address DeepCopy()
            {
                return new Address(StreetName, HouseNumber);
            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }
        static void Main(string[] args)
        {
            var John = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

            var jane = John.DeepCopy();
            /*var jane = new Person(John);
            jane.Address.HouseNumber = 321;
            var jane = John;
            jane.Names[0] = "Jane";*/


            Console.WriteLine(jane);
            Console.WriteLine(John);
        }
    }

    
}
