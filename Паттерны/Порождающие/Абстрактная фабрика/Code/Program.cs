using System;

namespace AbstractFactory
{
    public interface IAbstractFactory
    {
        IAbstractWeapon CreateGun();

        IAbstractCar CreateCar();
    }

    class CarFactory : IAbstractFactory
    {
        public IAbstractWeapon CreateGun()
        {
            return new RocketLauncher();
        }

        public IAbstractCar CreateCar()
        {
            return new Car();
        }
    }

    class BoatFactory : IAbstractFactory
    {
        public IAbstractWeapon CreateGun()
        {
            return new Shotgun();
        }

        public IAbstractCar CreateCar()
        {
            return new Boat();
        }
    }





    public interface IAbstractWeapon
    {
        string UsefulFunctionA();
    }

    class RocketLauncher : IAbstractWeapon
    {
        public string UsefulFunctionA()
        {
            return "ROCKET LAUNCHER";
        }
    }

    class Shotgun : IAbstractWeapon
    {
        public string UsefulFunctionA()
        {
            return "SHOTGUN";
        }
    }





    public interface IAbstractCar
    {
        string UseCar();

        string UseCarWithGun(IAbstractWeapon collaborator);
    }

    class Car : IAbstractCar
    {
        public string UseCar()
        {
            return "Car drive";
        }

        public string UseCarWithGun(IAbstractWeapon collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"Car drive with the {result}";
        }
    }

    class Boat : IAbstractCar
    {
        public string UseCar()
        {
            return "Boat drive";
        }

        public string UseCarWithGun(IAbstractWeapon collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"Boat drive with the {result}";
        }
    }






    class Client
    {
        public void Main()
        {
            Console.WriteLine("Client using Car Factory");
            ClientMethod(new CarFactory());
            Console.WriteLine();

            Console.WriteLine("Client using Boat Factory");
            ClientMethod(new BoatFactory());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var productA = factory.CreateGun();
            var productB = factory.CreateCar();

            Console.WriteLine(productB.UseCar());
            Console.WriteLine(productB.UseCarWithGun(productA));
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}