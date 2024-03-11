using System;

namespace Mediator
{
    public interface IMediator
    {
        void Notify(object sender, string operation);
    }

    class ConcreteMediator : IMediator
    {
        private Component1_AB _Component1_AB;

        private Component2_CD _Component2_CD;

        public ConcreteMediator(Component1_AB Component1_AB, Component2_CD Component2_CD)
        {
            this._Component1_AB = Component1_AB;
            this._Component1_AB.SetMediator(this);
            this._Component2_CD = Component2_CD;
            this._Component2_CD.SetMediator(this);
        } 

        public void Notify(object sender, string operation)
        {
            if (operation == "A")
            {
                Console.WriteLine("Mediator reacts on A and triggers following operations:");
                this._Component2_CD.DoC();
            }
            if (operation == "D")
            {
                Console.WriteLine("Mediator reacts on D and triggers following operations:");
                this._Component1_AB.DoB();
                this._Component2_CD.DoC();
            }
        }
    }



    class BaseComponent
    {
        protected IMediator _mediator;

        public BaseComponent(IMediator mediator = null!)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    class Component1_AB : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("Component 1 does A.");

            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component 1 does B.");

            this._mediator.Notify(this, "B");
        }
    }

    class Component2_CD : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("Component 2 does C.");

            this._mediator.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("Component 2 does D.");

            this._mediator.Notify(this, "D");
        }
    }
    


    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код.
            Component1_AB Component1_AB = new Component1_AB();
            Component2_CD Component2_CD = new Component2_CD();
            new ConcreteMediator(Component1_AB, Component2_CD);

            Console.WriteLine("Client triggers operation A.");
            Component1_AB.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation D.");
            Component2_CD.DoD();
        }
    }
}
