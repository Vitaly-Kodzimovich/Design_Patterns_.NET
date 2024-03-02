using System;

namespace Builder
{
    public interface IBuilder
    {
        void BuildAttic();
        
        void BuildResidentalFloor();
        
        void BuildBasement();
    }
    
    public class ConcreteBuilder : IBuilder
    {
        private House _House = new House();
        
        public ConcreteBuilder()
        {
            this.Reset();
        }
        
        public void Reset()
        {
            this._House = new House();
        }
        
        public void BuildAttic()
        {
            this._House.Add("Attic");
        }
        
        public void BuildResidentalFloor()
        {
            this._House.Add("Residental Floor");
        }
        
        public void BuildBasement()
        {
            this._House.Add("Basement");
        }
        
        public House GetHouse()
        {
            House result = this._House;

            this.Reset();

            return result;
        }
    }
    
    public class House
    {
        private List<object> _parts = new List<object>();
        
        public void Add(string part)
        {
            this._parts.Add(part);
        }
        
        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); // removing last ", "

            return "House parts: " + str + "\n";
        }
    }
    
    public class Director
    {
        private IBuilder _builder = null!;
        
        public IBuilder Builder
        {
            set { _builder = value; } 
        }
        
        public void BuildMinimalViableHouse()
        {
            this._builder.BuildResidentalFloor();
        }
        
        public void BuildFullFeaturedHouse()
        {
            this._builder.BuildAttic();
            this._builder.BuildResidentalFloor();
            this._builder.BuildBasement();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;
            
            Console.WriteLine("Cheap House:");
            director.BuildMinimalViableHouse();
            Console.WriteLine(builder.GetHouse().ListParts());

            Console.WriteLine("Standard House:");
            director.BuildFullFeaturedHouse();
            Console.WriteLine(builder.GetHouse().ListParts());

            Console.WriteLine("Custom House:");
            builder.BuildAttic();
            builder.BuildResidentalFloor();
            builder.BuildResidentalFloor();
            builder.BuildBasement();
            Console.Write(builder.GetHouse().ListParts());
        }
    }
}
