using Microsoft.EntityFrameworkCore;
using Service.Entities;
using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{

    public interface ICategoryRepository : IRepositoryBase<CategoryEntity>
    {
        Task<CategoryEntity> GetById(long id);
    }

    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {

        }

        public async Task<CategoryEntity> GetById(long id) => await _dbContext.Category.FirstOrDefaultAsync(x => x.Id == id);
    }
}
