  
  export interface User {
    id: number;
    username: string;
    email: string;
    Admin?: Admin;
    token?: string;
  }
  
  export enum Admin {
    User = 'User',
    Admin = 'Admin'
  }
  