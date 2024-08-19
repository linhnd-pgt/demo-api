using Microsoft.EntityFrameworkCore;
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
    public interface IBookCategoryRepository
    {

        Task<List<BookCategoryEntity>> GetByBookId(long bookId);

        Task<List<BookCategoryEntity>> GetByCategoryId(long categoryId);

        void CreateBookCategory(BookCategoryEntity entity);

        void AddRangeBookCategory(List<BookCategoryEntity> bookCategoryEntities);

        void UpdateBookCategory(BookCategoryEntity entity);

        void DeleteBookCategory(BookCategoryEntity entity);

        void DeleteBookCategoryByBookId(List<BookCategoryEntity> bookCategoryEntities);
    }

    public class BookCategoryRepository : RepositoryBase<BookRepository>, IBookCategoryRepository
    {
        public BookCategoryRepository(RepositoryContext context) : base(context)
        {

        }

        public async Task<List<BookCategoryEntity>> GetByBookId(long bookId) => await _dbContext.BookCategories.AsNoTracking().Where(e => e.BookId == bookId).ToListAsync();    

        public async Task<List<BookCategoryEntity>> GetByCategoryId(long categoryId) => await _dbContext.BookCategories.AsNoTracking().Where(e => e.CategoryId == categoryId).ToListAsync();


        public void CreateBookCategory(BookCategoryEntity entity) => _dbContext.Add(entity);

        public void DeleteBookCategory(BookCategoryEntity entity) => _dbContext.Update(entity);

        public void UpdateBookCategory(BookCategoryEntity entity) => _dbContext.Remove(entity);

        public void DeleteBookCategoryByBookId(List<BookCategoryEntity> bookCategoryEntities) => _dbContext.RemoveRange(bookCategoryEntities);

        public void AddRangeBookCategory(List<BookCategoryEntity> bookCategoryEntities) => _dbContext.AddRange(bookCategoryEntities);
    }
}
