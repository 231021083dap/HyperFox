export interface Genre {
    GenreId: number;
    GenreName: string;
    Films: Film[];
}

export interface Film {
    FilmId: number;
    FilmName: string;
    ReleaseDate: string;
    RunTimeInMin: number;
    Description: string;
    Price: number;
    Stock: number;
    Image: string;
    GenreId: number;
    Genre?: Genre;
}