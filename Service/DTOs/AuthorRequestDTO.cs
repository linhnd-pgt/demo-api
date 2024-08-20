using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class AuthorRequestDTO
    {

        public string Name { get; set; } = String.Empty;

        public string Biography { get; set; } = String.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
