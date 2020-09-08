using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment2
{
    public class Player : IPlayer
    {

        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }


    }
    public static class PlayerExtensions
    {
        public static Item GetHighestLevelItem(this Player player)
        {
            Item highest = player.Items[0];
            foreach (Item item in player.Items)
            {
                if (item.Level > highest.Level) { highest = item; }
            }
            return highest;
        }

        public static Item[] GetItemsWithLinq(this Player player)
        { return player.Items.ToArray(); }

        public static Item FirstItemWithLinq(this Player player)
        { return player.Items.FirstOrDefault(); }
    }


    public class Item
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
    }


    public class Game<T> where T : IPlayer
    {
        private List<T> _players;

        public Game(List<T> players)
        {
            _players = players;
        }

        public T[] GetTop10Players()
        {
            T[] topTen = new T[10];

            _players = _players.OrderBy(player => player.Score).ToList();
            for (int j = 0, i = _players.Count - 1; j < 10; j++, i--)
            {
                topTen[j] = _players[i];
            }

            return topTen;
        }
    }

    public class PlayerFromAnotherGame : IPlayer
    {
        public int Score { get; set; }
    }
}
