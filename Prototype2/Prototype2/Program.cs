using System;

namespace Prototype2
{
    class Program
    {
        public interface IDeepCopyable<T>
        {
            T DeepCopy();
        }
        public class Address: IDeepCopyable<Address>
        {
            public string StreetName;
            public int HouseNumer;

            public Address()
            {

            }
            public Address(string streetName, int houseNumer)
            {
                StreetName = streetName;
                HouseNumer = houseNumer;
            }

            public Address DeepCopy()
            {
                return (Address)MemberwiseClone();
            }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumer)}: {HouseNumer}";
            }
        }

        public class Person: IDeepCopyable<Person>
        {
            public string[] Names;
            public Address Address;

            public Person()
            {

            }
            public Person(string[] names, Address address)
            {
                Names = names;
                Address = address;
            }

            public Person DeepCopy()
            {
                return new Person((string[])Names.Clone(), Address.DeepCopy());
            }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(",", Names)}, {nameof(Address)}: {Address}";
            }
        }

        public class Employee: Person, IDeepCopyable<Employee>
        {
            public int Salary;

            public Employee()
            {

            }
            public Employee(string[] names, Address address, int salary): base(names, address)
            {
                Salary = salary;
            }

            public override string ToString()
            {
                return $"{base.ToString()}, {nameof(Address)}: {Address}, {nameof(Salary)}: {Salary}";
            }

            Employee IDeepCopyable<Employee>.DeepCopy()
            {
                return new Employee((string[])Names.Clone(), Address.DeepCopy(), Salary);
            }
        }
        static void Main(string[] args)
        {
            var John = new Employee();
            John.Names = new[] { "John", "Doe" };
            John.Address = new Address
            {
                HouseNumer = 123,
                StreetName = "London Road"
            };
            John.Salary = 321000;
            var copy = John.DeepCopy();

            copy.Names[1] = "Smith";
            copy.Address.HouseNumer++;
            

        }
    }
}
