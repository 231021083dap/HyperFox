import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { SlideComponent } from './components/slide/slide.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { FilmComponent } from './components/admin/film/film.component';
import { UserComponent } from './components/admin/user/user.component';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtInterceptor } from './_Assets/jwt.interceptor';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { SingleFilmComponent } from './components/single-film/single-film.component';
import { GenreFilmComponent } from './components/genre-film/genre-film.component';
import { GenreComponent } from './components/admin/genre/genre.component';

@NgModule({
  declarations: [
    GenreComponent,
    AppComponent,
    HomeComponent,
    SlideComponent,
    InfoComponent,
    LoginComponent,
    FilmComponent,
    UserComponent,
    RegisterComponent,
    ProfileComponent,
    SingleFilmComponent,
    GenreFilmComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
