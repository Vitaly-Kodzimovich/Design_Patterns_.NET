using System;

namespace Facade
{
    public class Facade
    {
        protected StandardSettings _StandardSettings;
        
        protected GameLauncher _GameLauncher;

        public Facade(StandardSettings StandardSettings, GameLauncher GameLauncher)
        {
            this._StandardSettings = StandardSettings;
            this._GameLauncher = GameLauncher;
        }
        
        public string Operation()
        {
            string result = $"{this._StandardSettings.SetUserName()}\n";
            result += this._GameLauncher.MainMenu();
            result += "Game was started:\n";
            result += this._StandardSettings.SetDifficulty() + "\n";
            result += this._GameLauncher.LaunchTheGame();
            return result;
        }
    }

    public interface Settings
    {
        public string SetUserName();
        public string SetDifficulty();
    }

    public class StandardSettings : Settings
    {
        private string userName = "User12345";
        private string difficulty = "Easy";

        public string SetUserName()
        {
            return $"... Setting User as {userName} ...";
        }

        public string SetDifficulty()
        {
            return $"... Setting Difficulty as {difficulty} Level ...";
        }
    }



    public interface Launcher
    {
        public string MainMenu();
    }

    public class GameLauncher
    {
        public string MainMenu()
        {
            return "--- GameLauncher started successfully ---\n";
        }

        public string LaunchTheGame()
        {
            return "--- GameLauncher runned the game with settings succesfully ---\n";
        }
    }


    class Client
    {
        public static void ClientCode(Facade facade)
        {
            Console.Write(facade.Operation());
        }
    }
    
    class Program
    {
        static void Main()
        {
            StandardSettings StandardSettings = new StandardSettings();
            GameLauncher GameLauncher = new GameLauncher();
            Facade facade = new Facade(StandardSettings, GameLauncher);
            Client.ClientCode(facade);
        }
    }
}