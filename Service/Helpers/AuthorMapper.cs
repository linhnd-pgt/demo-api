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
        AuthorEntity AuthorDtoToEntity(AuthorDTO source, AuthorEntity target);

        AuthorDTO AuthorEnityToAuthorDto(AuthorEntity source);

        AuthorEntity AuthorRequestDtoToAuthorEntity(AuthorRequestDTO source, AuthorEntity target);

    }

    public class AuthorMapper : IAuthorMapper
    {
        public AuthorEntity AuthorDtoToEntity(AuthorDTO source, AuthorEntity target) 
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.DateOfBirth = source.DateOfBirth;
            target.Biography = source.Biography;
            return target;
        }

        public AuthorDTO AuthorEnityToAuthorDto(AuthorEntity source) => new AuthorDTO()
        {
            Id = source.Id,
            DateOfBirth = source.DateOfBirth,
            Name = source.Name,
            Biography = source.Biography,
        };

        public AuthorEntity AuthorRequestDtoToAuthorEntity(AuthorRequestDTO source, AuthorEntity target) 
        {
            target.Name = source.Name;
            target.Biography = source.Biography;
            target.DateOfBirth = source.DateOfBirth;
            return target;
        }
    }
}
