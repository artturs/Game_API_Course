﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment5
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);

        Task<Item> CreateItem(Guid playerId, Item item);
        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> UpdateItem(Guid playerId, Item item);
        Task<Item> DeleteItem(Guid playerId, Item item);


        Task<List<Player>> GetPlayersMinScore(int minScore);
        Task<List<Player>> GetPlayersByItemListSize(int amountOfItems);
        Task<Player> UpdatePlayerName(Guid id, string name);
        Task<Item> CreateItemQuery(Guid playerId, Item item);
        Task<List<Player>> GetPlayersByDescendingScore();
    }
}
