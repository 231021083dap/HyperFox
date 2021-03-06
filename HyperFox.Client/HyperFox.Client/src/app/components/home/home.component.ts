import { Component, OnInit } from '@angular/core';
import { Film,Genre } from "../../models";
import { FilmService } from "src/app/film.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  genre: Genre = { GenreId: 0, GenreName: '', Films: [] };
  

  films: Film[] = [];
  film:Film = { FilmId:0, FilmName:"", ReleaseDate:"", RunTimeInMin:0, Description:"", Price:0, Stock:0, Image:"", GenreId:0 };
  constructor(private filmService:FilmService) { }
  
  ngOnInit(): void {
    this.getFilms();
  }
 getFilms():void{
   this.filmService.getFilms()
   .subscribe(a => {
     this.films = a
     console.log(this.films);
    });
 }
  
  view(genre: Genre): void {
    this.genre = genre;
  }
  
}
