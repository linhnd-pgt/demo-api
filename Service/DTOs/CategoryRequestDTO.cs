using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class CategoryRequestDTO
    {
        public string Name { get; set; }

        public List<long> BookIdList { get; set; }

    }
}
