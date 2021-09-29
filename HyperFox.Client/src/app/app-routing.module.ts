import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FilmComponent } from './components/admin/film/film.component';
import { HomeComponent } from './components/home/home.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { UserComponent } from './components/admin/user/user.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'info', component: InfoComponent},
  {path: 'login', component: LoginComponent},
  {path: 'admin/film', component: FilmComponent},
  {path: 'user', component: UserComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
