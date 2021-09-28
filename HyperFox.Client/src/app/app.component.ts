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

  genres: Genre[] = [];

  constructor(private genreService: GenreService) { }

  ngOnInit(): void {
    this.getGenres();
  }

  getGenres(): void {
    this.genreService.getGenres().subscribe(a =>
      this.genres = a)
  }
}
