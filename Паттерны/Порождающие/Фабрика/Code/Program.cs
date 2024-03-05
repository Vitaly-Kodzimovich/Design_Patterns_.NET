using System;

namespace FactoryMethod
{
    abstract class MachineFactory
    {
        public abstract IMachine CreateMachine();

        public string GetClientMachineName()
        {
            var machine = CreateMachine();
            var result = "Machine Name: " + machine.GetNameOfMachine();

            return result;
        }

        public string GetClientMachineSpeed()
        {
            var machine = CreateMachine();
            var result = "Machine Speed: " + machine.GetSpeedOfMachine();

            return result;
        }
        
    }





    class AirMachineCreator : MachineFactory
    {
        public override IMachine CreateMachine()
        {
            return new AirMachine();
        }
    }

    class EarthMachineCreator : MachineFactory
    {
        public override IMachine CreateMachine()
        {
            return new EarthMachine();
        }
    }





    public interface IMachine
    {
        string GetNameOfMachine();
        string GetSpeedOfMachine();
    }

    class AirMachine : IMachine
    {
        public string GetNameOfMachine()
        {
            return "AirMachine";
        }

        public string GetSpeedOfMachine()
        {
            return "340 km/h";
        }
    }

    class EarthMachine : IMachine
    {
        public string GetNameOfMachine()
        {
            return "EarthMachine";
        }

        public string GetSpeedOfMachine()
        {
            return "90 km/h";
        }
    }








    class Client
    {
        public void Main()
        {
            Console.WriteLine("Creating machine with the AirMachineCreator.");
            ClientCode(new AirMachineCreator());
            
            Console.WriteLine("");

            Console.WriteLine("Creating machine with the EarthMachineCreator.");
            ClientCode(new EarthMachineCreator());
        }

        public void ClientCode(MachineFactory creator)
        {

            Console.WriteLine("Client using this machine:\n" + creator.GetClientMachineName());
            Console.WriteLine(creator.GetClientMachineSpeed());

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
