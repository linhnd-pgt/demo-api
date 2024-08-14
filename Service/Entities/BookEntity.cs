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
    [Table("tblbook")]
    [Index(nameof(BookEntity.Title), IsUnique = true)]
    public class BookEntity : AbstractBaseEntity
    {

        [Column("title"), Required, MinLength(2)]
        public string Title { get; set; } = String.Empty;

        [Column("image"), MinLength(2)]
        public string Image {  get; set; } = String.Empty;

        [Column("published_date")]
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        [Column("author_id"), ForeignKey(nameof(AuthorEntity.Id))]
        public long AuthorId { get; set; }

        public AuthorEntity Author { get; set; }

        public Collection<BookCategoryEntity> BookCategoryList { get; set; }

    }
}
