  // Blueprint for setting data.
  // Also using models to get data from Entities

  export interface User {
    UserId: number;
    UserName: string;
    Email: string;
    Password?:string;
    Admin?: Admin;
    Token?: string;
  }

  export interface Genre {
    GenreId: number;
    GenreName: string;
    Films: Film[];
}

  export interface Film{
    FilmId: number;
    FilmName: string;
    ReleaseDate: string;
    RunTimeInMin: number;
    Description: string;
    Price: number;
    Stock: number;
    Image: string;
    GenreId: number;


}

  export interface Register{
    Email: string;
    Username: string;
    Password: string;
  }
  
  // Enum of Admin to set difference between Admin and User.
  export enum Admin {
    User = 'User',
    Admin = 'Admin'
  }
  