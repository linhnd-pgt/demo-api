using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class BookRequestDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public DateTime PublishDate { get; set; }

        public long AuthorId { get; set; }

        public List<long> CategoryIdList { get; set; }
    }
}
