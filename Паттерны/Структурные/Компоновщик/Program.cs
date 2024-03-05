using System;

namespace Composite
{
    abstract class Component
    {
        public abstract string NamesOperation();
        public abstract double PriceOperation();

        public virtual void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsPack()
        {
            return true;
        }
    }



    class Product1 : Component
    {
        private static double price = 5.50;
        private static string name = "Product1";

        public override string NamesOperation()
        {
            return $"{name}({price}$)";
        }
        public override double PriceOperation()
        {
            return price;
        }

        public override bool IsPack()
        {
            return false;
        }
    }

    class Product2 : Component
    {
        private static double price = 10.50;
        private static string name = "Product2";

        public override string NamesOperation()
        {
            return $"{name}({price}$)";
        }
        public override double PriceOperation()
        {
            return price;
        }

        public override bool IsPack()
        {
            return false;
        }
    }

    class Product3 : Component
    {
        private static double price = 18.0;
        private static string name = "Product3";

        public override string NamesOperation()
        {
            return $"{name}({price}$)";
        }
        public override double PriceOperation()
        {
            return price;
        }

        public override bool IsPack()
        {
            return false;
        }
    }



    class Pack : Component
    {
        protected List<Component> _childrenComponent = new List<Component>();
        
        public override void Add(Component component)
        {
            this._childrenComponent.Add(component);
        }

        public override void Remove(Component component)
        {
            this._childrenComponent.Remove(component);
        }

        

        public override string NamesOperation()
        {
            int i = 0;
            string result = $"Pack|{PriceOperation()}$|:(";

            foreach (Component component in this._childrenComponent)
            {
                result += component.NamesOperation();
                if (i != this._childrenComponent.Count - 1)
                {
                    result += " + ";
                }
                i++;
            }
            
            return result + ")";
        }

        public override double PriceOperation()
        {
            int i = 0;
            double price = 0;

            foreach (Component component in this._childrenComponent)
            {
                price += component.PriceOperation();
                i++;
            }

            return price;
        }
    }

    class Client
    {        public void ClientCode(Component product)
        {
            Console.WriteLine($"RESULT: {product.NamesOperation()}");
            Console.WriteLine($"RESULT: {product.PriceOperation()}$\n");
        }

        public void ClientCode2(Component component1, Component component2)
        {
            if (component1.IsPack())
            {
                component1.Add(component2);
            }
            
            Console.WriteLine($"RESULT: {component1.NamesOperation()}");
            Console.WriteLine($"RESULT: {component1.PriceOperation()}$\n");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            Product1 product1 = new Product1();
            Product3 product3 = new Product3();
            Console.WriteLine("Client: Get a simple component:");
            client.ClientCode(product1);

            Pack tree = new Pack();
            Pack pack1 = new Pack();
            pack1.Add(new Product1());
            pack1.Add(new Product2());
            Pack pack2 = new Pack();
            pack2.Add(new Product1());
            tree.Add(pack1);
            tree.Add(pack2);
            Console.WriteLine("Client: Get a pack of some components:");
            client.ClientCode(tree);

            Console.Write("Client: Get a pack of separate components\n");
            client.ClientCode2(tree, product3);
        }
    }
}
