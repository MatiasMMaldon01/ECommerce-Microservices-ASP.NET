﻿using Api.Basket.Entities;
using System.Threading.Tasks;

namespace Api.Basket.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }
}