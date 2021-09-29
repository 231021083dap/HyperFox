import { Router,ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/authentication.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email: string = '';
  password: string = '';
  submitted = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) { 
    if (this.authenticationService.currentUserValue != null && this.authenticationService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit(){

  }

  login(): void {
    this.error = '';
    this.authenticationService.login(this.email, this.password)
    .subscribe({
      next: () => {

        const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
        this.router.navigate([returnUrl]);

      },
      error: obj => {
        //
        if (obj.error.status == 400 || obj.error.status == 401 || obj.error.status == 500){
          this.error ='Forkert ';
        }
        else{
          this.error = obj.error.title;
          
        }

      }

    });
  }

}
