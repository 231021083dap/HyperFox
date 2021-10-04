  export interface User {
    UserId: number;
    UserName: string;
    Email: string;
    Password?:string;
    Admin?: Admin;
    Token?: string;
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
  
  export enum Admin {
    User = 'User',
    Admin = 'Admin'
  }
  