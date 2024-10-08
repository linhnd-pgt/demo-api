﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class BookDTO
    {
        public long Id {  get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public DateTime PublishDate { get; set; }

        public AuthorDTO Author { get; set; }

        public List<CategoryDTO> CategoryDTOs { get; set; }

    }
}
