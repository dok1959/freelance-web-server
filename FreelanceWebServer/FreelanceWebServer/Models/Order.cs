namespace FreelanceWebServer.Models
{
    public class Order
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public long CustomerId { get; set; }

        public long? EmployeeId { get; set; }
    }
}
