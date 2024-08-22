using Microsoft.AspNetCore.Http;
using Service.DTOs;
using Service.Entities;
using Service.Helpers;
using Service.Repositories.Base;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{

    public interface IAuthorService
    {
        Task<List<AuthorDTO>> GetAllAuthors();

        Task<List<AuthorDTO>> GetAuthorListPaginated(int page, int pageSize);

        Task<List<AuthorDTO>> GetAuthorByName(string keyword);

        AuthorEntity GetAuthorById(long id);

        Task<bool> CreateAuthor(AuthorRequestDTO authorDTO, string username);

        Task<bool> UpdateAuthor(AuthorRequestDTO authorDTO, string username, long authorId);

        Task<bool> DeleteAuthor(long authorID);
    }

    public class AuthorService : ServiceBase, IAuthorService
    {
   
        private readonly IAuthorMapper _authorMapper;

        public AuthorService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
            _authorMapper = new AuthorMapper();
        }

        public async Task<List<AuthorDTO>> GetAllAuthors()
        {
            var authorEntityList = await _repositoryManager.authorRepository.GetAllAuthors();
            return authorEntityList.Select(e => _authorMapper.AuthorEnityToAuthorDto(e)).ToList();
        }

        public async Task<List<AuthorDTO>> GetAuthorListPaginated(int page, int pageSize)
        {
            var authorEntityList = await _repositoryManager.authorRepository.GetAllAuthorsPagenated(page, pageSize);
            return authorEntityList.Select(e => _authorMapper.AuthorEnityToAuthorDto(e)).ToList();
        }

        public AuthorEntity GetAuthorById(long id) => _repositoryManager.authorRepository.GetAuthorById(id).Result;
        public async Task<bool> CreateAuthor(AuthorRequestDTO authorDTO, string username)
        {
            try
            {
                AuthorEntity author = new AuthorEntity();
                author = _authorMapper.AuthorRequestDtoToAuthorEntity(authorDTO, author);
                author.CreatedBy = username;
                author.UpdatedBy = username;
                _repositoryManager.authorRepository.Create(author);
                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<bool> UpdateAuthor(AuthorRequestDTO authorDTO, string username, long authorId)
        {
            AuthorEntity author = await _repositoryManager.authorRepository.GetAuthorById(authorId);
            if (author != null)
            {
                try
                {
                    author = _authorMapper.AuthorRequestDtoToAuthorEntity(authorDTO, author);
                    author.CreatedBy = username;
                    author.UpdatedBy = username;
                    _repositoryManager.authorRepository.Update(author);
                    await _repositoryManager.SaveAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> DeleteAuthor(long authorID)
        {
            try
            {
                _repositoryManager.authorRepository.Delete(GetAuthorById(authorID));
                await _repositoryManager.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<AuthorDTO>> GetAuthorByName(string keyword)
        {
            return await _repositoryManager.authorRepository.FilterAuthorByName(keyword);
        }

    }

}
