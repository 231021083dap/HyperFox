import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FilmComponent } from './components/admin/film/film.component';
import { GenreComponent } from './components/admin/genre/genre.component';
import { GenreFilmComponent } from './components/genre-film/genre-film.component';
import { HomeComponent } from './components/home/home.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { SingleFilmComponent } from './components/single-film/single-film.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'info', component: InfoComponent},
  {path: 'login', component: LoginComponent},
  {path: 'single-film', component: SingleFilmComponent},
  {path: 'genre-film', component: GenreFilmComponent},
  {path: 'admin/film', component: FilmComponent},
  {path: 'admin/genre', component: GenreComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
