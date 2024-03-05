using System;

namespace Bridge
{
    class BasicProgram
    {
        protected IOperatingSystem _operatingSystem;
        
        public BasicProgram(IOperatingSystem operatingSystem)
        {
            this._operatingSystem = operatingSystem;
        }
        
        public virtual string StartPage()
        {
            return "-You are using basic version of our program\n" +
                   "Current Operating System: " +
                   _operatingSystem.SystemName() + "\n\n";
        }
    }

    class ExtendedProgram : BasicProgram
    {
        public ExtendedProgram(IOperatingSystem operatingSystem) : base(operatingSystem)
        {
        }
        
        public override string StartPage()
        {
            return "-You are using extended version of our program\n" +
                   "-New features have been added\n"+
                   "Current Operating System: " +
                    base._operatingSystem.SystemName() + "\n\n";
        }
    }


    public interface IOperatingSystem
    {
        string SystemName();
    }

    class WindowsOS : IOperatingSystem
    {
        public string SystemName()
        {
            return "Windows";
        }
    }

    class LinuxOS : IOperatingSystem
    {
        public string SystemName()
        {
            return "Linux";
        }
    }

    class MacOS : IOperatingSystem
    {
        public string SystemName()
        {
            return "MacOS";
        }
    }



    class Client
    {
        public void ClientCode(BasicProgram program)
        {
            Console.Write(program.StartPage());
        }
    }
    


    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            BasicProgram program1;
            program1 = new BasicProgram(new WindowsOS());
            client.ClientCode(program1);
            
            ExtendedProgram program2;
            program2 = new ExtendedProgram(new LinuxOS());
            client.ClientCode(program2);

            BasicProgram program3;
            program3 = new ExtendedProgram(new MacOS());
            client.ClientCode(program3);
        }
    }
}
