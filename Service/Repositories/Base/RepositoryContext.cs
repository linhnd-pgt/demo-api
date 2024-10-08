﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Service.Entities;
using Service.Entities.@base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories.Base
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions<RepositoryContext> dbContextOptions) : base (dbContextOptions) 
        {

        }


        // default! is a way to suppress nullability warnings when initializing properties or fields
        public DbSet<UserEntity> User { get; set; } = default!;

        public DbSet<AuthorEntity> Author { get; set; } = default!;

        public DbSet<BookEntity> Book { get; set; } = default!;

        public DbSet<CategoryEntity> Category { get; set; } = default!;

        public DbSet<BookCategoryEntity> BookCategories { get; set; } = default!;   


        // Configuring for Abstract Base Model for all other Models
        private void ConfigureBaseEntity<TAnyModel>(ModelBuilder modelBuilder) where TAnyModel : AbstractBaseEntity
        {

            // Set created date to timestamp
            modelBuilder.Entity<TAnyModel>()
                .Property(abstractBase => abstractBase.CreatedDate)
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Set updated date to timestamp and it will store current timestamp whenever the record is udpated
            modelBuilder.Entity<TAnyModel>()
                .Property(abstractBase => abstractBase.UpdatedDate)
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

          


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            // created date, updated date as timestamp for all tables
            ConfigureBaseEntity<AuthorEntity>(modelBuilder);
            ConfigureBaseEntity<BookEntity>(modelBuilder);
            ConfigureBaseEntity<CategoryEntity>(modelBuilder);
            ConfigureBaseEntity<UserEntity>(modelBuilder);

            // set dob to timme stamp
            modelBuilder.Entity<AuthorEntity>()
                .Property(author => author.DateOfBirth)
                .HasColumnType("TIMESTAMP");

            // set compositekey for book category id
            modelBuilder.Entity<BookCategoryEntity>()
                .HasKey(value => new { value.BookId, value.CategoryId });

            // Convert the enum to a string in the database
            modelBuilder.Entity<UserEntity>()
               .Property(u => u.Role)
               .HasConversion<string>();

            modelBuilder.Entity<BookEntity>()
                .HasMany(book => book.BookCategoryList)
                .WithOne(bookCate => bookCate.Book)
                .HasForeignKey(bookCate => bookCate.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryEntity>()
                .HasMany(cate => cate.CategoryBookList)
                .WithOne(cateBook => cateBook.Category)
                .HasForeignKey(cateBook => cateBook.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);

        }


    }
}
