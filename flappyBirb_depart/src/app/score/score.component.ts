import { Component, OnInit } from '@angular/core';
import { Score } from '../models/score';
import { FlappyService } from '../FlappyService';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {

  myScores : Score[] = [];
  userIsConnected : boolean = false;
  publicScores : Score[] = [];
  listscore : Score[] = [];

  constructor(public _flappyService : FlappyService) { }

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
    this.myScores  = await this._flappyService.GetScores();
  }

  async GetPublicScores() : Promise<void>{
    this.publicScores = [];
    this.publicScores= await this._flappyService.GetPublicScores();
  }

  async changeScoreVisibility(score : Score) : Promise<void>{
    if(score.isPublic) {score.isPublic = false;}
    else { score.isPublic = true;}

    await this._flappyService.PutScore(score);
    this.GetScores();
    this.GetPublicScores();
  }
}