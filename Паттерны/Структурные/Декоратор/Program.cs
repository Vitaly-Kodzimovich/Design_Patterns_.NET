using System;

namespace Decorator
{
    public abstract class Component
    {
        public abstract string Encrypt();
    }


    class NotEncryptedComponent : Component
    {
        public override string Encrypt()
        {
            return "Not Encrypted Component";
        }
    }

    abstract class Cryptographer : Component
    {
        protected Component _component;

        public Cryptographer(Component component)
        {
            this._component = component;
        }

        public void SetComponent(Component component)
        {
            this._component = component;
        }

        public override string Encrypt()
        {
            if (this._component != null)
            {
                return this._component.Encrypt();
            }
            else
            {
                return string.Empty;
            }
        }
    }



    class CaesarCryptographer : Cryptographer
    {
        public CaesarCryptographer(Component comp) : base(comp)
        {
        }

        public override string Encrypt()
        {
            return $"CaesarCryptographer({base.Encrypt()})";
        }
    }

    class CustomCryptographer : Cryptographer
    {
        public CustomCryptographer(Component comp) : base(comp)
        {
        }

        public override string Encrypt()
        {
            return $"CustomCryptographer({base.Encrypt()})";
        }
    }
    


    public class Client
    {
        public void ClientCode(Component component)
        {
            Console.WriteLine("RESULT: " + component.Encrypt());
        }
    }
    


    class Program
    {
        static void Main()
        {
            Client client = new Client();

            var simple = new NotEncryptedComponent();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(simple);
            Console.WriteLine();

            CaesarCryptographer Cryptographer1 = new CaesarCryptographer(simple);
            CustomCryptographer Cryptographer2 = new CustomCryptographer(Cryptographer1);
            CaesarCryptographer Cryptographer3 = new CaesarCryptographer(Cryptographer2);
            Console.WriteLine("Client: Now I've got a decorated components:");
            client.ClientCode(Cryptographer3);
        }
    }
}
