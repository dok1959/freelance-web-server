namespace FreelanceWebServer.Models.DTO
{
    public class OrderDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public OrderDTO(Order order)
        {
            Id = order.Id;
            Title = order.Title;
            Author = order.Author;
        }
    }
}