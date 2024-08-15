using Microsoft.EntityFrameworkCore;
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

    public interface IUserService
    {

        void CreateUser(UserDTO user);

        Task<UserEntity> Login(LogInDTO logInDTO);

    }

    public class UserService : ServiceBase, IUserService
    {

        private readonly UserMapper _userMapper;

        public UserService(IRepositoryManager repositoryManager) : base(repositoryManager) 
        {
            _userMapper = new UserMapper();
        }

        public async Task<UserEntity> Login(LogInDTO logInDTO) => await _repositoryManager.userRepository.GetUser(new UserEntity() { UserName = logInDTO.username });

        public void CreateUser(UserDTO userDTO) => _repositoryManager.userRepository.Create(_userMapper.userDtoToEntity(userDTO));
    }
}
