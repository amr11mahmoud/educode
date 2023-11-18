import { Component, Input, OnInit } from '@angular/core';
import { courseLevel } from 'src/app/core/enums/courseLevel';
import { courseTag } from 'src/app/core/enums/courseTag';
import { Course } from 'src/app/core/models/course';

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.scss']
})
export class CourseCardComponent{

  @Input() Course!: Course;

  public courseLevel(_courselevel:courseLevel):string {
    return courseLevel[_courselevel]; 
  }

  public courseTag(_courseTag:courseTag):string {
    return courseTag[_courseTag]; 
  }

  public priceAfterDiscount(price:number, discount:number):string{
    if(discount == null) return price.toString();
    if(discount === 0 || discount < 0) return price.toString();
    return (price - (price*(discount/100))).toFixed(2);
  }
}
