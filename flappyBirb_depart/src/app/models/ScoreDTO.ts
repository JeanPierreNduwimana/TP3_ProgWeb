export class ScoreDTO{


    constructor(
        public Id : number,
        public timeInSeconds : string,
        public scoreValue : number,
        public isPublic : boolean
    ){}
}