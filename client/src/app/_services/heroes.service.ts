import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Hero } from '../models/Hero';
import { AccountService } from './account.service';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root'
})
export class HeroesService {
  httpOptions ={
    headers: new HttpHeaders({
      Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
    })
  }
  baseUrl= environment.apiUrl;
  constructor(private http :HttpClient, private router:Router, private accountService:AccountService, private snackBar:MatSnackBar) { }

  getHeroes(){
    return this.http.get(this.baseUrl + 'heroes', this.httpOptions);
  }
  getAllHeroes(){
    return this.http.get(this.baseUrl + 'heroes/all', this.httpOptions);
  }
  deleteHero(id:number){
    return this.http.delete(this.baseUrl + 'heroes/'+id, this.httpOptions);
  }
  trainHero(id:number){
    return this.http.put(this.baseUrl + 'heroes/train/'+id,{},this.httpOptions);
  }
  createHero(hero:Hero){
    console.log('sdf', hero);
    return this.http.post(this.baseUrl + 'heroes/create', hero, this.httpOptions);
  }

  showSnackBar(txt:string){
    this.snackBar.open(txt, 'Ok', {duration:2500});
  }


}
