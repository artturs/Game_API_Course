using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment5.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {

        private readonly IRepository repository;
        public PlayersController(IRepository fileRepository)
        {
            repository = fileRepository;
        }

        [HttpGet("/players/{id}")]
        public Task<Player> Get(Guid id)
        {
            return repository.Get(id);
        }

        [HttpGet("/players/all")]
        public Task<Player[]> GetAll()
        {
            return repository.GetAll();
        }

        [HttpPost("/players/create")]
        public Task<Player> Create(Player player)
        {
            Player newPlayer = new Player()
            {
                Name = player.Name,
                Id = Guid.NewGuid(),
                CreationTime = DateTime.Now,
                PlayerItems = new List<Item>()
            };
            return repository.Create(newPlayer);
        }

        [HttpPut("/players/modify/{id}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            return repository.Modify(id, player);
        }

        [HttpDelete("/players/delete/{id}")]
        public Task<Player> Delete(Guid id)
        {
            return repository.Delete(id);
        }


        [HttpGet("/players")]
        public Task<List<Player>> GetPlayersMinScore([FromQuery] int score)
        {
            return repository.GetPlayersMinScore(score);
        }

        [HttpPut("/players/{id}/updatename")]
        public Task<Player> UpdatePlayerName(Guid id, [FromQuery] string name)
        {
            return repository.UpdatePlayerName(id, name);
        }

        [HttpGet("/players/items")]
        public Task<List<Player>> GetPlayersByItemListSize([FromQuery] int size)
        {
            return repository.GetPlayersByItemListSize(size);
        }

        [HttpGet("/players/descendingscore")]
        public Task<List<Player>> GetPlayersByDescendingScore()
        {
            return repository.GetPlayersByDescendingScore();
        }


    }
}
