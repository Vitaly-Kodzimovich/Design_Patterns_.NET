using System;
using System.Threading;

namespace Singleton
{
    // Singleton realization (not thread-safe)
    public sealed class Singleton_unsafe
    {
        private Singleton_unsafe() { }

        private static Singleton_unsafe _instance = null!;

        public string? Name  { get; set; }

        public static Singleton_unsafe GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton_unsafe();
            }
            return _instance;
        }

        public void someBusinessLogic()
        {
            Console.WriteLine(Name);
        }
    }

    // Singleton realization (thread-safe)
    class Singleton_safe
    {
        private Singleton_safe() { }

        private static Singleton_safe _instance = null!;
        private static readonly object _lock = new object();
        public string? Name { get; set; }

        public static Singleton_safe GetInstance(string name)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton_safe();
                        _instance.Name = name;
                    }
                }
            }
            return _instance;
        }
    }

    // Singleton without locks realization (thread-safe) 
    public sealed class Singleton_without_locks
    {
        private Singleton_without_locks()
        {
        }

        public static Singleton_without_locks Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Singleton_without_locks instance = new Singleton_without_locks();
        }
    }

    // Singleton lazy realization with using System.Lazy<T> (thread-safe)
    public sealed class Singleton_lazy
    {
        private static readonly Lazy<Singleton_lazy> lazy =
            new Lazy<Singleton_lazy>(() => new Singleton_lazy());

        public static Singleton_lazy Instance { get { return lazy.Value; } }

        private Singleton_lazy()
        {
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            // Singleton_unsafe realization (result: BB)
            Singleton_unsafe s1 = Singleton_unsafe.GetInstance();
            s1.Name = "A";
            Singleton_unsafe s2 = Singleton_unsafe.GetInstance();
            s2.Name = "B";

            if (s1 == s2)
            {
                Console.WriteLine("Singleton_unsafe works, both variables contain the same instance.");
                Console.WriteLine($"s1 name = {s1.Name}");
                Console.WriteLine($"s2 name = {s2.Name}");
            }
            else
            {
                Console.WriteLine("Singleton_unsafe failed, variables contain different instances.");
            }






            // Singleton_safe realization (result: AA or BB(rare situation) )
            Console.WriteLine();
            Console.WriteLine("Singleton_safe realisation");

            Thread process1 = new Thread(() =>
            {
                string name = "A";
                Singleton_safe singleton = Singleton_safe.GetInstance(name);
                Console.WriteLine("s1 name = " + singleton.Name);
            });
            Thread process2 = new Thread(() =>
            {
                string name = "B";
                Singleton_safe singleton = Singleton_safe.GetInstance(name);
                Console.WriteLine("s2 name = " + singleton.Name);
            });
            
            process1.Start();
            process2.Start();
            
            process1.Join();
            process2.Join();



        }
    }
}