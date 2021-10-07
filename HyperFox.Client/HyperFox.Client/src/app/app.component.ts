
import {Router} from '@angular/router';
import { User,Film,Genre } from './models';
import { AuthenticationService } from './authentication.service';
import { Component, OnInit } from '@angular/core';
import { FilmService } from './services/film.service';
import { GenreService } from './services/genre.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'HyperFox-Client';

  // Property "genres" of class "Genre" that is an array of data
  genres: Genre[] = [];
  films: Film[] = [];
  film: Film = { FilmId: 0, FilmName: '', ReleaseDate: '', RunTimeInMin: 0, Description: '', Price: 0, Stock: 0, Image: '', GenreId: 1,};

  currentUser: User = {UserId: 0, UserName:'', Email: '',};
  constructor(
    private router: Router,
    private genreService: GenreService, 
    private filmService: FilmService,
    private authenticationService: AuthenticationService
  ){
    // get the current user from authentication service
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    // Prints/shows data in category dropdown
    this.getGenres();
  }

  logout() {
    if (confirm('Er du sikker på du vil logge ud')) {
      // ask authentication service to perform logout
      this.authenticationService.logout();

      // subscribe to the changes in currentUser, and load Home component
      this.authenticationService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['/']);
      });
    }
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
