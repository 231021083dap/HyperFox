import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../models';
import { Observable, observable } from 'rxjs';
import { getMissingNgModuleMetadataErrorData } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = "https://localhost:5001/api/User";

  httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json"})
  };



  constructor(private http: httpClient) {
    
  }

  //Get User
  getUser(userId: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${userId}`);
  }

  //Add User
  addUser(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl, user, this.httpOptions);
  }

  //Update User
  updateUser(userId: number, user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${userId}`, user, this.httpOptions);
  }

  //Delete User
  deleteUser(userId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${userId}`, this.httpOptions);
  }
  
}
