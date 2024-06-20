using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ContactRequest
    {
        [Key]
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMessage { get; set; }
        public DateTime DateOfSubmission { get; set; }
    }
}
