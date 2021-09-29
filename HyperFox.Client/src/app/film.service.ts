import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Film } from './models';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FilmService {
  private apiUrl = "https://localhost:5001/api/Film";


  httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" })
  };

  constructor(private http: HttpClient) {

  }
    getFilms(): Observable<Film[]> {
        return this.http.get<Film[]>(this.apiUrl)
    }

    getFilm(FilmId: number): Observable<Film> {
        return this.http.get<Film>(`${this.apiUrl}/${FilmId}`);
      }

      addFilm(film: Film): Observable<Film> {
        return this.http.post<Film>(this.apiUrl, film, this.httpOptions);
      }
    
      updateFilm(FilmId: number, film: Film): Observable<Film> {
        return this.http.put<Film>(`${this.apiUrl}/${FilmId}`, film, this.httpOptions);
      }
    
      deleteFilm(FilmId: number): Observable<boolean> {
        return this.http.delete<boolean>(`${this.apiUrl}/${FilmId}`, this.httpOptions);
      }
}