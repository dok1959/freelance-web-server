using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using FreelanceWebServer.Models.DTO;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(OrderDTO[]), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var ordersDTO = new List<OrderDTO>();

            foreach(var order in _orderRepository.GetAll())
            {
                ordersDTO.Add(new OrderDTO(order));
            }

            return Ok(ordersDTO); 
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        public IActionResult Get(long id) 
        {
            Order order = _orderRepository.GetById(id);

            if (order == null)
                return BadRequest("Order with this id not found in database");

            OrderDTO orderDTO = new OrderDTO(order);

            return Ok(orderDTO);
        }

        /// <summary>
        /// Add order
        /// </summary>
        /// <param name="model">Order DTO</param>
        /// <returns></returns>
        [Authorize(Roles = "admin, moderator, customer")]
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO model)
        {
            Order order = new Order
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author
            };

            _orderRepository.Add(order);

            return Ok();
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="model">Order DTO</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] OrderDTO model)
        {
            Order order = new Order
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author
            };

            _orderRepository.Update(order);

            return Ok();
        }
        
        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _orderRepository.DeleteById(id);

            return Ok();
        }
    }
}
