using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Entities;
using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{

    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        
        void AddUser(UserEntity user);

        Task<UserEntity> GetUserByUserName(string username);

        Task<UserEntity> GetUserById(long id);


    }

    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {

        public UserRepository(RepositoryContext dbContext)  : base(dbContext) { }

        public void AddUser(UserEntity user) => Create(user);

        public async Task<UserEntity> GetUserByUserName(string username)
        {
            return await _dbContext.User
                .Where(u => !string.IsNullOrEmpty(username) || u.UserName.Equals(username))
                .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetUserById(long id)
        {
            return await _dbContext.User
                .Where(u => id != 0 || u.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

    }
}
