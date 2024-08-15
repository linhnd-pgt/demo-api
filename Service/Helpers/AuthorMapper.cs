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
    }

    public class AuthorMapper : IAuthorMapper
    {
        public AuthorEntity AuthorDtoToEntity(AuthorDTO authorDTO) => new AuthorEntity(authorDTO.id, authorDTO.name, authorDTO.biography, authorDTO.dateOfBirth);


    }
}
