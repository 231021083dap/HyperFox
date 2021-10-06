import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Film } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FilmService {
  getFilmsByGenreId(GenreId: number) {
    throw new Error('Method not implemented.');
  }

  private URLApi = 'https://localhost:5001/api/Film'

  constructor(private http: HttpClient) { }

  // getFilmsByGenreId(GenreId: number): Observable<Film[]>{
  //   return this.http.get<Film[]>(`${this.URLApi}/${FilmId}`);
  // }

  getFilm(FilmId: number): Observable<Film>{
    return this.http.get<Film>(`${this.URLApi}/${FilmId}`);
  }
}
