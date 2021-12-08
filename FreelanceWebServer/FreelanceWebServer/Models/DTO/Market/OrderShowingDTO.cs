using System;
using System.Collections.Generic;

namespace FreelanceWebServer.Models.DTO.Market
{
    public class OrderShowingDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long? ContractorId { get; set; }

        public string ContractorFullName { get; set; }

        public long CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public List<object> Info { get; set; }

        public decimal Cost { get; set; }

        public DateTime Deadline { get; set; }

        public long? SpecialId { get; set; }
    }
}
