﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTOs.Responses;

namespace WebApi.Services
{
    public interface IGenreService
    {
        List<GenreResponse> GetAllGenres();
    }

    public class GenreService : IGenreService
    {
        public List<GenreResponse> GetAllGenres()
        {
            List<GenreResponse> Genres = new();

            Genres.Add(new GenreResponse
            {
                GenreId = 1,
                GenreName = "Action"
            });

            Genres.Add(new GenreResponse
            {
                GenreId = 2,
                GenreName = "Comedy"
            });

            return Genres;
        }
    }
}
