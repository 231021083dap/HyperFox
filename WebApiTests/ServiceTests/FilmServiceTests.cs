using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using WebApi.DTOs.Requests;
using WebApi.DTOs.Responses;
using WebApi.Repositories;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
    public class FilmServiceTests
    {
        private readonly FilmService _sut;
        private readonly Mock<IFilmRepository> _filmRepository = new();
        private readonly Mock<IGenreRepository> _genreRepository = new();

        public FilmServiceTests()
        {
            _sut = new FilmService(_filmRepository.Object, _genreRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfFilmResponses_WhenFilmExist()
        {
            // Arrange
            List<Film> Films = new List<Film>();

            Films.Add(new Film
            {
                FilmId = 1,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                Genre = new Genre
                {
                    GenreId = 1,
                    GenreName = "Action"
                }
            });

            Films.Add(new Film
            {
                FilmId = 2,
                FilmName = "Harry potter",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about the wizard world",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\2.jpg",
                Genre = new Genre
                {
                    GenreId = 2,
                    GenreName = "Comedy"
                }
            });

            _filmRepository
                .Setup(f => f.GetAll())
                .ReturnsAsync(Films);

            // Act
            var result = await _sut.GetAllFilms();

            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<FilmResponse>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfFilmResponses_WhenNoFilmExist()
        {
            // Arrange
            List<Film> Films = new List<Film>();

            _filmRepository
                .Setup(f => f.GetAll())
                .ReturnsAsync(Films);

            // Act
            var result = await _sut.GetAllFilms();

            // Assert 
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<FilmResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAFilmResponse_WhenFilmExists()
        {
            // Arrange
            int filmId = 1;

            Film film = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                Genre = new Genre
                {
                    GenreId = 1,
                    GenreName = "Action"
                }
            };

            _filmRepository
                .Setup(f => f.GetById(It.IsAny<int>()))
                .ReturnsAsync(film);

            // Act
            var result = await _sut.GetById(filmId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FilmResponse>(result);
            Assert.Equal(film.FilmId, result.FilmId);
            Assert.Equal(film.FilmName, result.FilmName);
            Assert.Equal(film.ReleaseDate, result.ReleaseDate);
            Assert.Equal(film.RuntimeInMin, result.RuntimeInMin);
            Assert.Equal(film.Description, result.Description);
            Assert.Equal(film.Price, result.Price);
            Assert.Equal(film.Stock, result.Stock);
            Assert.Equal(film.Image, result.Image);
            Assert.Equal(film.Genre.GenreId, result.Genre.GenreId);
            Assert.Equal(film.Genre.GenreName, result.Genre.GenreName);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenFilmDoesNotExists()
        {
            // Arrange
            int filmId = 1;

            _filmRepository
                .Setup(f => f.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(filmId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnAFilmResponse_WhenCreateIsSuccess()
        {
            // Arrange
            NewFilm newFilm = new NewFilm
            {
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                GenreId = 1
            };

            int filmId = 1;

            Film film = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                GenreId = 1
            };

            _filmRepository
                .Setup(f => f.Create(It.IsAny<Film>()))
                .ReturnsAsync(film);

            Genre genre = new Genre
            {
                GenreId = 1,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(g => g.GetById(It.IsAny<int>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Create(newFilm);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FilmResponse>(result);
            Assert.Equal(filmId, result.FilmId);
            Assert.Equal(newFilm.FilmName, result.FilmName);
            Assert.Equal(newFilm.ReleaseDate, result.ReleaseDate);
            Assert.Equal(newFilm.RuntimeInMin, result.RuntimeInMin);
            Assert.Equal(newFilm.Description, result.Description);
            Assert.Equal(newFilm.Price, result.Price);
            Assert.Equal(newFilm.Stock, result.Stock);
            Assert.Equal(newFilm.Image, result.Image);
            Assert.Equal(newFilm.GenreId, result.Genre.GenreId);
            Assert.Equal(genre.GenreName, result.Genre.GenreName);
        }

        [Fact]
        public async void Update_ShouldReturnUpdatedFilmResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UpdateFilm updateFilm = new UpdateFilm
            {
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                GenreId = 1
            };

            int filmId = 1;

            Film film = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                Genre = new Genre
                {
                    GenreId = 2,
                    GenreName = "Comedy"
                }
            };

            _filmRepository
                .Setup(f => f.Update(It.IsAny<int>(), It.IsAny<Film>()))
                .ReturnsAsync(film);

            Genre genre = new Genre
            {
                GenreId = 1,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(g => g.GetById(It.IsAny<int>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Update(filmId, updateFilm);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FilmResponse>(result);
            Assert.Equal(filmId, result.FilmId);
            Assert.Equal(updateFilm.FilmName, result.FilmName);
            Assert.Equal(updateFilm.ReleaseDate, result.ReleaseDate);
            Assert.Equal(updateFilm.RuntimeInMin, result.RuntimeInMin);
            Assert.Equal(updateFilm.Description, result.Description);
            Assert.Equal(updateFilm.Price, result.Price);
            Assert.Equal(updateFilm.Stock, result.Stock);
            Assert.Equal(updateFilm.Image, result.Image);
            Assert.Equal(updateFilm.GenreId, result.Genre.GenreId);
            Assert.Equal(genre.GenreName, result.Genre.GenreName);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenFilmDoesNotExist()
        {
            // Arrange
            UpdateFilm updateFilm = new UpdateFilm
            {
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                GenreId = 1
            };

            int filmId = 1;

            _filmRepository
                .Setup(f => f.Update(It.IsAny<int>(), It.IsAny<Film>()))
                .ReturnsAsync(() => null);

            Genre genre = new Genre
            {
                GenreId = 1,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(g => g.GetById(It.IsAny<int>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Update(filmId, updateFilm);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            // Arrange
            int filmId = 1;

            Film film = new Film
            {
                FilmId = filmId,
                FilmName = "The lord of the rings",
                ReleaseDate = "16-09-2001",
                RuntimeInMin = 123,
                Description = "This movie is about a ring",
                Price = 79.99M,
                Stock = 50,
                Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                GenreId = 1
            };

            _filmRepository
                .Setup(f => f.Delete(It.IsAny<int>()))
                .ReturnsAsync(film);

            Genre genre = new Genre
            {
                GenreId = 1,
                GenreName = "Action"
            };

            _genreRepository
                .Setup(g => g.GetById(It.IsAny<int>()))
                .ReturnsAsync(genre);

            // Act
            var result = await _sut.Delete(filmId);

            // Assert
            Assert.True(result);
        }
    }
}
