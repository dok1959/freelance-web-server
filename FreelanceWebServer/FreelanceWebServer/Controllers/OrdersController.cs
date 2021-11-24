using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using FreelanceWebServer.Models.DTO;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models;

namespace FreelanceWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IMapper _mapper;
        IOrderRepository _orderRepository;

        public OrdersController(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var orders = _orderRepository.GetAll();
            var ordersDTO = _mapper.Map<IEnumerable<OrderDTO>>(orders);

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

            var orderDTO = _mapper.Map<OrderDTO>(order);

            return Ok(orderDTO);
        }

        /// <summary>
        /// Add order
        /// </summary>
        /// <param name="model">Order DTO</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            _orderRepository.Add(order);

            return Ok();
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="model">Order DTO</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            _orderRepository.Update(order);

            return Ok();
        }
        
        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _orderRepository.DeleteById(id);

            return Ok();
        }
    }
}
