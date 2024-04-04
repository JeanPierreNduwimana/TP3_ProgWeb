import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { LoginDTO } from '../models/LoginDTO';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegisterDTO } from '../models/RegisterDTO';
import { FlappyService } from '../FlappyService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  hide = true;
  domain : string = "http://localhost:7151/";
  registerUsername : string = "";
  registerEmail : string = "";
  registerPassword : string = "";
  registerPasswordConfirm : string = "";
  loginUsername : string = "";
  loginPassword : string = "";

  constructor(public route : Router,public http : HttpClient,public _flappyservice : FlappyService) { }

  ngOnInit() {

    
  }

  async login() : Promise<void>{

    let logindto = new LoginDTO(this.loginUsername,this.loginPassword);
    let x = await this._flappyservice.login(logindto);
    
    // Redirection si la connexion a réussi :
    let token : string | null = x.token;
    if(token != null){
      localStorage.setItem("token", token);
      this.route.navigate(["/play"]);
    }
    
  }

  async register(): Promise<void>{
    let registerdto = new RegisterDTO(this.registerUsername,this.registerEmail,this.registerPassword,this.registerPasswordConfirm);
    let x = await this._flappyservice.register(registerdto);
    alert(x.message);
  }

}
