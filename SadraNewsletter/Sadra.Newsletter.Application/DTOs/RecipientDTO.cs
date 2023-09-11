using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadra.Newsletter.Application.DTOs
{
    public class RecipientDTO
    {
        public int Id { get; set; }
        public int NewsLetterId { get; set; }
        public string Email { get; set; }
        public DateTime? LastViewed { get; }
        public DateTime? GiveDate { get; set; }
        public DateTime? RecivedDate { get; init; }
    }

    public class SendToRecipientDto
    {
        public int Id { get; set; }
        public int NewsLetterId { get; set; }
        public string Email { get; set; }
    }
}
