using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models;
using System.Threading.Tasks;
using FreelanceWebServer.DTO.Market;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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

        #region CRUD operations
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
                var customer = await _userRepository.Get(order.CustomerId);

                var orderShowingDTO = new OrderShowingDTO
                {
                    Id = order.Id,
                    Title = order.Title,
                    Description = order.Description,
                    ContractorId = order.ContractorId,
                    CustomerId = order.CustomerId,
                    CustomerFullName = $"{customer.Surname} {customer.Name}",
                    Info = new List<object>(),
                    Cost = order.Cost,
                    Deadline = order.Deadline,
                    SpecialId = order.SpecialId
                };

                if (order.ContractorId.HasValue)
                {
                    var contractor = await _userRepository.Get(order.ContractorId.Value);
                    orderShowingDTO.ContractorFullName = $"{contractor.Surname} {contractor.Name}";
                }
                    

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

            var customer = await _userRepository.Get(order.CustomerId);

            var orderShowingDTO = new OrderShowingDTO
            {
                Id = order.Id,
                Title = order.Title,
                Description = order.Description,
                ContractorId = order.ContractorId,
                CustomerId = order.CustomerId,
                CustomerFullName = $"{customer.Surname} {customer.Name}",
                Info = new List<object>(),
                Cost = order.Cost,
                Deadline = order.Deadline,
                SpecialId = order.SpecialId
            };

            if (order.ContractorId.HasValue)
            {
                var contractor = await _userRepository.Get(order.ContractorId.Value);
                orderShowingDTO.ContractorFullName = $"{contractor.Surname} {contractor.Name}";
            }

            return Ok(orderShowingDTO);
        }

        /// <summary>
        /// Add order
        /// </summary>
        /// <param name="model">CreateOrder DTO</param>
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Post([FromBody] OrderCreatingDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest($"Errors count when validating fileds {ModelState.ErrorCount}");

            var userId = GetUserIdFromToken();

            var order = new Order
            {
                Title = model.Title,
                Description = model.Description,
                CustomerId = userId,
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
        [HttpPut]
        [Authorize(Roles = "user")]
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
        [HttpDelete("{id}")]
        [Authorize(Roles = "user, moderator")]
        public async Task<IActionResult> Delete(long id)
        {
            await _orderRepository.Delete(id);
            return Ok();
        }
        #endregion

        /// <summary>
        /// Respond to order
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        [HttpPost("{id}/respond")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Respond(long id)
        {
            var userId = GetUserIdFromToken();

            var order = await _orderRepository.Get(id);

            if (order.CustomerId == userId)
                return BadRequest("Cannot respond to your order");

            order.ContractorId = userId;

            await _orderRepository.Update(order);

            return Ok();
        }

        /// <summary>
        /// Refuse order response
        /// </summary>
        /// <param name="id">order id</param>
        /// <returns></returns>
        [HttpPost("{id}/refuse")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Refuse(long id)
        {
            var userId = GetUserIdFromToken();

            var order = await _orderRepository.Get(id);

            if (order.CustomerId == userId)
                return BadRequest("Cannot refuse to your order");

            if (order.ContractorId != userId)
                return BadRequest("Cannot reject someone else's order");

            order.ContractorId = null;

            await _orderRepository.Update(order);

            return Ok();
        }
    
        private long GetUserIdFromToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last();
            var claims = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToArray();
            return long.Parse(claims.First().Value);
        }
    }
}
