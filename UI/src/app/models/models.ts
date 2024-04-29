export interface SideNavItem{
    title :string;
    link:string;
}
export interface User{
    id: number;
    firstname : string;
    lastname: string;
    email: string;
    mobile:string;
    password: string;
    blocked:boolean;
    active:boolean;
    createdOn:string;
    userType:UserType;
    fine:number;
    }
export enum UserType{
  ADMIN,
  USER,
    
}
export interface Book{
    id : number;
    title:string;
    category : string;
    subCategory: string;
    author:string;
    price : number;
    available : boolean;
    count?: number; 
}
export interface CategoryBooks{
    category : string;
    subCategory: string;
    books: Book[];
}