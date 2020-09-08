using System;
using System.Linq;
using System.Collections.Generic;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {

            Tehtava1();
            //Tehtava2();
            //Tehtava3();
            //Tehtava4();
            //Tehtava5();
            //Tehtava6();
            //Tehtava7();


            Console.ReadLine();
        }

        // Tehtävä 1
        public static void Tehtava1()
        {
            Player[] allPlayers;
            int numberOfPlayers = 1000000;
            allPlayers = InstantiatePlayers(numberOfPlayers);

            Console.WriteLine(CheckForDuplicates(allPlayers));
        }
        // Tehtävä 2
        public static void Tehtava2()
        {
            Player[] allPlayers;
            int numberOfPlayers = 1;
            allPlayers = InstantiatePlayers(numberOfPlayers);

            GiveItemsToPlayers(allPlayers, 0, 15);

            Console.WriteLine("ID: " + allPlayers[0].GetHighestLevelItem().Id + "  Level: " + allPlayers[0].GetHighestLevelItem().Level);
        }

        // Tehtävä 3

        public static void Tehtava3()
        {
            Player[] allPlayers;
            int numberOfPlayers = 1;
            allPlayers = InstantiatePlayers(numberOfPlayers);

            GiveItemsToPlayers(allPlayers, 0, 15);

            PrintLoop(GetItems(allPlayers[0]));

            Console.WriteLine("");

            PrintLoop(allPlayers[0].GetItemsWithLinq());
        }

        // Tehtävä 4
        public static void Tehtava4()
        {
            Player[] allPlayers;
            int numberOfPlayers = 1;
            allPlayers = InstantiatePlayers(numberOfPlayers);

            GiveItemsToPlayers(allPlayers, 0, 15);

            Console.WriteLine(FirstItem(allPlayers[0]).Id);

            Console.WriteLine("");

            Console.WriteLine(allPlayers[0].FirstItemWithLinq().Id);
        }


        // Tehtävä 5
        public static void Tehtava5()
        {
            Player[] allPlayers;
            int numberOfPlayers = 1;
            allPlayers = InstantiatePlayers(numberOfPlayers);

            GiveItemsToPlayers(allPlayers, 0, 15);

            Action<Item> action = PrintItem;

            ProcessEachItem(allPlayers[0], action);
        }

        // Tehtävä 6
        public static void Tehtava6()
        {
            Player[] allPlayers;
            int numberOfPlayers = 1;
            allPlayers = InstantiatePlayers(numberOfPlayers);

            GiveItemsToPlayers(allPlayers, 0, 15);

            Action<Player, Action<Item>> Processeachitem = (player, process) =>
            {
                foreach (Item item in player.Items) { process(item); }
            };

            Action<Item> action = (item) => Console.WriteLine("Id: " + item.Id + "  Level: " + item.Level);

            ProcessEachItem(allPlayers[0], action);
        }

        // Tehtävä 7
        public static void Tehtava7()
        {
            var allPlayers = InstantiatePlayers(15);
            Random rnd = new Random();
            foreach (var player in allPlayers) { player.Score = rnd.Next(0, 30); }

            var allOtherPlayers = new PlayerFromAnotherGame[15];
            for (int i = 0; i < allOtherPlayers.Length; i++)
            {
                allOtherPlayers[i] = new PlayerFromAnotherGame { Score = rnd.Next(0, 30) };
            }

            Game<Player> game = new Game<Player>(allPlayers.ToList());
            var topTen = game.GetTop10Players();
            foreach (var player in topTen)
            {
                Console.WriteLine(player.Score);
            }
            Console.WriteLine("");

            Game<PlayerFromAnotherGame> gameOther = new Game<PlayerFromAnotherGame>(allOtherPlayers.ToList());
            var topTenOther = gameOther.GetTop10Players();
            foreach (var player in topTenOther)
            {
                Console.WriteLine(player.Score);
            }
        }


        // Player Control Functions

        public static void GiveItemsToPlayers(Player[] players, int lvlMin, int lvlMax)
        {
            Random rnd = new Random();
            foreach (Player player in players)
            {
                player.Items = new List<Item> { new Item { Id = Guid.NewGuid(), Level = rnd.Next(lvlMin, lvlMax) },
                new Item { Id = Guid.NewGuid(), Level = rnd.Next(lvlMin, lvlMax) }, new Item { Id = Guid.NewGuid(), Level = rnd.Next(lvlMin, lvlMax) } };
            }
        }

        public static Player[] InstantiatePlayers(int amount)
        {
            Player[] players = new Player[amount];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player { Id = Guid.NewGuid() };
            }

            return players;
        }

        public static bool CheckForDuplicates(Player[] players)
        {

            for (int i = 0; i < players.Length; i++)
            {
                for (int j = i + 1; j < players.Length; j++)
                {
                    if (players[i].Id == players[j].Id)
                        return true;
                }
            }

            return false;
        }

        public static Item[] GetItems(Player player)
        {
            Item[] items = new Item[player.Items.Count];
            for (int i = 0; i < player.Items.Count; i++)
            {
                items[i] = player.Items[i];
            }
            return items;
        }

        static void PrintLoop(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine("ID: " + items[i].Id + "  Level: " + items[i].Level);
            }
        }

        static Item FirstItem(Player player)
        {
            if(player.Items[0] != null) { return player.Items[0]; }
            else { return null; }
        }

        public static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach (Item item in player.Items)
            {
                process(item);
            }
        }

        public static void PrintItem(Item item)
        {
            Console.WriteLine("Id: " + item.Id + "  Level: " + item.Level);
        }
    }
}
