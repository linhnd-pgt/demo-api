using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Entities;
using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{

    public interface ICategoryRepository : IRepositoryBase<CategoryEntity>
    {
        Task<List<CategoryEntity>> GetAll();

        Task<List<CategoryEntity>> GetAllPaginated(int page, int pageSize);

        Task<CategoryEntity> GetById(long id);

        Task<List<CategoryDTO>> GetByBookId(long bookId);

        void AddCategory(CategoryEntity categoryEntity);

        void UpdateCategory(CategoryEntity categoryEntity);

        void DeleteCategory(CategoryEntity categoryEntity);

    }

    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {

        }

        public void AddCategory(CategoryEntity categoryEntity)
        {
            _dbContext.Category.Add(categoryEntity);
        }

        public void UpdateCategory(CategoryEntity categoryEntity)
        {
            _dbContext.Category.Update(categoryEntity);
        }

        public void DeleteCategory(CategoryEntity categoryEntity)
        {
            _dbContext.Category.Remove(categoryEntity);
        }

        public async Task<List<CategoryEntity>> GetAll() => await _dbContext.Category.ToListAsync();

        public async Task<List<CategoryEntity>> GetAllPaginated(int page, int pageSize) => await _dbContext.Category.Skip(page).Take(pageSize).ToListAsync(); 

        public async Task<CategoryEntity> GetById(long id) => await _dbContext.Category.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<CategoryDTO>> GetByBookId(long bookId)
        {
            var categoryDtoList = from book in _dbContext.Book
                           join bookCate in _dbContext.BookCategories
                           on book.Id equals bookCate.BookId
                           join cate in _dbContext.Category
                           on bookCate.CategoryId equals cate.Id
                           where book.Id == bookId
                           select new CategoryDTO
                           {
                               Id = cate.Id,
                               Name = cate.Name,
                           };

            return await categoryDtoList.ToListAsync();

        }
    }
}
