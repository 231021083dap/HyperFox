import { UserService } from 'src/app/user.service.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/models';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  users: User[] = [];
  user: User = { UserId:0, UserName:"", Email:"", Password:"" }

  constructor(private userService:UserService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  //GetAll
  getUsers():void{
    this.userService.getUsers().subscribe(a => this.user = a); //Fix this at home
  }

  //Edit
  edit(user:User):void{
    this.user = user;
  }
  //Delete
  delete(user:User):void {
    if(confirm("Er du sikker på du vil slette?")){
      this.userService.deleteUser(user.UserId).subscribe(() => {
        this.getUsers();
      })
    }
  }

  //Cancel
  cancel():void{
    this.user = { UserId:0, UserName:"", Email:"", Password:""}
  }

  //Create
  save():void{
    if(this.user.UserId == 0){
      this.userService.addUser(this.user).subscribe(a => {
        this.users.push(a);
        this.cancel();
      });
    }else{
      this.userService.updateUser(this.user.UserId, this.user).subscrube(() => this.cancel());
    }
  }

}
