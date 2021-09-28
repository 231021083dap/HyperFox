export interface User{
    UserId:Number,
    UserName:String,
    Email:String,
    Password:String,
    Admin?:Admin
}

export enum Admin{
    User = "User",
    Admin = "Admin"
}