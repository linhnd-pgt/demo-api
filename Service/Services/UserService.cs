using Microsoft.EntityFrameworkCore;
using Service.Constants;
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

        Task<string> CreateUser(UserDTO userDTO);

        Task<string> UpdateUser(UserDTO userDTO, string username);

        Task<UserEntity> GetUserLoggedIn(string email, string password);

        Task<UserEntity> GetUserByUsername(string username);

        Task<UserEntity> GetUserById(long id);
        
        Task<UserDTO> UpdateUserWithEntity(UserEntity user);

    }

    public class UserService : ServiceBase, IUserService
    {

        private readonly UserMapper _userMapper;

        public UserService(IRepositoryManager repositoryManager) : base(repositoryManager) 
        {
            _userMapper = new UserMapper();
        }

        public async Task<string> CreateUser(UserDTO userDTO)
        {
            try
            {
                var user = _userMapper.userDtoToEntity(userDTO);
                user.Role = Enums.EnumType.Role.ROLE_MEMBER;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                user.CreatedBy = user.UserName;
                user.UpdatedBy = user.UserName;
                _repositoryManager.userRepository.Create(user);
                await _repositoryManager.SaveAsync();
                return DevMessageConstants.ADD_OBJECT_SUCCESS;
            }
            catch(Exception ex)
            {
                return DevMessageConstants.ADD_OBJECT_FAILED + "Error: " + ex;
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

        public async Task<string> UpdateUser(UserDTO userDTO, string username)
        {
            UserEntity existedUser = await _repositoryManager.userRepository.GetUserById(userDTO.Id);
            if(existedUser != null)
            {
                try
                {
                    switch (userDTO.Role)
                    {
                        case "ROLE_ADMIN":
                            existedUser.Role = Enums.EnumType.Role.ROLE_ADMIN;
                            break;
                        case "ROLE_LIBRARIAN":
                            existedUser.Role = Enums.EnumType.Role.ROLE_LIBRARIAN;
                            break;
                        case "ROLE_MEMBER":
                            existedUser.Role = Enums.EnumType.Role.ROLE_MEMBER;
                            break;
                    }

                    existedUser.UpdatedBy = username;

                    _repositoryManager.userRepository.Update(existedUser);

                    await _repositoryManager.SaveAsync();

                    return DevMessageConstants.NOTIFICATION_UPDATE_SUCCESS;
                }
                catch (Exception ex)
                {
                    return DevMessageConstants.NOTIFICATION_UPDATE_FAILED + "Error: " + ex;
                }
            }
            return DevMessageConstants.OBJECT_NOT_FOUND;
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
