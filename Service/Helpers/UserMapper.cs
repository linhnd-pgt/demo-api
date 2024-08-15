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
    }

    public class UserMapper : IUserMapper
    {

        public UserEntity userDtoToEntity(UserDTO userDTO) => new UserEntity(userDTO.id, userDTO.username, userDTO.password, userDTO.email);

    }
}
