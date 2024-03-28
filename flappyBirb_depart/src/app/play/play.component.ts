import { Component, OnDestroy, OnInit } from '@angular/core';
import { Game } from './gameLogic/game';
import { Score } from '../models/score';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { ScoreDTO } from '../models/ScoreDTO';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.css']
})
export class PlayComponent implements OnInit, OnDestroy{

  domain : string = "http://localhost:7151";
  game : Game | null = null;
  scoreSent : boolean = false;

  constructor(public http : HttpClient){}

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

  async sendScore(): Promise<void>{
    if(this.scoreSent) return;

    this.scoreSent = true;
    
    // ██ Appeler une requête pour envoyer le score du joueur ██
    // Le score est dans sessionStorage.getItem("score")
    // Le temps est dans sessionStorage.getItem("time")
    // La date sera choisie par le serveur

    let token = localStorage.getItem("token");
    let httpOptions = {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + token
      })
    };
    let scoredto = (sessionStorage.getItem("score"));
    let time = sessionStorage.getItem("time");
    
    if(scoredto != null && time != null){
      let score = new ScoreDTO(0,time,scoredto,true);
      let x = await lastValueFrom(this.http.post<ScoreDTO>( this.domain + "/api/Scores/AddScore", score, httpOptions));
      console.log(x);
    }
    





  }

}
