import { Component, Input, OnInit } from '@angular/core';
import { Hero } from '../models/Hero';
import { HeroRes } from '../models/HeroRes';
import { HeroesService } from '../_services/heroes.service';
@Component({
  selector: 'app-hero-card',
  templateUrl: './hero-card.component.html',
  styleUrls: ['./hero-card.component.css']
})
export class HeroCardComponent implements OnInit {
  //@Input() hero : Hero;
  @Input() heroes : Hero[];
  res:any;
  userId:number;
  constructor(private service:HeroesService, ) { }

  ngOnInit(): void {
    this.userId = JSON.parse(localStorage.getItem('user'))?.id;
  }
  
  deleteHero(id:number){
    this.service.deleteHero(id).subscribe((res:HeroRes) => {
      console.log(res);
      this.heroes = this.heroes.filter(hero => hero.id !== id)
    });
    this.service.showSnackBar('Hero deleted');
  }

  trainHero(id:number){
    let hero = this.heroes.find((obj => obj.id == 4));
    this.service.trainHero(id).subscribe((res:HeroRes)=> {
      console.log(res);
      let hero = this.heroes.find((obj => obj.id == id));
      let heroRes = res.hero;
      hero.currentPower = heroRes.currentPower;
      hero.startTrainingDate = heroRes.startTrainingDate;
      this.sortHeroesByPower(this.heroes);
      this.service.showSnackBar(res.response);
    });
  }

  sortHeroesByPower(heroes:Hero[]){
    heroes.sort((a,b) => (a.currentPower > b.currentPower) ? -1: ((b.currentPower > a.currentPower) ? 1 : 0))
  }

  // showSnackBar(txt:string){
  //   this.snackBar.open(txt, 'Ok', {duration:2500});
  // }

}
