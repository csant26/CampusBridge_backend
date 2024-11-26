using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models.DTO.Content.FAQ
{
    public class FAQRequestDTO
    {
        public string? Question { get; set; }
        public string? Category { get; set; }
    }
}
