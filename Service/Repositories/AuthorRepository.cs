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

    public interface IAuthorRepository : IRepositoryBase<AuthorEntity>
    {
        Task<List<AuthorEntity>> GetAllAuthors();

        Task<List<AuthorEntity>> GetAllAuthorsPagenated(int page, int pageSize);

        Task<AuthorEntity> GetAuthorById(long id);

        void CreateAuthor(AuthorEntity entity);

        void UpdateAuthor(AuthorEntity entity);

        void DeleteAuthor(AuthorEntity entity);

        Task<List<AuthorDTO>> FilterAuthorByName(string name);

    }

    public class AuthorRepository : RepositoryBase<AuthorEntity>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext context) : base(context)
        {

        }

        public async Task<List<AuthorEntity>> GetAllAuthors() => await _dbContext.Author.ToListAsync();

        public async Task<List<AuthorEntity>> GetAllAuthorsPagenated(int page, int pageSize) => await _dbContext.Author.Skip(page).Take(pageSize).ToListAsync();

        public async Task<AuthorEntity> GetAuthorById(long id) => await _dbContext.Author.Where(author => author.Id == id).FirstOrDefaultAsync();

        public async Task<List<AuthorDTO>> FilterAuthorByName(string name)
        {
            return await _dbContext.Author.Include(author => author.Books)
                            .ThenInclude(book => book.BookCategoryList)
                            .ThenInclude(bookCategory => bookCategory.Category)
                            .Where(author => author.Name.Contains(name))
                            .Select(a => new AuthorDTO
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Biography = a.Biography,
                                DateOfBirth = a.DateOfBirth,
                                Books = a.Books.Select(bc => new BookAuthorDTO
                                {
                                    Id = bc.Id,
                                    Title = bc.Title,
                                    PublishDate = bc.PublishedDate,
                                    Image = bc.Image,
                                    Categories = bc.BookCategoryList.Select(bc => new CategoryAuthorDTO
                                    {
                                        Id = bc.Category.Id,
                                        Name = bc.Category.Name,
                                    }).ToList()
                                }).ToList()
                            }).ToListAsync();
        }

        public void CreateAuthor(AuthorEntity author) => Create(author);

        public void UpdateAuthor(AuthorEntity author) => Update(author);

        public void DeleteAuthor(AuthorEntity author) => Delete(author);
    }
}
