namespace FreelanceWebServer.Models.DTO.Market.Order
{
    public class UpdateOrderDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public long? EmployeeId { get; set; }
    }
}
