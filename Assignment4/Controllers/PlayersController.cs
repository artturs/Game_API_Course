using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment4.Controllers
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
                CreationTime = DateTime.Now
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

    }
}
