using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadra.Newsletter.Domain.Entities
{
    public class Recipient
    {
        public int Id { get; set; }  
        public string Name { get; set; } 
        public string Email { get; set; }  
        public DateTime LastViewed { get; set; }  
    }
}
