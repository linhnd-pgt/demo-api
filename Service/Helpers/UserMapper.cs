using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Entities;
using Service.DTOs;

namespace Service.Helpers
{

    public interface IUserMapper
    {
        UserEntity userDtoToEntity(UserDTO userDTO);

        UserDTO userEntityToDTO(UserEntity userEntity);
    }

    public class UserMapper : IUserMapper
    {

        public UserEntity userDtoToEntity(UserDTO userDTO) => new UserEntity(userDTO.Id, userDTO.Username, userDTO.Password, userDTO.Email);

        public UserDTO userEntityToDTO(UserEntity userEntity) => new UserDTO
        {
            Id = userEntity.Id,
            Email = userEntity.Email,
            Username = userEntity.UserName,
            Role = userEntity.Role.ToString(),
        };

    }
}
