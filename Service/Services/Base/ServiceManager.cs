using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Base
{

    public interface IServiceManager
    {
        ITokenService TokenService { get; }
        IUserService UserService { get; }
        IAuthorService AuthorService { get; }
    }

    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<ITokenService> _lazytokenService;

        private readonly Lazy<IUserService> _lazyUserService;

        private readonly Lazy<IAuthorService> _lazyAuthorService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazytokenService = new Lazy<ITokenService> (() => new TokenService(UserService));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));
            _lazyAuthorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager));
        }

        public ITokenService TokenService => _lazytokenService.Value;

        public IUserService UserService => _lazyUserService.Value;

        public IAuthorService AuthorService => _lazyAuthorService.Value;
    }
}
