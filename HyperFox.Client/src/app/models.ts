  
  export interface User {
    id: number;
    username: string;
    email: string;
    role?: Role;
    token?: string;
  }
  
  export enum Role {
    User = 'User',
    Admin = 'Admin'
  }
  