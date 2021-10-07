import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenreFilmComponent } from './genre-film.component';

describe('GenreFilmComponent', () => {
  let component: GenreFilmComponent;
  let fixture: ComponentFixture<GenreFilmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenreFilmComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenreFilmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
