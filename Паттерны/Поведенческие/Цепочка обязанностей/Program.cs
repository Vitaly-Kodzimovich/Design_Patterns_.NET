using System;

namespace ChainOfResponsibility
{
    class Receiver
    {
        public bool BankTransfer { get; set; }

        public bool MoneyTransfer { get; set; }

        public bool PayPalTransfer { get; set; }
        public Receiver(bool BankAvailable, bool MoneyPaymentAvailable, bool PayPalAvailable)
        {
            BankTransfer = BankAvailable;
            PayPalTransfer = PayPalAvailable;
            MoneyTransfer = MoneyPaymentAvailable;
        }
    }


    abstract class PaymentHandler
    {
        public PaymentHandler? Successor { get; set; }
        public abstract void Handle(Receiver receiver);
    }
    
    class BankPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.BankTransfer == true)
                Console.WriteLine("Выполняем банковский перевод");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
    
    class PayPalPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.PayPalTransfer == true)
                Console.WriteLine("Выполняем перевод через PayPal");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
    // переводы с помощью системы денежных переводов
    class MoneyPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.MoneyTransfer == true)
                Console.WriteLine("Выполняем перевод через системы денежных переводов");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }

    class NoPaymentHandler : PaymentHandler
    {
        public override void Handle(Receiver receiver)
        {
            Console.WriteLine("Нет доступных способов оплаты");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver1_bank = new Receiver(BankAvailable: true, PayPalAvailable:true, MoneyPaymentAvailable:true);
            Receiver receiver2_paypal = new Receiver(BankAvailable: false, PayPalAvailable:true, MoneyPaymentAvailable:false);
            Receiver receiver3_money = new Receiver(BankAvailable: false, PayPalAvailable:false, MoneyPaymentAvailable:true);
            Receiver receiver4_nopayment = new Receiver(BankAvailable: false, PayPalAvailable:false, MoneyPaymentAvailable:false);

            PaymentHandler bankPaymentHandler   = new BankPaymentHandler();
            PaymentHandler paypalPaymentHandler = new PayPalPaymentHandler();
            PaymentHandler moneyPaymentHandler  = new MoneyPaymentHandler();
            PaymentHandler noPaymentHandler     = new NoPaymentHandler();
            
            bankPaymentHandler.Successor = paypalPaymentHandler;
            paypalPaymentHandler.Successor = moneyPaymentHandler;
            moneyPaymentHandler.Successor = noPaymentHandler;
    
            bankPaymentHandler.Handle(receiver1_bank);
            bankPaymentHandler.Handle(receiver2_paypal);
            bankPaymentHandler.Handle(receiver3_money);
            bankPaymentHandler.Handle(receiver4_nopayment);
        }
    }
}