import { Component, OnInit } from '@angular/core';
import { Register } from '../models';
import { RegisterService } from '../register.service';
import { Router } from '@angular/router';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
  registers: Register[] = [];
  register: Register = { Email: '', Username: '', Password: ''}

  constructor(private registerService: RegisterService, private route: Router) { }

  ngOnInit(): void {
  }

  cancel():void{
    this.register = { Email: '', Username: '', Password: ''}
  }

  Create(): void {
    this.registerService.addUser(this.register).subscribe(a => {
      this.registers.push(a);
      this.cancel();
      this.route.navigate(['/login']);
    });
    
  }
}
