  export interface User {
    UserId: number;
    UserName: string;
    Email: string;
    Password?:string;
    Admin?: Admin;
    Token?: string;
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
  