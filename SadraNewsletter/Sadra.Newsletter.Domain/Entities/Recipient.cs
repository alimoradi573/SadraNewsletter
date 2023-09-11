using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadra.Newsletter.Domain.Entities
{
    public class Recipient
    {
        public Recipient()
        {
            RecivedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public int NewsLetterId{get;set;}
        public string Email { get; set; }  
        public DateTime? LastViewed { get; set; }
        public DateTime? GiveDate { get; set; }
        public DateTime? RecivedDate { get; init; }
    }
}
