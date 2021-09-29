import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FilmComponent } from './components/admin/film/film.component';
import { HomeComponent } from './components/home/home.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { UserComponent } from './components/admin/user/user.component';
import { Admin } from './models';
import { AuthGuard } from './_Assets/auth.guard';
import { AdminComponent } from './components/admin/admin.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'info', component: InfoComponent},
  {path: 'login', component: LoginComponent},
  {path: 'admin/film',  component: FilmComponent, canActivate: [AuthGuard],data: {roles: [Admin.Admin]}},
  {path: 'admin/user', component: UserComponent, canActivate: [AuthGuard],data: {roles: [Admin.Admin]}},
  {path: 'admin', component: AdminComponent, canActivate: [AuthGuard],data: {roles: [Admin.Admin]}},
  {path: 'register', component: RegisterComponent},
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
