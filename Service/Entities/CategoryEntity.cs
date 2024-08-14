using Microsoft.EntityFrameworkCore;
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
    [Table("tblcategory")]
    [Index(nameof(CategoryEntity.Name), IsUnique = true)]
    public class CategoryEntity : AbstractBaseEntity
    {

        [Column("name"), Required, MinLength(2)]
        public string Name { get; set; } = String.Empty;

        public Collection<BookCategoryEntity> CategoryBookList { get; set; }

    }
}
