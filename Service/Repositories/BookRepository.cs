using Microsoft.EntityFrameworkCore;
using Service.Entities;
using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        
    }

    public class BookRepository : RepositoryBase<BookEntity>, IBookRepository
    {

        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<List<BookEntity>> GetBookList() => await _dbContext.Book.Include(b => b.Author).ToListAsync();

        public async Task<List<BookEntity>> GetBookListPaginated(int page, int pageSize) => await _dbContext.Book.Include(b => b.Author).Skip(page).Take(pageSize).ToListAsync();

        public void AddBook(BookEntity book) => _dbContext.Book.Add(book);

        public void UpdateBook(BookEntity book) => _dbContext.Book.Update(book);

        public void DeleteBook(BookEntity book) => _dbContext.Book.Remove(book);

        public async Task<BookEntity> GetBookById(long id) => await _dbContext.Book.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);   


    }
}
