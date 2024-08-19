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

        BookEntity BookDtoToBookEntity(BookDTO bookDto);

        BookEntity BookRequestDtoToBookEntity(BookRequestDTO bookRequestDto);
    }

    public class BookMapper : IBookMapper
    {

        public BookDTO BookEntityToBookDto(BookEntity book) => new BookDTO
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
            }
        };

        public BookEntity BookDtoToBookEntity(BookDTO bookDto) =>  new BookEntity
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            Image = bookDto.Image,
            PublishedDate = bookDto.PublishDate
        };

        public BookEntity BookRequestDtoToBookEntity(BookRequestDTO bookRequestDto) => new BookEntity
        {
            Id = bookRequestDto.Id,
            Title = bookRequestDto.Title,
            Image = bookRequestDto.Image,
            PublishedDate = bookRequestDto.PublishDate,
            AuthorId = bookRequestDto.AuthorId,
        };
    }
}
