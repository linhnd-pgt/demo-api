using Service.DTOs;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public interface IAuthorMapper
    {
        AuthorEntity AuthorDtoToEntity(AuthorDTO authorDTO);

        AuthorDTO AuthorEnityToAuthorDto(AuthorEntity authorEntity);

    }

    public class AuthorMapper : IAuthorMapper
    {
        public AuthorEntity AuthorDtoToEntity(AuthorDTO authorDTO) => new AuthorEntity
        {
            Id = authorDTO.Id,
            Name = authorDTO.Name,
            Biography = authorDTO.Biography,
            DateOfBirth = authorDTO.DateOfBirth,

        };

        public AuthorDTO AuthorEnityToAuthorDto(AuthorEntity authorEntity)
        {
            return new AuthorDTO
            {
                Id = authorEntity.Id,
                Name = authorEntity.Name,
                Biography = authorEntity.Biography,
                DateOfBirth = authorEntity.DateOfBirth,
            };
            
        }
    }
}
