using Service.DTOs;
using Service.Entities;
using Service.Helpers;
using Service.Repositories.Base;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface ICategoryService
    {

        Task<List<CategoryDTO>> GetAllCategory();

        Task<List<CategoryDTO>> GetAllCategoryPaginated(int page, int pageSize);

        Task<bool> AddCategory(CategoryRequestDTO categoryRequestDTO, string username);

        Task<bool> UpdateCategory(CategoryRequestDTO categoryRequestDTO, long categoryId, string username);

        Task<bool> DeleteCategory(long bookId);

    }

    public class CategoryService : ServiceBase, ICategoryService
    {
        private readonly ICategoryMapper _categoryMapper = new CategoryMapper();

        public CategoryService(IRepositoryManager repositoryManager) : base(repositoryManager) { }

        public async Task<bool> AddCategory(CategoryRequestDTO categoryRequestDTO, string username)
        {
            try
            {
                CategoryEntity categoryEntity = _categoryMapper.CategoryRequestDtoToCategoryEntity(categoryRequestDTO);

                List<BookCategoryEntity> bookCategoryEntities = new List<BookCategoryEntity>();

                foreach (var bookId in categoryRequestDTO.BookIdList)
                {
                    if (_repositoryManager.categoryRepository.GetById(bookId) == null)
                    {
                        return false;
                    }
                    bookCategoryEntities.Add(new BookCategoryEntity
                    {
                        CategoryId = categoryEntity.Id,
                        BookId = bookId
                    });
                }

                categoryEntity.CategoryBookList = new Collection<BookCategoryEntity>();

                _repositoryManager.bookCategoryRepository.AddRangeBookCategory(bookCategoryEntities);

                foreach (var bookCategory in bookCategoryEntities)
                {
                    categoryEntity.CategoryBookList.Add(bookCategory);
                }

                categoryEntity.CreatedBy = username;
                categoryEntity.UpdatedBy = username;

                _repositoryManager.categoryRepository.AddCategory(categoryEntity);

                await _repositoryManager.SaveAsync();

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(long categoryID)
        {
            CategoryEntity existedCategory = await _repositoryManager.categoryRepository.GetById(categoryID);
            if(existedCategory != null)
            {
                _repositoryManager.categoryRepository.Delete(existedCategory);
                await _repositoryManager.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var categoryEntityList = await _repositoryManager.categoryRepository.GetAll();
            return categoryEntityList.ToList().Select(e => _categoryMapper.CategoryEntityToCategoryDto(e)).ToList();
        }

        public async Task<List<CategoryDTO>> GetAllCategoryPaginated(int page, int pageSize)
        {
            var categoryEntityList = await _repositoryManager.categoryRepository.GetAll();
            return categoryEntityList.ToList().Skip(page).Take(pageSize).Select(e => _categoryMapper.CategoryEntityToCategoryDto(e)).ToList();
        }

        public async Task<bool> UpdateCategory(CategoryRequestDTO categoryRequestDTO, long categoryId, string username)
        {
            CategoryEntity existedCategory = await _repositoryManager.categoryRepository.GetById(categoryId);
            if (existedCategory != null)
            {
                List<BookCategoryEntity> bookCategoryEntities = new List<BookCategoryEntity>();

                foreach (var bookId in categoryRequestDTO.BookIdList)
                {
                    if (_repositoryManager.categoryRepository.GetById(bookId) == null)
                    {
                        return false;
                    }
                    bookCategoryEntities.Add(new BookCategoryEntity
                    {
                        CategoryId = existedCategory.Id,
                        BookId = bookId
                    });
                }

                List<BookCategoryEntity> bookCategories = await _repositoryManager.bookCategoryRepository.GetByBookId(categoryId);

                _repositoryManager.bookCategoryRepository.DeleteBookCategoryById(bookCategories);

                await _repositoryManager.SaveAsync();

                _repositoryManager.bookCategoryRepository.AddRangeBookCategory(bookCategoryEntities);

                existedCategory.CategoryBookList = new Collection<BookCategoryEntity>();

                foreach (var bookCategory in bookCategoryEntities)
                {
                    existedCategory.CategoryBookList.Add(bookCategory);
                }

                existedCategory.UpdatedBy = username;

                _repositoryManager.categoryRepository.UpdateCategory(existedCategory);

                await _repositoryManager.SaveAsync();

                return true;
            }
            return false;
        }
    }
}
