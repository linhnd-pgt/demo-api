using Service.Entities.@base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.Entities
{
    [Table("tblauthor")]
    public class AuthorEntity : AbstractBaseEntity
    {
        [Column("name"), Required, MinLength(2)]
        public string Name { get; set; } = String.Empty;

        [Column("biography"), Required, MinLength(2), DataType(DataType.Text)]
        public string Biography { get; set; } = String.Empty;

        [Column("dob")]
        public DateTime DateOfBirth { get; set;}

        [JsonIgnore]
        public Collection<BookEntity> Books { get; set; }

        public AuthorEntity() { }

        public AuthorEntity(long id, string name, string biography, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            Biography = biography;
            DateOfBirth = dateOfBirth;
        }
    }
}
