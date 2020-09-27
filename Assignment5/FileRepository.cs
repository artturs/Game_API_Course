using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assignment5
{
    public class FileRepository : IRepository
    {
        public Task<Player> Create(Player player)
        {
            using (StreamWriter sw = File.AppendText(@"game-dev.txt"))
            {
                string json = JsonConvert.SerializeObject(player);
                sw.WriteLine(json);
            }
            return Task.FromResult(player);
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            bool playerFound = false;
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player player = new Player();

            foreach(var line in lines)
            {
                player = JsonConvert.DeserializeObject<Player>(line);

                if (player.Id == playerId)
                {
                    playerFound = true;
                    player.PlayerItems.Add(item);
                }

                newLines.Add(JsonConvert.SerializeObject(player));
            }

            if (!playerFound)
            {
                throw new NotFoundException();
            }

            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(item);
        }





        public Task<Player> Delete(Guid id)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player[] players = new Player[lines.Length];
            Player tempPlayer = new Player();

            foreach (var line in lines)
            {
                tempPlayer = JsonConvert.DeserializeObject<Player>(line);

                if (tempPlayer.Id == id)
                {
                    continue;
                }
                else
                {
                    newLines.Add(line);
                }
            }
            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(tempPlayer);
        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player player = new Player();
            Item deletedItem = new Item();

            foreach (var line in lines)
            {
                player = JsonConvert.DeserializeObject<Player>(line);

                if (player.Id == playerId)
                {

                    foreach (var playerItem in player.PlayerItems)
                    {
                        if (playerItem.Id == item.Id)
                        {
                            deletedItem = playerItem;
                            player.PlayerItems.Remove(playerItem);
                        }
                    }
                }

                newLines.Add(JsonConvert.SerializeObject(player));
            }

            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(deletedItem);
        }





        public Task<Player> Get(Guid id)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            Player player = new Player();

            foreach (var line in lines)
            {
                player = JsonConvert.DeserializeObject<Player>(line); 

                if (player.Id == id)
                {
                    return Task.FromResult(player);
                }
            }
            return Task.FromResult<Player>(null);
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            Player player = new Player();

            foreach (var line in lines)
            {
                player = JsonConvert.DeserializeObject<Player>(line);

                if (player.Id == playerId)
                {
                    foreach (var playerItem in player.PlayerItems)
                    {
                        if (playerItem.Id == itemId)
                        {
                            return Task.FromResult(playerItem);
                        }
                    }
                }
            }
            return null;
        }





        public Task<Player[]> GetAll()
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<Player> players = new List<Player>();

            foreach (var line in lines)
            {
                Player player = JsonConvert.DeserializeObject<Player>(line);
                players.Add(player);
            }
            return Task.FromResult(players.ToArray());
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            Player player = new Player();

            foreach (var line in lines)
            {
                player = JsonConvert.DeserializeObject<Player>(line);

                if (player.Id == playerId)
                {
                    return Task.FromResult(player.PlayerItems.ToArray());
                }
            }

            return null;
        }





      

        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player tempPlayer = new Player();

            foreach (var line in lines)
            {
                tempPlayer = JsonConvert.DeserializeObject<Player>(line);

                if (tempPlayer.Id == id)
                {
                    tempPlayer.Score = player.Score;
                }

                newLines.Add(JsonConvert.SerializeObject(tempPlayer));
            }
            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(tempPlayer);
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            string[] lines = File.ReadAllLines(@"game-dev.txt");
            List<string> newLines = new List<string>();
            Player player = new Player();
            Item modifiedItem = new Item();

            foreach (var line in lines)
            {
                player = JsonConvert.DeserializeObject<Player>(line);

                if (player.Id == playerId)
                {
                    foreach (var playerItem in player.PlayerItems)
                    { 
                        if (playerItem.Id == item.Id)
                        {
                            playerItem.Level = item.Level;
                            modifiedItem = playerItem;
                        }
                    }
                }

                newLines.Add(JsonConvert.SerializeObject(player));
            }
            File.WriteAllLines("game-dev.txt", newLines.ToArray());
            return Task.FromResult(modifiedItem);
        }
    }
}
