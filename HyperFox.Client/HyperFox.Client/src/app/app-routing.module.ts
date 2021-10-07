import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FilmComponent } from './components/admin/film/film.component';
import { GenreComponent } from './components/admin/genre/genre.component';
import { GenreFilmComponent } from './components/genre-film/genre-film.component';
import { HomeComponent } from './components/home/home.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { UserComponent } from './components/admin/user/user.component';
import { Admin } from './models';
import { AuthGuard } from './_Assets/auth.guard';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { SingleFilmComponent } from './components/single-film/single-film.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'info', component: InfoComponent},
  {path: 'login', component: LoginComponent},
  {path: 'admin/film',  component: FilmComponent, canActivate: [AuthGuard],data: {roles: [Admin.Admin]}},
  {path: 'admin/user', component: UserComponent, canActivate: [AuthGuard],data: {roles: [Admin.Admin]}},
  {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard],data: {roles: [Admin.User,Admin.Admin]}},
  {path: 'register', component: RegisterComponent},
  {path: 'single-film', component: SingleFilmComponent},
  {path: 'genre-film', component: GenreFilmComponent},
  {path: 'admin/genre', component: GenreComponent, canActivate: [AuthGuard],data: {roles: [Admin.Admin]}}
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
