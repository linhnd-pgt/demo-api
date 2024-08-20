using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class BookRequestDTO
    {
        public string Title { get; set; }

        public IFormFile Image { get; set; }

        public DateTime PublishDate { get; set; }

        public long AuthorId { get; set; }

        public List<long> CategoryIdList { get; set; }
    }
}
