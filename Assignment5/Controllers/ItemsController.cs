using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assignment5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository repository;

        public ItemsController(IRepository repo)
        {
            repository = repo;
        }

        [HttpPost("/players/{playerId}/items/create")]
        public Task<Item> CreateItem(Guid playerId, NewItem item)
        {

            Item newItem = new Item()
            {
                Id = Guid.NewGuid(),
                Level = item.Level,
                Type = item.Type,
                CreationDate = DateTime.Today
            };
            Console.WriteLine("LOLOLOL");
            return repository.CreateItem(playerId, newItem);
        }
        [HttpGet("/players/{playerId}/items/get/{itemId}")]
        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            return repository.GetItem(playerId, itemId);
        }

        [HttpGet("/players/{playerId}/items/all")]
        public Task<Item[]> GetAllItems(Guid playerId)
        {
            return repository.GetAllItems(playerId);
        }

        [HttpPut("/players/{playerId}/items/modify/{itemId}")]
        public Task<Item> UpdateItem(Guid playerId, Guid itemId, ModifiedItem modifiedItem)
        {
            Item newItem = new Item()
            {
                Id = itemId,
                Level = modifiedItem.Level,
                Type = modifiedItem.Type,
                CreationDate = DateTime.Today
            };

            return repository.UpdateItem(playerId, newItem);
        }

        [HttpDelete("/players/{playerId}/items/delete/{item}")]
        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            return repository.DeleteItem(playerId, item);
        }



        [HttpPost("/players/{playerId}/items/createquery")]
        public Task<Item> CreateItemQuery(Guid playerId, NewItem item)
        {

            Item newItem = new Item()
            {
                Id = Guid.NewGuid(),
                Level = item.Level,
                Type = item.Type,
                CreationDate = DateTime.Today
            };

            return repository.CreateItemQuery(playerId, newItem);
        }

    }
}