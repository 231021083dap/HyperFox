import { Component, OnInit } from '@angular/core';
import { Genre } from './models';
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

  constructor(private genreService: GenreService) { }

  ngOnInit(): void {
    // Prints/shows data in category dropdown
    this.getGenres();
  }

  // Get all the existing genre data from api
  getGenres(): void {
    this.genreService.getGenres().subscribe(a =>
      this.genres = a)
  }
}
