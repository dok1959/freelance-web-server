using System;
using System.ComponentModel.DataAnnotations;

namespace FreelanceWebServer.Models.DTO.Market
{
    public class OrderUpdatingDTO
    {
        [Required]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public long? ContractorId { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public long? SpecialId { get; set; }
    }
}
