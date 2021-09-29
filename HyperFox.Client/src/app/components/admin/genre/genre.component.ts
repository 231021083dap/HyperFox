import { Component, OnInit } from '@angular/core';
import { Genre } from 'src/app/models';
import { GenreService } from 'src/app/services/genre.service';

@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})
export class GenreComponent implements OnInit {

  genres: Genre[] = [];
  genre: Genre = { GenreId: 0, GenreName: '', Films: [] };
  
  constructor(private genreService: GenreService) { }

  ngOnInit(): void {
    this.getGenres();
  }

  getGenres(): void {
    this.genreService.getGenres().subscribe(a =>
      this.genres = a)
  }

  edit(genre: Genre): void {
    this.genre = genre;
  }

  save(): void {
    if (this.genre.GenreId == 0) {
      this.genreService.addGenre(this.genre).subscribe(a => {
        this.genres.push(a)
        this.genre = { GenreId: 0, GenreName: '', Films: [] };
      });
    } else {
      this.genreService.updateGenre(this.genre.GenreId, this.genre).subscribe(() => {
        this.genre = { GenreId: 0, GenreName: '', Films: [] };
      });
    }
  }

  cancel(): void {
    this.genre = { GenreId: 0, GenreName: '', Films: [] };
  }

  delete(genre: Genre): void {
    if (confirm('Are you sure you want to delete?')) {
      this.genreService.deleteGenre(genre.GenreId).subscribe(() => {
          this.getGenres();
        });
    }
  }

  
}
