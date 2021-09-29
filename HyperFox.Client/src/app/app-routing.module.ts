import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FilmComponent } from './components/admin/film/film.component';
import { HomeComponent } from './components/home/home.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { UserComponent } from './components/admin/user/user.component';
import { Role } from './models';
import { AuthGuard } from './_Assets/auth.guard';
import { AdminComponent } from './components/admin/admin.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'info', component: InfoComponent},
  {path: 'login', component: LoginComponent},
  {path: 'admin/film',  component: FilmComponent, canActivate: [AuthGuard],data: {roles: [Role.Admin]}},
  {path: 'admin/user', component: UserComponent, canActivate: [AuthGuard],data: {roles: [Role.Admin]}},
  {path: 'admin', component: AdminComponent, canActivate: [AuthGuard],data: {roles: [Role.Admin]}}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
