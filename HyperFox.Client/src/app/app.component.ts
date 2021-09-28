import { Component } from '@angular/core';
import {Router} from '@angular/router';
import { User } from './models';
import { AuthenticationService } from './authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  currentUser: User = {id: 0, email: '', username:''};
  title = 'HyperFox-Client';
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ){
    //
    this.authenticationService.

  }
}
