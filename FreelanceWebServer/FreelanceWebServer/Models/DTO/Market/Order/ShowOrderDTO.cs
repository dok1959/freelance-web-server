namespace FreelanceWebServer.Models.DTO.Market.Order
{
    public class ShowOrderDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public long CustomerId { get; set; }

        public string Customer { get; set; }

        public long? EmployeeId { get; set; }

        public string Employee { get; set; }
    }
}
