using Service.DTOs;
using Service.Entities;
using Service.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Base
{

    public class ServiceBase
    {

        protected IRepositoryManager _repositoryManager;

        public ServiceBase(IRepositoryManager repositoryManager)
        {
            // throw new ArgumentException is to throw an exception dictates that the parameter is currently null
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
        }
        
    }
}
