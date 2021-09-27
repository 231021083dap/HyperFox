import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { SlideComponent } from './components/slide/slide.component';
import { InfoComponent } from './components/info/info.component';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { FilmComponent } from './components/admin/film/film.component';
import { GenreComponent } from './components/admin/genre/genre.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SlideComponent,
    InfoComponent,
    LoginComponent,
    AdminComponent,
    FilmComponent,
    GenreComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
