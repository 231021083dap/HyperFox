import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../authentication.service';
import { User } from '../models';
import { UserService } from '../user.service.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  user: User = { UserId: 0,UserName:'',Email:'', Password:''};

  constructor(private userService:UserService, private authenticationService: AuthenticationService) { 
    this.authenticationService.currentUser.subscribe(x => this.user = x);
  }

  ngOnInit(): void {
    
    this.getUser(this.user);
  }

  //Get user by UserId
  getUser(user:User):void{
    this.userService.getUser(user.UserId).subscribe(a => this.user = a);
  }

  //Edit
  edit(user:User):void{
    this.user = user;
  }

  save():void{

      this.userService.updateUser(this.user.UserId, this.user).subscribe();
    }

  //Delete
  delete(user:User):void {
    if(confirm("Er du sikker på du vil slette?")){
      this.userService.deleteUser(user.UserId).subscribe();
    }
  }

 
}