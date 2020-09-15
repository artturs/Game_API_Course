using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assignment3
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
            return Task.FromResult<Player>(player);
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
            return Task.FromResult<Player>(tempPlayer);
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
                    return Task.FromResult<Player>(player);
                }
            }
            return Task.FromResult<Player>(null);
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
            return Task.FromResult<Player[]>(players.ToArray());
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
    }
}
