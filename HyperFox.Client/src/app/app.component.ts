import { Component, OnInit } from '@angular/core';
import { Film, Genre } from './models';
import { FilmService } from './services/film.service';
import { GenreService } from './services/genre.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'HyperFox-Client';

  // Property "genres" of class "Genre" that is an array of data
  genres: Genre[] = [];
  films: Film[] = [];
  film: Film = { FilmId: 0, FilmName: '', ReleaseDate: '', RunTimeInMin: 0, Description: '', Price: 0, Stock: 0, Image: '', GenreId: 1,};

  constructor(private genreService: GenreService, private filmService: FilmService) { }

  ngOnInit(): void {
    // Prints/shows data in category dropdown
    this.getGenres();
  }

  // Get all the existing genre data from api
  getGenres(): void {
    this.genreService.getGenres().subscribe(a =>
      this.genres = a)
  }

  // getFilmsByGenreId(GenreId: number): void {
  //   this.filmService.getFilmsByGenreId(GenreId).subscribe(a =>
  //     this.films = a)
  // }
}
