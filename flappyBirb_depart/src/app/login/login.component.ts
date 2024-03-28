import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { LoginDTO } from '../models/LoginDTO';
import { HttpClient } from '@angular/common/http';
import { RegisterDTO } from '../models/RegisterDTO';

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

  constructor(public route : Router,public http : HttpClient) { }

  ngOnInit() {
  }

  async login() : Promise<void>{

    let logindto = new LoginDTO(this.loginUsername,this.loginPassword);
    let x = await lastValueFrom(this.http.post<LoginDTO>( this.domain + "api/Users/Login", logindto ));
    console.log(x);



    // Redirection si la connexion a réussi :
    //this.route.navigate(["/play"]);
  }

  async register(): Promise<void>{


    let registerdto = new RegisterDTO(this.registerUsername,this.registerEmail,this.registerPassword,this.registerPasswordConfirm);
    let x = await lastValueFrom(this.http.post<RegisterDTO>( this.domain + "api/Users/Register", registerdto));

    console.log(x);
  }

}
