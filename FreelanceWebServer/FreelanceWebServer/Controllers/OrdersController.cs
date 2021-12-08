using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models;
using System.Threading.Tasks;
using FreelanceWebServer.Models.DTO.Market;

namespace FreelanceWebServer.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IMapper _mapper;
        IOrderRepository _orderRepository;
        IUserRepository _userRepository;

        public OrdersController(IMapper mapper, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderShowingDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var orderShowingDTOs = new List<OrderShowingDTO>();

            var orders = await _orderRepository.GetAll();

            foreach (var order in orders)
            {
                /*var ContractorFullName = _userRepository.GetById(order.CustomerId);
                var CustomerFullName = _userRepository.GetById(order.CustomerId);*/


                var orderShowingDTO = new OrderShowingDTO
                {
                    Id = order.Id,
                    Title = order.Title,
                    Description = order.Description,
                    ContractorId = order.ContractorId,
                    //ContractorFullName = $"{employee.Surname} {employee.Name}"
                    CustomerId = order.CustomerId,
                    //CustomerFullName = $"{customer.Surname} {customer.Name}",
                    Info = new List<object>(),
                    Cost = order.Cost,
                    Deadline = order.Deadline,
                    SpecialId = order.SpecialId
                };

                orderShowingDTOs.Add(orderShowingDTO);
            }

            return Ok(orderShowingDTOs);
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Order id</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderShowingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(long id) 
        {
            Order order = await _orderRepository.Get(id);

            if (order == null)
                return BadRequest("Order with this id not found in database");

            /*var ContractorFullName = _userRepository.GetById(order.CustomerId);
            var CustomerFullName = _userRepository.GetById(order.CustomerId);*/

            var orderShowingDTO = new OrderShowingDTO
            {
                Id = order.Id,
                Title = order.Title,
                Description = order.Description,
                ContractorId = order.ContractorId,
                //ContractorFullName = $"{employee.Surname} {employee.Name}"
                CustomerId = order.CustomerId,
                //CustomerFullName = $"{customer.Surname} {customer.Name}",
                Info = new List<object>(),
                Cost = order.Cost,
                Deadline = order.Deadline,
                SpecialId = order.SpecialId
            };

            return Ok(orderShowingDTO);
        }

        /// <summary>
        /// Add order
        /// </summary>
        /// <param name="model">CreateOrder DTO</param>
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreatingDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest($"Errors count when validating fileds {ModelState.ErrorCount}");

            var order = new Order
            {
                Title = model.Title,
                Description = model.Description,
                CustomerId = 0,
                InfoId = 0,
                Cost = model.Cost,
                Deadline = model.Deadline,
                SpecialId = model.SpecialId,
            };

            await _orderRepository.Add(order);

            return Ok();
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="model">UpdateOrder DTO</param>
        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] OrderUpdatingDTO model)
        {
            var order = new Order
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                ContractorId = model.ContractorId,
                Cost = model.Cost,
                Deadline = model.Deadline,
                SpecialId = model.SpecialId,
            };

            await _orderRepository.Update(order);

            return Ok();
        }
        
        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="id">Order id</param>
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _orderRepository.Delete(id);

            return Ok();
        }
    }
}
