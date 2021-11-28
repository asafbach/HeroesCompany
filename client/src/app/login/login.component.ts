import { registerLocaleData } from '@angular/common';
import { ThrowStmt } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model :any = {};
  formType:string = 'r';
  validationError :string[] = [];
  
  constructor(public accountService:AccountService){}
  
  ngOnInit(): void {}

  submitForm(){
    if(this.formType === 'r'){
      this.accountService.register(this.model).subscribe({
        next : (res) => console.log(res),
        error : (err) => {
          if(err.error){
            this.validationError = [];
            this.validationError.push(err.error);
          }
          else
            this.validationError = err;
        }
      });
    }
    else{
      this.accountService.login(this.model).subscribe({
        next : (res) => console.log(res),
        error : (err) => {this.validationError = err;}
      });
    }
  }
}


