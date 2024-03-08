using System;

namespace Command
{
    public interface ICommand
    {
        void Execute();
    }


    class Command1 : ICommand
    {
        private string _payload = string.Empty;

        public Command1(string payload)
        {
            this._payload = payload;
        }

        public void Execute()
        {
            Console.WriteLine($"Command1: Sending Company Info: ({this._payload})");
        }
    }

    class Command2 : ICommand
    {
        private Engineer _Engineer;
        private Manager _Manager;

        private string _a;
        private string _b;

        public Command2(Engineer Engineer, Manager Manager, string a, string b)
        {
            this._Manager = Manager;
            this._Engineer = Engineer;
            this._a = a;
            this._b = b;
        }

        public void Execute()
        {
            Console.WriteLine("Command2: Starting work on your project.");
            this._Manager.CompanyPresentation();
            this._Engineer.DesignMachine(this._a);
            this._Engineer.SendDesign(this._b);
            this._Manager.SendContacts();
        }
    }



    class Engineer
    {
        public void DesignMachine(string a)
        {
            Console.WriteLine($"***Engineer: Designing Machine: ({a}.)");
        }

        public void SendDesign(string b)
        {
            Console.WriteLine($"***Engineer: Sending Design: ({b}.)");
        }
    }


    class Manager
    {
        public void CompanyPresentation()
        {
            Console.WriteLine(".Manager: Our company 'X-Company' glad to work with you");
        }

        public void SendContacts()
        {
            Console.WriteLine(".Manager: Our Contaсts: + 0-000-000-00");
        }
    }



    class Company
    {
        private ICommand _onStart = null!;

        private ICommand _onFinish = null!;

        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }

        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }

        public void DesignMachineImportant()
        {
            Console.WriteLine("----- Company Welcome Message -----");

            if (this._onStart is ICommand)
            {
                this._onStart.Execute();
            }
            
            Console.WriteLine();
            Console.WriteLine("----- Company Order Message -----");
            
            if (this._onFinish is ICommand)
            {
                this._onFinish.Execute();
            }

            Console.WriteLine();
            Console.WriteLine("----- Company Farewell Message -----");
        }
    }



    class Program
    {
        static void Main()
        {
            Company Company = new Company();
            Company.SetOnStart(new Command1("History of company"));
            Engineer Engineer = new Engineer();
            Manager Manager = new Manager();
            Company.SetOnFinish(new Command2(Engineer, Manager, "Machine B-95", "Standard Form"));

            Company.DesignMachineImportant();
        }
    }
}
