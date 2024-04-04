import { Component, OnInit } from '@angular/core';
import { Score } from '../models/score';
import { lastValueFrom } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {

  domain : string = "http://localhost:7151";
  myScores : Score[] = [];
  userIsConnected : boolean = false;
  publicScores : Score[] = [];
  listscore : Score[] = [];

  constructor(public http : HttpClient) { }

  async ngOnInit() {

    this.userIsConnected = localStorage.getItem("token") != null;

    if(this.userIsConnected)
    {
      this.GetScores();
    }

    this.GetPublicScores();
    
    
  }

  async GetScores() : Promise<void>{
    this.myScores = [];

    let token = localStorage.getItem("token");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };

    let x = await lastValueFrom(this.http.get<any>( this.domain + "/api/Scores/GetScore", httpOptions));
    for( let i =0; i < x.length; i++)
    {
      this.myScores.push(new Score(x[i].id,x[i].pseudo,x[i].date,x[i].timeInSeconds,x[i].scoreValue,x[i].isPublic));
    }
  }

  async GetPublicScores() : Promise<void>{

    this.publicScores = [];
    let x = await lastValueFrom(this.http.get<any>( this.domain + "/api/Scores/GetPublicScore"));
    console.log(x);

    for( let i =0; i < x.length; i++){
      this.publicScores.push(new Score(x[i].id,x[i].pseudo,x[i].date,x[i].timeInSeconds,x[i].scoreValue,x[i].isPublic));
    }
    
  }

  async changeScoreVisibility(score : Score) : Promise<void>{
    if(score.isPublic) {score.isPublic = false;}
    else { score.isPublic = true;}

    let x = await lastValueFrom(this.http.put<any>( this.domain + "/api/Scores/PutScore", score));
    console.log(x);
    this.GetScores();
    this.GetPublicScores();
  }
}