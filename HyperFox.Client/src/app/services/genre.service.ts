import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from '../models';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  private URLApi = 'https://localhost:5001/api/Genre'

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };
  
  constructor(private http: HttpClient) { }

  getGenres(): Observable<Genre[]>{
    return this.http.get<Genre[]>(this.URLApi);
  }

  getGenre(GenreId: number): Observable<Genre>{
    return this.http.get<Genre>(`${this.URLApi}/${GenreId}`);
  }

  addGenre(genre: Genre): Observable<Genre>{
    return this.http.post<Genre>(this.URLApi, genre, this.httpOptions);
  }

  updateGenre(GenreId: number, genre: Genre): Observable<Genre>{
    return this.http.put<Genre>(`${this.URLApi}/${GenreId}`, genre, this.httpOptions);
  }

  deleteGenre(GenreId: number): Observable<boolean>{
    return this.http.delete<boolean>(`${this.URLApi}/${GenreId}`, this.httpOptions);
  }
}
