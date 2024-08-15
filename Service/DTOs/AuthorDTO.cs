using Service.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class AuthorDTO
    {
        [JsonIgnore]
        public long id { get; set; }
         
        public string name { get; set; } = String.Empty;

        public string biography { get; set; } = String.Empty;

        public DateTime dateOfBirth { get; set; }

    }
}
