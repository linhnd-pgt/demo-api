using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Service.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        public string Email { get; set; }

    }
}
