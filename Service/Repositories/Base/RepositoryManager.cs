using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories.Base
{

    public interface IRepositoryManager
    {

        IUserRepository userRepository { get; }

        IAuthorRepository authorRepository { get; }

        IBookRepository bookRepository { get; }

        IBookCategoryRepository bookCategoryRepository { get; }

        ICategoryRepository categoryRepository { get; }

        Task SaveAsync();

    }

    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _repositoryContext;

        private readonly IUserRepository _userRepository;

        private readonly IAuthorRepository _authorRepository;

        private readonly IBookRepository _bookRepository;

        private readonly IBookCategoryRepository _bookCategoryRepository;

        private readonly ICategoryRepository _categoryRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _repositoryContext = context;
        }

        public IUserRepository userRepository => _userRepository ?? new UserRepository(_repositoryContext);

        public IAuthorRepository authorRepository => _authorRepository ?? new AuthorRepository(_repositoryContext);

        public IBookRepository bookRepository => _bookRepository ?? new BookRepository(_repositoryContext);
        
        public IBookCategoryRepository bookCategoryRepository => _bookCategoryRepository ?? new BookCategoryRepository(_repositoryContext);

        public ICategoryRepository categoryRepository => _categoryRepository ?? new CategoryRepository(_repositoryContext);

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
