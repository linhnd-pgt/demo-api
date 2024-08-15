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
        [JsonIgnore]
        public long id { get; set; }
        public string username { get; set; } = String.Empty;

        public string password { get; set; } = String.Empty;

        public string email { get; set; }

    }
}
