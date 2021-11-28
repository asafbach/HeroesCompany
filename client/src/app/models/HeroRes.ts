import { Hero } from "./Hero";



export interface HeroRes{
    response :string,
    errors:string[],
    hero:Hero,
}