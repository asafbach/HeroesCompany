import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Hero } from '../models/Hero';
import { AccountService } from '../_services/account.service';
import { HeroesService } from '../_services/heroes.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heroes : any; 
  constructor(private service:HeroesService, private router:Router) { 
  }

  ngOnInit(): void {
console.log('heroes init...')
    let url : string = this.router.routerState.snapshot.url;
    if(url.includes('allHeroes')) this.getAllHeroes();
    else this.getHeroes();
  }

  getHeroes(){
    this.service.getHeroes().subscribe((heroes:Hero[]) => {
      console.log(heroes);
      this.sortHeroesByPower(heroes);
      this.heroes = heroes
    });  }
  getAllHeroes(){
    this.service.getAllHeroes().subscribe((heroes:Hero[]) => {
      this.sortHeroesByPower(heroes);
      this.heroes = heroes
    });
  }
  sortHeroesByPower(heroes:Hero[]){
    //heroes.sort((a,b) => a.currentPower > b.currentPower ? 1:-1)
    heroes.sort((a,b) => (a.currentPower > b.currentPower) ? -1: ((b.currentPower > a.currentPower) ? 1 : 0))
  }


}
