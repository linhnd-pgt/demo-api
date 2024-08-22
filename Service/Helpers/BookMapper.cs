using Service.DTOs;
using Service.Entities;
using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public interface IBookMapper
    {
        BookDTO BookEntityToBookDto(BookEntity book);

        BookEntity BookDtoToBookEntity(BookDTO source, BookEntity target);

        BookEntity BookRequestDtoToBookEntity(BookRequestDTO source, BookEntity target);
    }

    public class BookMapper : IBookMapper
    {

        public BookDTO BookEntityToBookDto(BookEntity book)
        {
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Image = book.Image,
                PublishDate = book.PublishedDate,
                Author = new AuthorDTO
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    Biography = book.Author.Biography,
                    DateOfBirth = book.Author.DateOfBirth
                },
            };
        }

        public BookEntity BookDtoToBookEntity(BookDTO source, BookEntity target) 
        {
            target.Title = source.Title;
            target.Image = source.Image;
            target.PublishedDate = source.PublishDate;
            return target;
        }

        public BookEntity BookRequestDtoToBookEntity(BookRequestDTO source, BookEntity target)
        {
            target.Title = source.Title;
            target.PublishedDate = source.PublishDate;
            target.AuthorId = source.AuthorId;
            return target;
        }
    }
}
