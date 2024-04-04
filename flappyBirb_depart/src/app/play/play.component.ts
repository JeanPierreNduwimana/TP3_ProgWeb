import { Component, OnDestroy, OnInit } from '@angular/core';
import { Game } from './gameLogic/game';
import { Score } from '../models/score';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom, of } from 'rxjs';
import { ScoreDTO } from '../models/ScoreDTO';
import { FlappyService } from '../FlappyService';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.css']
})
export class PlayComponent implements OnInit, OnDestroy{

  domain : string = "http://localhost:7151";
  game : Game | null = null;
  scoreSent : boolean = false;

  constructor(public _flappyService : FlappyService){}

  ngOnDestroy(): void {
    // Ceci est crotté mais ne le retirez pas sinon le jeu bug.
    location.reload();
  }

  ngOnInit() {
    this.game = new Game();
  }

  replay(){
    if(this.game == null) return;
    this.game.prepareGame();
    this.scoreSent = false;
  }

  verifierconncetion() : void {
    let token = localStorage.getItem("token");

    if(token == null)
    {
      alert("Vous n'êtes pas connecté(e)");
    }
    else
    {
      this.sendScore(token);
    }
    
  }

  async sendScore(token : string): Promise<void>{
    if(this.scoreSent) return;
    this.scoreSent = true;
    await this._flappyService.AddScore(token);
  }

}
