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

  getGenre(genreId: number): Observable<Genre>{
    return this.http.get<Genre>(`${this.URLApi}/${genreId}`);
  }

  // addAuthor(author: Author): Observable<Author>{
  //   return this.http.post<Author>(this.URLApi, author, this.httpOptions);
  // }

  // updateAuthor(authorId: number, author: Author): Observable<Author>{
  //   return this.http.put<Author>(`${this.URLApi}/${authorId}`, author, this.httpOptions);
  // }

  // deleteAuthor(authorId: number): Observable<boolean>{
  //   return this.http.delete<boolean>(`${this.URLApi}/${authorId}`, this.httpOptions);
  // }
}
