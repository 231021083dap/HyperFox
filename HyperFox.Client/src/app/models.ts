export interface Genre {
    genreId: number;
    genreName: string;
    films: Film[];
}

export interface Film {
    filmId: number;
    filmName: string;
    releaseDate: string;
    runTimeInMin: number;
    description: string;
    price: number;
    stock: number;
    image: string;
    genreId: number;
    genre?: Genre;
}