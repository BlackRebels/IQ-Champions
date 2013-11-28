using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDecorators
{
    public abstract class Coffee
    {
        public abstract string Ingredients { get;  }
        public abstract double Cost { get; }

        public override string ToString()
        {
            return Ingredients + " " + Cost + " ft";
        }

    }
    public class SimpleCoffee : Coffee
    {
        public override string Ingredients
        {
            get
            { return "water, coffee"; }
        }
        public override double Cost
        {
            get { return 100; }
        }
    }

    public abstract class CoffeeDecorator : Coffee
    {
        protected Coffee coffee;
        public CoffeeDecorator(Coffee coffee)
        {
            this.coffee = coffee;
        }
    }

    public class Sugar : CoffeeDecorator
    {
        public Sugar(Coffee coffee)
            : base(coffee){}
        public override string Ingredients
        {
            get { return coffee.Ingredients + ", sugar"; }
        }
        public override double Cost
        {
            get { return coffee.Cost + 20; }
        }
    }

    public class Milk : CoffeeDecorator
    {
        public Milk(Coffee coffee)
            : base(coffee) { }
        public override string Ingredients
        {
            get { return coffee.Ingredients + ", milk"; }
        }
        public override double Cost
        {
            get { return coffee.Cost + 40; }
        }
    }
}
