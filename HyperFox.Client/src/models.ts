export interface User{
    UserId:number,
    UserName:string,
    Email:string,
    Password:string,
    Admin?:Admin
}

export enum Admin{
    User = "User",
    Admin = "Admin"
}