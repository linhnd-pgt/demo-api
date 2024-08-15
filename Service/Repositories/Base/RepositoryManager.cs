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

        Task SaveAsync();

    }

    public class RepositoryManager : IRepositoryManager
    {

        private readonly RepositoryContext _repositoryContext;

        private readonly IUserRepository _userRepository;

        private readonly IAuthorRepository _authorRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _repositoryContext = context;
        }

        public IUserRepository userRepository => _userRepository ?? new UserRepository(_repositoryContext);

        public IAuthorRepository authorRepository => _authorRepository ?? new AuthorRepository(_repositoryContext);

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}
