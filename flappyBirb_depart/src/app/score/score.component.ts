import { Component, OnInit } from '@angular/core';
import { Score } from '../models/score';
import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';

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
    this.GetScores();


  }

  async GetScores() : Promise<void>{

    let x = await lastValueFrom(this.http.get<any>( this.domain + "/api/Scores/GetScore"));
    console.log(x);

    for( let i =0; i < x.length; i++){
      this.myScores.push(new Score(x[i].id,x[i].pseudo,x[i].date,x[i].timeInSeconds,x[i].scoreValue,x[i].isPublic));
    }
  
    console.log(this.myScores);
  
    console.log(this.userIsConnected);
    
  }

  async changeScoreVisibility(score : Score){


  }

}
