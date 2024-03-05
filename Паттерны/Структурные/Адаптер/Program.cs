using System;

namespace Adapter
{
    public interface ILorry
    {
        string TransportCar();
    }

    public interface IMachine
    {
        string GetNameOfCar();
    }

    class Mazda : IMachine
    {
        public string GetNameOfCar()
        {
            return "Mazda";
        }
    }

    class Ford : IMachine
    {
        public string GetNameOfCar()
        {
            return "Ford";
        }
    }

    class Lorry : ILorry
    {
        private readonly IMachine _Mazda;

        public Lorry(IMachine Mazda)
        {
            this._Mazda = Mazda;
        }

        public string TransportCar()
        {
            return $"I am transporting: '{this._Mazda.GetNameOfCar()}' cars";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mazda Mazda = new Mazda();
            Ford Ford = new Ford();

            ILorry lorry1 = new Lorry(Mazda);
            ILorry lorry2 = new Lorry(Ford);

            Console.WriteLine(lorry1.TransportCar());
            Console.WriteLine(lorry2.TransportCar());
        }
    }
}
