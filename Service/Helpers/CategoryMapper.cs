using Service.DTOs;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public interface ICategoryMapper
    {
        CategoryRequestDTO CategoryEntityToCategoryRequestDto(CategoryEntity categoryEntity);

        CategoryEntity CategoryRequestDtoToCategoryEntity(CategoryRequestDTO categoryRequestDto);

        CategoryDTO CategoryEntityToCategoryDto(CategoryEntity categoryEntity);
    }

    public class CategoryMapper : ICategoryMapper
    {
        public CategoryDTO CategoryEntityToCategoryDto(CategoryEntity categoryEntity) => new CategoryDTO
        {
            Name = categoryEntity.Name
        };

        public CategoryRequestDTO CategoryEntityToCategoryRequestDto(CategoryEntity categoryEntity) => new CategoryRequestDTO
        {
            Name = categoryEntity.Name
        };

        public CategoryEntity CategoryRequestDtoToCategoryEntity(CategoryRequestDTO categoryRequestDto) => new CategoryEntity
        {
            Name = categoryRequestDto.Name
        };
    }
}
