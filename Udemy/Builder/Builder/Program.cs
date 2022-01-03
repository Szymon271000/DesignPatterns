using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder
{
    public class Person
    {
        // address
        public string StreetAddress, Postcode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }
    
    public class PersonBuilder
    {
        //reference !
        protected Person person = new Person();
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb;
        }
    }

    public class PersonAddressBuilder: PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostCode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }

    }
    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder (Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder ASA (string position)
        {
            person.Position = position;
            return this;
        }

        public PersonBuilder Earning (int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }
    //************************************************************
    /*public class Person
    {
        public string Name, Position;
    }

    public abstract class FunctionalBuilder<TSubject,TSelf>
        where TSelf : FunctionalBuilder<TSubject, TSelf>
        where TSubject: new()
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();
        public TSelf Called(string name)
            => Do(p => p.Name = name);
        public TSelf Do(Action<Person> action)
            => AddAction(action);

        public Person Build()
            => actions.Aggregate(new Person(), (p, f) => f(p));
        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p); return p; });
            return (TSelf)this;
        }
    }

    public sealed class PersonBuilder: FunctionalBuilder<Person, PersonBuilder>
    {
        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
    }*/
    //*****************************************************


    //*****************************************************
    /*public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();

        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
        public PersonBuilder Do(Action<Person> action)
            => AddAction(action);

        public Person Build()
            => actions.Aggregate(new Person(), (p, f) => f(p));
        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(p => { action(p); return p; });
            return this;
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorkAs(this PersonBuilder builder, string position) => builder.Do(p => p.Position = position);
    }*/
    //**************************************************************


    // 
    //************************************************************************
    /*public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        private class Impl :
            ISpecifyCarType,
            ISpecifyWheelSize,
            IBuildCar
        {
            private Car car = new Car();
            public Car Build()
            {
                return car;
            }

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                        break;
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.Type}");
                }
                car.WheelSize = size;
                return this;
            }
        }
        public static ISpecifyCarType Create()
        {
            return new Impl(); 
        }
            
    }
    
    
    //**************************************************

    //**************************************************
    /*public class Person
    {
        public string Name;
        public string Position;

        public class Builder: PersonJobBuilder<Builder>
        {
            
        }

        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"{nameof(Name)} {Name}, {nameof(Position)} {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    // class Foo: Bar<Foo>
    public class PersonInfoBuilder<SELF> : PersonBuilder
        where SELF: PersonInfoBuilder<SELF>
    {
        public SELF Called (string name)
        {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder <SELF>: PersonInfoBuilder<SELF> where SELF: PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }
    //*************************************************

    //*************************************************
    /*public class HtmlElement
    {
        public string Name;
        public string Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int intendSize = 2;

        public HtmlElement()
        {

        }
        public HtmlElement(string name, string text) 
        {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', intendSize * indent);
            sb.AppendLine($"{i} <{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                //sb.Append(' ', intendSize, (indent + 1));
                //sb.AppendLine(Text);
            }
            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i} </{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

    }
    public class HtmlBuilder
    {
        private readonly string rootname;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootname)
        {
            this.rootname = rootname;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootname };
        }
    }*/
    //*******************************************************
    class Program
    {
        static void Main(string[] args)
        {
            /*var hellow = "hellow";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hellow);
            sb.Append("<p>");
            Console.WriteLine(sb);

            var words = new[] { "hellow", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat($"<li> {0} <li>,{word}");
            }
            sb.Append("</ul>");
            Console.WriteLine(sb);*/

            /*var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "hello");
            Console.WriteLine(builder.ToString());*/


            /*var Car = CarBuilder.Create() //ISpecifyCarType
            .OfType(CarType.Crossover)  //ISpecifyWheelSize
            .WithWheels(18) //IBuildCar
            .Build();*/
            /*var Person = new PersonBuilder()
            .Called("Sarah")
            .Build();*/
            var pb = new PersonBuilder();
            Person person = pb
            .Lives.At("123 London Road")
                .In("London")
                .WithPostCode("SW12AC")
            .Works.At("Fabrikam")
                .ASA("Engineer")
                .Earning(123000);
            Console.WriteLine(person);
        }
    }
}
