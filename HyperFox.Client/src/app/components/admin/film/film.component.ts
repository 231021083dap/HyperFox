import { Component, OnInit } from '@angular/core';
import { Film } from "../../../models";
import { FilmService } from "src/app/film.service";

@Component({
  selector: 'app-film',
  templateUrl: './film.component.html',
  styleUrls: ['./film.component.css']
})
export class FilmComponent implements OnInit {


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
  
 edit(film:Film):void{
  this.film = film;
}

delete(film:Film):void{
  if(confirm("Are you sure you want to delete?")){
    this.filmService.deleteFilm(film.FilmId)
    .subscribe(() => { 
      this.getFilms();
    })
  }
}

//Clear 
cancel():void{
  this.film = { FilmId:0, FilmName:'', ReleaseDate:'', RunTimeInMin:0, Description:"", Price:0, Stock:0, Image:"", GenreId:0 }
}

//Create
save():void{
  if(this.film.FilmId == 0){
    this.filmService.addFilm(this.film)
    .subscribe(a => {
      this.films.push(a); // push = adds to films array, then refreshes
      //Clear 
      this.cancel();
    });
  }else{
      this.filmService.updateFilm(this.film.FilmId, this.film)
      .subscribe(() => this.cancel());
    }
  }

}