using System;

namespace Flyweight 
{
    abstract class House
    {
        protected int stages; 

        public abstract void Build(double longitude, double latitude);
    }

    class PanelHouse : House 
    {
        public PanelHouse()
        {
            stages = 16;
        }

        public override void Build(double longitude, double latitude)
        {
            Console.WriteLine("Построен панельный дом из 16 этажей;  координаты: {0} широты и {1} долготы", 
                latitude, longitude);
        }
    }

    class BrickHouse : House
    {
        public BrickHouse()
        {
            stages = 5;
        }

        public override void Build(double longitude, double latitude)
        {
            Console.WriteLine("Построен кирпичный дом из 5 этажей;   координаты: {0} широты и {1} долготы",
                latitude, longitude);
        }
    }


    class HouseFactory
    {
        Dictionary<string, House> houses = new Dictionary<string, House>();
        public HouseFactory()
        {
            houses.Add("Panel", new PanelHouse());
            houses.Add("Brick", new BrickHouse());
        }

        public House? GetHouse(string key)
        {
            if (houses.ContainsKey(key))
                return  houses[key];
            else
                return null;
        }
    }


    static class OperationsWithHouses
    {
        public static void BuildSomeHouses (int count, string typeOfHouse, HouseFactory houseFactory, ref double longitude, ref double latitude, double distanceBetween)
        {
            for (int i = 0; i < count;i++)
            {
                House? panelHouse = houseFactory.GetHouse(typeOfHouse);
                if (panelHouse != null)
                    panelHouse.Build(longitude, latitude);

                longitude += distanceBetween;
                latitude  += distanceBetween;
                longitude  = Math.Round(longitude,4);
                latitude   = Math.Round(latitude,4);
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            double longitude = 37.61;
            double latitude = 55.74;

            HouseFactory houseFactory = new HouseFactory();

            OperationsWithHouses.BuildSomeHouses( 3, "Panel", houseFactory, ref longitude, ref latitude, distanceBetween:0.2);
            Console.WriteLine();
            OperationsWithHouses.BuildSomeHouses( 4, "Brick", houseFactory, ref longitude, ref latitude, distanceBetween:0.1);
        }
    }
}