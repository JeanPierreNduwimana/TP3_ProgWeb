export class Score{

    constructor(
        public id : number,
        public pseudo : string | null,
        public date : string | null,
        public timeInSeconds : string,
        public scoreValue : string,
        public isPublic : boolean
    ){}

}