  
  export interface User {
    id: number;
    username: string;
    email: string;
    Admin?: Admin;
    token?: string;
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
  