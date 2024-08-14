using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    [Table("tbl_category_book")]
    public class BookCategoryEntity
    {
        [Column("book_id"), ForeignKey(nameof(BookEntity.Id))]
        public long BookId { get; set; }

        [Column("category_id"), ForeignKey(nameof(CategoryEntity.Id))]
        public long CategoryId { get; set; }

        public BookEntity Book { get; set; }   

        public CategoryEntity Category { get; set; }

    }
}
