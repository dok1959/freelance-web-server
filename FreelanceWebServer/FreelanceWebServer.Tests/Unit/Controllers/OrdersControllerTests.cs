using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using NUnit.Framework;
using FreelanceWebServer.Controllers;
using FreelanceWebServer.Models.DTO.Market;
using FreelanceWebServer.Repositories;


namespace FreelanceWebServer.Tests.Unit.Controllers
{
    
    public class OrdersControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;


        public OrdersControllerTests() 
        {
            var provider = GetServiceProvider();
            var scope = provider.CreateScope();

            _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            _orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        }

        //[Test]
        public async Task GetAll_ShouldReturnAllOrders()
        {
            var controller = new OrdersController(_mapper, _orderRepository, _userRepository);
            var expectedResult = new OrderShowingDTO();

            var actualResult = await controller.Get();

            Assert.AreEqual(expectedResult, actualResult);
        }

        //[Test]
        public void GetById_ShouldReturnOneOrder_WhenGivenId()
        {

        }

        //[Test]
        public void Post_ShouldAddOrder_WhenGivenOrderCreatingDTO()
        {

        }

        //[Test]
        public void Update_ShouldUpdateOrder_WhenGivenOrderUpdatingDTO()
        {

        }

        //[Test]
        public void Delete_ShouldDeleteOrder_WhenGivenOrderId()
        {

        }

        private ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IOrderRepository, MemoryOrderRepository>();
            services.AddScoped<IUserRepository, MemoryUserRepository>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services.BuildServiceProvider();
        }
    }
}
