using System;
using System.ComponentModel.DataAnnotations;
using BookStore.Models;

namespace BookStore.DTOs
{
	public class AuthorCreateDTO
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static Author createAuthorFromDTO(AuthorCreateDTO authorCreateDTO)
        {
            return new Author
            {
                FirstName = authorCreateDTO.FirstName,
                LastName = authorCreateDTO.LastName,
                // You may add other properties as needed
            };
        }
    }
}

