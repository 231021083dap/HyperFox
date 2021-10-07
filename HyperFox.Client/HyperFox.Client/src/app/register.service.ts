import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Register } from './models';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = 'https://localhost:5001/api/User/register';

  httpOptions = {
    headers: new HttpHeaders({'Content-Type':'application/json'})
  };

  constructor(private http: HttpClient) { }

  //addUser(register:Register): Observable<Register>{
    //return this.http.post<Register>(this.apiUrl, register, this.httpOptions);
  //}


  addUser(register: Register): Observable<Register> {
    return this.http.post<Register>(this.apiUrl, register, this.httpOptions);
  }

}
