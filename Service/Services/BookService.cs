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

        Task<bool> AddBook(BookRequestDTO bookDTO);

        Task<bool> UpdateBook(BookRequestDTO bookDTO);

        Task<bool> DeleteBook(long bookId);
    }

    public class BookService : ServiceBase, IBookService
    {

        private readonly IBookMapper _bookMapper = new BookMapper();

        public BookService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
            _bookMapper = new BookMapper();
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

        public async Task<bool> AddBook(BookRequestDTO bookDTO)
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

        public async Task<bool> UpdateBook(BookRequestDTO bookDTO)
        {
            BookEntity bookEntity = await _repositoryManager.bookRepository.GetBookById(bookDTO.Id);
            if (bookEntity != null)
            {
                try
                {
                    BookEntity book = _bookMapper.BookRequestDtoToBookEntity(bookDTO);

                    List<BookCategoryEntity> bookCategoryEntities = new List<BookCategoryEntity>();

                    foreach (var categoryId in bookDTO.CategoryIdList)
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

                    List<BookCategoryEntity> bookCategories = _repositoryManager.bookCategoryRepository.GetByBookId(bookDTO.Id).Result;

                    _repositoryManager.bookCategoryRepository.DeleteBookCategoryByBookId(bookCategories);

                    await _repositoryManager.SaveAsync();

                    _repositoryManager.bookCategoryRepository.AddRangeBookCategory(bookCategoryEntities);

                    book.BookCategoryList = new Collection<BookCategoryEntity>();


                    foreach (var bookCategory in bookCategoryEntities)
                    {
                        book.BookCategoryList.Add(bookCategory);
                    }

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
