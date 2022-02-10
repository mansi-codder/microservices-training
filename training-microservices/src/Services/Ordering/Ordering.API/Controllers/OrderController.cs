using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ordering.API.Entities;
using Ordering.API.Repositories;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepositoryRedis _repository;

        public OrderController(IOrderRepositoryRedis orderRepository)
        {
            _repository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public  async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserName(string userName)
        {
            var orders =  await _repository.GetOrdersByUserName(userName);
            return Ok(orders);
        }

       
        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] Order order)
        {
          await _repository.UpdateAsync(order);
            return NoContent();
        }

        [HttpPost(Name = "AddOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Order>> AddOrder([FromBody] Order order)
        {
            order.Id = Guid.NewGuid();
            await _repository.AddAsync(order);
            return Created("",order);
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(string userName)
        {
            var orderToDelete = await _repository.GetOrdersByUserName(userName);
            if (orderToDelete == null)
            {
                // throw ex
                // throw new NotFoundException(nameof(Order), request.Id);
            }
            await _repository.DeleteAsync(userName);
            return NoContent();
        }
    }
}