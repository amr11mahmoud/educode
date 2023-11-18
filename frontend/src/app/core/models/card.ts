export class Card{
    id:number;
    title:string;
    subTitle:string;
    iconPath:string;
  
    /**
     *
     */
    constructor(Id:number, Title:string, SubTitle:string, IconPath:string) {
      this.id = Id;
      this.title = Title;
      this.subTitle = SubTitle;
      this.iconPath = IconPath;
    }
    
  }