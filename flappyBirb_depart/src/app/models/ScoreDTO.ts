export class ScoreDTO{


    constructor(
        public Id : number,
        public timeInSeconds : string,
        public scoreValue : string,
        public isPublic : boolean
    ){}
}