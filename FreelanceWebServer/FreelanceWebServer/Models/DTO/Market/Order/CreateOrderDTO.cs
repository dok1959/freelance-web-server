namespace FreelanceWebServer.Models.DTO.Market.Order
{
    public class CreateOrderDTO
    {
        public string Title { get; set; }

        public long CustomerId { get; set; }

        public long EmployeeId { get; set; }
    }
}
