﻿namespace FreelanceWebServer.Models
{
    public class Order : BaseModel
    {
        public string Title { get; set; }

        public long CustomerId { get; set; }

        public long EmployeeId { get; set; }
    }
}
