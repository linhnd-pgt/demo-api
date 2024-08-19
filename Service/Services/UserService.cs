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

        Task<bool> CreateUser(UserDTO userDTO);

        Task<bool> UpdateUser(UserDTO userDTO);

        Task<UserDTO> UpdateUserWithEntity(UserEntity user);

        Task<UserEntity> GetUserLoggedIn(string email, string password);

        Task<UserEntity> GetUserByUsername(string username);

        Task<UserEntity> GetUserById(long id);

    }

    public class UserService : ServiceBase, IUserService
    {

        private readonly UserMapper _userMapper;

        public UserService(IRepositoryManager repositoryManager) : base(repositoryManager) 
        {
            _userMapper = new UserMapper();
        }

        public async Task<bool> CreateUser(UserDTO userDTO)
        {
            try
            {
                var user = _userMapper.userDtoToEntity(userDTO);
                user.Role = Enums.EnumType.Role.ROLE_MEMBER;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                _repositoryManager.userRepository.Create(user);
                await _repositoryManager.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<UserEntity> GetUserLoggedIn(string username, string password)
        {
            // check user with the username first, then proceed to check its password
            var user = await _repositoryManager.userRepository.GetUserByUserName(username);

            // if user cant be found with username and bcrypt cant verify password
            // throw new exception
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new Exception("Username or Password is Incorrect");

            // if not, return to the logged in user
            return user;
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            try
            {
                _repositoryManager.userRepository.Update(_userMapper.userDtoToEntity(userDTO));
                await _repositoryManager.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserDTO> UpdateUserWithEntity(UserEntity user)
        {
            try
            {
                _repositoryManager.userRepository.Update(user);
                await _repositoryManager.SaveAsync();
                return _userMapper.userEntityToDTO(user);
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserEntity> GetUserByUsername(string username) => await _repositoryManager.userRepository.GetUserByUserName(username);
        public async Task<UserEntity> GetUserById(long id) => await _repositoryManager.userRepository.GetUserById(id);

    }
}
