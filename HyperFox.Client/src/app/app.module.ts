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
import { UserComponent } from './components/admin/user/user.component';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtInterceptor } from './_Assets/jwt.interceptor';

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
    UserComponent
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
