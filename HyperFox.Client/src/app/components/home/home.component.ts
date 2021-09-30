import { Component, OnInit } from '@angular/core';
import { Genre } from 'src/app/models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  genre: Genre = { GenreId: 0, GenreName: '', Films: [] };
  
  constructor() { }

  ngOnInit(): void {
  }

  view(genre: Genre): void {
    this.genre = genre;
  }
}
