using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
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
        IBookService BookService { get; }
        ICloudinaryService CloudinaryService { get; }
        ICategoryService CategoryService { get; }
    }

    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<ITokenService> _lazytokenService;

        private readonly Lazy<IUserService> _lazyUserService;

        private readonly Lazy<IAuthorService> _lazyAuthorService;

        private readonly Lazy<IBookService> _lazyBookService;

        private readonly Lazy<ICloudinaryService> _lazyCloudinaryService;

        private readonly Lazy<ICategoryService> _lazyCategoryService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazytokenService = new Lazy<ITokenService> (() => new TokenService(UserService));
            _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));
            _lazyAuthorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager));
            _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager));
            
            // config cloudinary and add it to book service
            Cloudinary cloudinary = new Cloudinary("cloudinary://944914366153752:Qv6TlyPXA0rqWzsWzPlDkHzxMcs@duylinhmedia");
            _lazyCloudinaryService = new Lazy<ICloudinaryService>(() => new CloudinaryService(cloudinary));

            _lazyBookService = new Lazy<IBookService>(() => new BookService(repositoryManager, _lazyCloudinaryService.Value));
        }

        public ITokenService TokenService => _lazytokenService.Value;

        public IUserService UserService => _lazyUserService.Value;

        public IAuthorService AuthorService => _lazyAuthorService.Value;

        public IBookService BookService => _lazyBookService.Value;

        public ICloudinaryService CloudinaryService => _lazyCloudinaryService.Value;

        public ICategoryService CategoryService => _lazyCategoryService.Value;
    }
}
