using Service.DTOs;
using Service.Entities;
using Service.Helpers;
using Service.Repositories.Base;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{

    public interface IBookService
    {
        Task<List<BookDTO>> GetAllBook();

        Task<List<BookDTO>> GetAllBookPaginated(int page, int pageSize);

        Task<bool> AddBook(BookRequestDTO bookDTO, string username);

        Task<bool> UpdateBook(BookRequestDTO bookDTO, long bookId, string username);

        Task<bool> DeleteBook(long bookId);
    }

    public class BookService : ServiceBase, IBookService
    {

        private readonly IBookMapper _bookMapper = new BookMapper();

        private readonly ICloudinaryService _cloudinaryService;

        public BookService(IRepositoryManager repositoryManager, ICloudinaryService cloudinaryService) : base(repositoryManager)
        {
            _bookMapper = new BookMapper();
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<BookDTO>> GetAllBook()
        {
            var bookEntityList = await _repositoryManager.bookRepository.GetBookList();
            return bookEntityList.ToList().Select(e => _bookMapper.BookEntityToBookDto(e)).ToList();

        }

        public async Task<List<BookDTO>> GetAllBookPaginated(int page, int pageSize) 
        {
            var bookEntityList = await _repositoryManager.bookRepository.GetBookList();
            return bookEntityList.ToList().Skip(page).Take(pageSize).Select(e => _bookMapper.BookEntityToBookDto(e)).ToList();
        }

        public async Task<bool> AddBook(BookRequestDTO bookDTO, string username)
        {
            try
            {
                BookEntity book = _bookMapper.BookRequestDtoToBookEntity(bookDTO);

                List<BookCategoryEntity> bookCategoryEntities = new List<BookCategoryEntity>();

                foreach(var categoryId in bookDTO.CategoryIdList)
                {
                    if (_repositoryManager.categoryRepository.GetById(categoryId) == null)
                    {
                        return false;
                    }
                    bookCategoryEntities.Add(new BookCategoryEntity
                    {
                        BookId = book.Id,
                        CategoryId = categoryId
                    });
                }

                book.BookCategoryList = new Collection<BookCategoryEntity>();

                _repositoryManager.bookCategoryRepository.AddRangeBookCategory(bookCategoryEntities);

                foreach (var bookCategory in bookCategoryEntities)
                {
                    book.BookCategoryList.Add(bookCategory);
                }

                // uploading book's cover image
                var uploadResult = await _cloudinaryService.UploadImageAsync(bookDTO.Image);
                if (uploadResult.Error != null)
                {
                    return false;
                }

                book.Image = uploadResult.SecureUrl.ToString();

                book.CreatedBy = username;
                book.UpdatedBy = username;

                _repositoryManager.bookRepository.AddBook(book);

                await _repositoryManager.SaveAsync();

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> UpdateBook(BookRequestDTO bookDTO, long bookId, string username)
        {
            BookEntity bookEntity = await _repositoryManager.bookRepository.GetBookById(bookId);
            if (bookEntity != null)
            {
                try
                {
                    BookEntity book = _bookMapper.BookRequestDtoToBookEntity(bookDTO);

                    List<BookCategoryEntity> bookCategoryEntities = new List<BookCategoryEntity>();

                    foreach (var categoryId in bookDTO.CategoryIdList)
                    {
                        if (await _repositoryManager.categoryRepository.GetById(categoryId) == null)
                        {
                            return false;
                        }
                        bookCategoryEntities.Add(new BookCategoryEntity
                        {
                            BookId = book.Id,
                            CategoryId = categoryId
                        });
                    }

                    List<BookCategoryEntity> bookCategories = await _repositoryManager.bookCategoryRepository.GetByBookId(bookId);

                    _repositoryManager.bookCategoryRepository.DeleteBookCategoryById(bookCategories);

                    await _repositoryManager.SaveAsync();

                    _repositoryManager.bookCategoryRepository.AddRangeBookCategory(bookCategoryEntities);

                    book.BookCategoryList = new Collection<BookCategoryEntity>();


                    foreach (var bookCategory in bookCategoryEntities)
                    {
                        book.BookCategoryList.Add(bookCategory);
                    }

                    // uploading book's cover image
                    var uploadResult = await _cloudinaryService.UploadImageAsync(bookDTO.Image);

                    if (uploadResult.Error != null)
                    {
                        return false;
                    }

                    book.Image = uploadResult.SecureUrl.ToString();

                    book.UpdatedBy = username;

                    // saving book to db
                    _repositoryManager.bookRepository.Update(book);

                    await _repositoryManager.SaveAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteBook(long bookId)
        {
            try
            {
                BookEntity book = await _repositoryManager.bookRepository.GetBookById(bookId);
                _repositoryManager.bookRepository.UpdateBook(book);
                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
