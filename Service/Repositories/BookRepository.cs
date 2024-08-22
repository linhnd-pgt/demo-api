using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Entities;
using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{

    public interface IBookRepository : IRepositoryBase<BookEntity>
    {
        Task<List<BookEntity>> GetBookList();

        Task<List<BookEntity>> GetBookListPaginated(int page, int pageSize);

        Task<BookEntity> GetBookById(long id);

        void AddBook(BookEntity book);

        void UpdateBook(BookEntity book);

        void DeleteBook(BookEntity book);

        Task<List<BookDTO>> FilterBook(string keyword);


    }

    public class BookRepository : RepositoryBase<BookEntity>, IBookRepository
    {

        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<List<BookEntity>> GetBookList() => await _dbContext.Book.AsNoTracking().Include(b => b.Author).ToListAsync();

        public async Task<List<BookEntity>> GetBookListPaginated(int page, int pageSize) => await _dbContext.Book.AsNoTracking().Include(b => b.Author).Skip(page).Take(pageSize).ToListAsync();

        public void AddBook(BookEntity book) => _dbContext.Book.Add(book);

        public void UpdateBook(BookEntity book) => _dbContext.Book.Update(book);

        public void DeleteBook(BookEntity book) => _dbContext.Book.Remove(book);

        public async Task<BookEntity> GetBookById(long id) => await _dbContext.Book.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<BookDTO>> FilterBook(string keyword)
        {
            var booksWithCategories = await _dbContext.Book
            .Include(b => b.Author)
            .Include(b => b.BookCategoryList)
            .ThenInclude(bc => bc.Category)
            .Where(b => b.Title.Contains(keyword) || b.Author.Name.Contains(keyword) ||
                    b.BookCategoryList.Any(bc => bc.Category.Name.Contains(keyword)))
            .Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                PublishDate = b.PublishedDate,
                Author = new AuthorDTO
                {
                    Id = b.Author.Id,
                    Name = b.Author.Name,
                    Biography = b.Author.Biography,
                    DateOfBirth = b.Author.DateOfBirth,
                },
                CategoryDTOs = b.BookCategoryList
                              .Select(bc => new CategoryDTO
                              {
                                  Id = bc.Category.Id,
                                  Name = bc.Category.Name
                              })
                              .ToList()
            })
            .ToListAsync();

            return booksWithCategories;
        }
    }
}
