using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Models;
using FreelanceWebServer.Models.DTO.Market.Order;
using System.Threading.Tasks;

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
        [ProducesResponseType(typeof(IEnumerable<ShowOrderDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var showOrdersDTO = new List<ShowOrderDTO>();

            var orders = await _orderRepository.GetAll();

            foreach (var order in orders)
            {
                /*var customer = _userRepository.GetById(order.CustomerId);
                var employee = _userRepository.GetById(order.CustomerId);*/

                var showOrderDTO = new ShowOrderDTO
                {
                    Id = order.Id,
                    Title = order.Title,
                    CustomerId = order.CustomerId,
                    //Customer = $"{customer.Surname} {customer.Name}",
                    EmployeeId = order.EmployeeId,
                    //Employee = $"{employee.Surname} {employee.Name}"
                };

                showOrdersDTO.Add(showOrderDTO);
            }

            return Ok(showOrdersDTO);
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Order id</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ShowOrderDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(long id) 
        {
            Order order = await _orderRepository.Get(id);

            if (order == null)
                return BadRequest("Order with this id not found in database");

            /*var customer = _userRepository.GetById(order.CustomerId);
            var employee = _userRepository.GetById(order.CustomerId);*/

            var showOrderDTO = new ShowOrderDTO
            {
                Id = order.Id,
                Title = order.Title,
                CustomerId = order.CustomerId,
                //Customer = $"{customer.Surname} {customer.Name}",
                EmployeeId = order.EmployeeId,
                //Employee = $"{employee.Surname} {employee.Name}"
            };

            return Ok(showOrderDTO);
        }

        /// <summary>
        /// Add order
        /// </summary>
        /// <param name="model">CreateOrder DTO</param>
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderDTO model)
        {
            var order = new Order
            {
                Title = model.Title,
                CustomerId = model.CustomerId,
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
        public async Task<IActionResult> Put([FromBody] UpdateOrderDTO model)
        {
            var order = new Order
            {
                Id = model.Id,
                Title = model.Title,
                EmployeeId = model.EmployeeId
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
