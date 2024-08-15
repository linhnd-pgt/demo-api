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

        Task<UserEntity> GetUser(UserEntity user);

    }

    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {

        public UserRepository(RepositoryContext dbContext)  : base(dbContext) { }

        public void AddUser(UserEntity user) => Create(user);

        public async Task<UserEntity> GetUser(UserEntity user)
        {
            return await _dbContext.User
                .Where(u => !string.IsNullOrEmpty(user.UserName) && u.UserName.Equals(user.UserName))
                .FirstOrDefaultAsync();
        }

    }
}
