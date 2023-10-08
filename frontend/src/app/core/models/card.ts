export class Card{
    Id:number;
    Title:string;
    SubTitle:string;
    IconPath:string;
  
    /**
     *
     */
    constructor(Id:number, Title:string, SubTitle:string, IconPath:string) {
      this.Id = Id;
      this.Title = Title;
      this.SubTitle = SubTitle;
      this.IconPath = IconPath;
    }
    
  }