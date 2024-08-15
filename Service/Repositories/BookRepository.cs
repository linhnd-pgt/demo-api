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

    public interface IBookRepostiroy
    {
        Task<List<BookEntity>> GetBookList();

        Task<List<BookEntity>> GetBookListPaginated(int page, int pageSize);

        void AddBook(BookEntity book);

        void UpdateBook(BookEntity book);

        void DeleteBook(BookEntity book);

        void UpdateBookToCategory(BookEntity book);
    }
    public class BookRepository : RepositoryBase<BookEntity>, IBookRepostiroy
    {

        private readonly RepositoryContext _repositoryContext;

        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<List<BookEntity>> GetBookList() => await _repositoryContext.Book.ToListAsync();

        public async Task<List<BookEntity>> GetBookListPaginated(int page, int pageSize) => await _repositoryContext.Book.Skip(page).Take(pageSize).ToListAsync();

        public async void AddBook(BookEntity book) => _repositoryContext.Book.Add(book);

        public void UpdateBook(BookEntity book) => _repositoryContext.Book.Update(book);

        public void DeleteBook(BookEntity book) => _repositoryContext.Book.Remove(book);

    }
}
