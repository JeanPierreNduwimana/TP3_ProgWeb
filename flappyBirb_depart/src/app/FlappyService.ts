import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { LoginDTO } from "./models/LoginDTO";
import { lastValueFrom } from "rxjs";
import { RegisterDTO } from "./models/RegisterDTO";
import { Injectable } from "@angular/core";
import { Score } from "./models/score";
import { ScoreDTO } from "./models/ScoreDTO";


const domain : string = "http://localhost:7151/";

@Injectable({
    providedIn: 'root'
  })
  
export class FlappyService {

    constructor(public http : HttpClient){}

    async login(logindto : LoginDTO) : Promise<any>
    {
        let messageerror : string = "";
        let x = await lastValueFrom(this.http.post<any>(domain + "api/Users/Login", logindto)).catch((error: HttpErrorResponse) => {
            messageerror = error.error.text;
            if(error.error.text == null) {
                messageerror = error.error.title;
            }
            if(error.error.title == null) {
                messageerror = error.error.message;
            }
          });
          if(x == "" || x == null)
          {
            x = messageerror;
          }
        return x;
    }

    async register(registerdto : RegisterDTO): Promise<any>
    {
        let messageerror : string = "";
        let x = await lastValueFrom(this.http.post<any>( domain + "api/Users/Register", registerdto)).catch((error: HttpErrorResponse) => {
            messageerror = error.error.text;
            if(error.error.text == null) {
                messageerror = error.error.title;
            }
            if(error.error.title == null) {
                messageerror = error.error.message;
            }
          });

          if(x == "" || x == null)
          {
            x = messageerror;
          }
        return x;
    }

    async AddScore() : Promise<void>
    {
          let scoredto = (sessionStorage.getItem("score"));
          let time = sessionStorage.getItem("time");
          
          if(scoredto != null && time != null)
          {
            let score = new ScoreDTO(0,time,+scoredto,true);
            await lastValueFrom(this.http.post<ScoreDTO>( domain + "api/Scores/AddScore", score));
          }
    }

    async GetScores() : Promise<Score[]>
    {
        let x = await lastValueFrom(this.http.get<any>( domain + "api/Scores/GetScore"));

        let listscore : Score[] = [];

        for( let i =0; i < x.length; i++)
        {
            listscore.push(new Score(x[i].id,x[i].pseudo,x[i].date,x[i].timeInSeconds,x[i].scoreValue,x[i].isPublic));
        }

        return listscore;
    }

    async GetPublicScores() : Promise<Score[]>
    {
        let listscore : Score[] = [];

        let x = await lastValueFrom(this.http.get<any>( domain + "api/Scores/GetPublicScore"));
        
        for( let i =0; i < x.length; i++)
        {
            listscore.push(new Score(x[i].id,x[i].pseudo,x[i].date,x[i].timeInSeconds,x[i].scoreValue,x[i].isPublic));
        }

        return listscore;
    }

    async PutScore(score : Score) : Promise<void>
    {
        await lastValueFrom(this.http.put<any>( domain + "api/Scores/PutScore", score));
    }
}