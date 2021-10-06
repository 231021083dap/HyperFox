import { Component, OnInit } from '@angular/core';
import { Film, Genre } from 'src/app/models';
import { FilmService } from 'src/app/services/film.service';

@Component({
  selector: 'app-genre-film',
  templateUrl: './genre-film.component.html',
  styleUrls: ['./genre-film.component.css']
})
export class GenreFilmComponent implements OnInit {

  genre: Genre[]=[];
  films: Film[] = [];
  film: Film = { FilmId: 0, FilmName: '', ReleaseDate: '', RunTimeInMin: 0, Description: '', Price: 0, Stock: 0, Image: '', GenreId: 1,};
  
  constructor(private filmService: FilmService) { }

  ngOnInit(): void {
  }

  
}
