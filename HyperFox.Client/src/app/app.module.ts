import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { SlideComponent } from './components/slide/slide.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { FilmComponent } from './components/admin/film/film.component';
import { GenreComponent } from './components/admin/genre/genre.component';
import { SingleFilmComponent } from './components/single-film/single-film.component';
import { GenreFilmComponent } from './components/genre-film/genre-film.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SlideComponent,
    InfoComponent,
    LoginComponent,
    AdminComponent,
    FilmComponent,
    GenreComponent,
    SingleFilmComponent,
    GenreFilmComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
