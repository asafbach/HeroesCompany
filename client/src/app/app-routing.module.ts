import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { HeroesComponent } from './heroes/heroes.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { NewHeroComponent } from './new-hero/new-hero.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {path:'login', component:LoginComponent},
  {path:'heroes', component:HeroesComponent, canActivate:[AuthGuard]},
  {path:'allHeroes', component:HeroesComponent, canActivate:[AuthGuard]},
  {path:'new-hero', component:NewHeroComponent, canActivate:[AuthGuard]},
  {path:'not-found', component:NotFoundComponent},
  {path:'**', component:HomeComponent, pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
