import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Hero } from '../models/Hero';
import { HeroesService } from '../_services/heroes.service';

@Component({
  selector: 'app-new-hero',
  templateUrl: './new-hero.component.html',
  styleUrls: ['./new-hero.component.css']
})
export class NewHeroComponent implements OnInit {
  hero :Hero = {} as Hero ;
  model :any = {};
  ability:string;
  validationError :string[] = [];


  constructor(private heroesService : HeroesService, private router:Router) { }

  ngOnInit(): void {
  }

  createHero(){
    let userId = JSON.parse(localStorage.getItem('user'))?.id;
    this.hero.trainerId = userId;
    this.hero.currentPower = this.hero.startingPower;
    this.hero.ability = this.ability;
    console.log(this.hero);
    this.heroesService.createHero(this.hero).subscribe({
      next : (res) =>    { this.router.navigateByUrl('/heroes')},
      error : (err) => {this.validationError.push(err.error);console.log(this.validationError)}

    });
    //this.router.navigateByUrl('/heroes');
  }
}
