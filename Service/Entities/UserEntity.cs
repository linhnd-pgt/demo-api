using Microsoft.EntityFrameworkCore;
using Service.Entities;
using Service.Entities.@base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{

/*    public enum Role
    {
        Admin, Librarian, Member
    }*/

    // set username and email column to unique
    [Table("tbluser")]
    [Index(nameof(UserEntity.UserName), IsUnique = true)]
    [Index(nameof(UserEntity.Email), IsUnique = true)]
    public class UserEntity : AbstractBaseEntity
    {

        [Column("username"), Required]
        public string UserName { get; set; } = String.Empty;

        [Column("password_hash"), Required]
        public string PasswordHash { get; set; } = String.Empty;

        [Column("email"), EmailAddress, DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }

        // role only receives Admin as 0, Librarian as 1, Member as 2
        [Column("role")]
        public string Role { get; set; }

        public UserEntity() { }

        public UserEntity(long id, string userName, string passwordHash, string email)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
        }
    }
}
