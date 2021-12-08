using System;
using System.ComponentModel.DataAnnotations;

namespace FreelanceWebServer.Models.DTO.Market
{
    public class OrderCreatingDTO
    {
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public long? SpecialId { get; set; }
    }
}
