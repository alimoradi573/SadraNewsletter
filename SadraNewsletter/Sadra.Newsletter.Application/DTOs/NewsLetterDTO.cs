using Sadra.Newsletter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadra.Newsletter.Application.DTOs
{
    public class NewsLetterDTO
    {
        public int Id { get; set; }  
        public string Title { get; set; } 
        public string Content { get; set; }  
        public DateTime CreatedDate { get; set; } 
        public DateTime SentDate { get; set; }  

      
        public ICollection<RecipientDTO> Recipients { get; set; } = new List<RecipientDTO>();

    }
}
