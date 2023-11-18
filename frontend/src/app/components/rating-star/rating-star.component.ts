import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-rating-star',
  templateUrl: './rating-star.component.html',
  styleUrls: ['./rating-star.component.scss']
})
export class RatingStarComponent implements OnInit{

  stars:boolean[][] = [[false, false, false]];
  @Input() rating:number = 0;
  @Input() numberOfStars:number = 5;

  ngOnInit(): void {
    let floorNumOfFull:number = Math.floor(this.rating);
    let ceilNumOfFull:number = Math.ceil(this.rating);
    let numOfEmpty:number = this.numberOfStars - floorNumOfFull;
    let numOfHalf:number = ceilNumOfFull - floorNumOfFull;

    for (let index = 1; index <= floorNumOfFull; index++) {
      this.stars.push([true, false, false])
    }

    for (let index = 1; index === numOfHalf; index++) {
      this.stars.push([false, true, false])
    }

    for (let index = 1; index <= numOfEmpty - numOfHalf; index++) {
      this.stars.push([false, false, true])
    }
  }

  public createRange(length:number){
    return new Array(length).fill(0).map((n, index)=> index +1);
  }
}
