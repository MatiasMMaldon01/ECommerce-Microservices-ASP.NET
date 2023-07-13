using Api.Basket.Entities;
using Api.Basket.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Basket.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly gRPCDiscountService.gRPCDiscountService _gRPCDiscountService;

        public BasketController(IBasketRepository repository, gRPCDiscountService.gRPCDiscountService gRPCDiscountService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _gRPCDiscountService = gRPCDiscountService ?? throw new ArgumentNullException(nameof(gRPCDiscountService));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            if (basket == null)
            {
                return NotFound("Opss...There are no items here...");
            }

            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _gRPCDiscountService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            await _repository.UpdateBasket(basket);
            return Ok("Items from Basket created succesfully");
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok("Items from Basket deleted succesfully");
        }

    }
}
