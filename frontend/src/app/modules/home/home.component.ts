import { Component } from '@angular/core';
import { Card } from 'src/app/core/models/card';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],

})

export class HomeComponent {

  carouselIndex:number[] = [1,2,3,4,5,6];

  carouselCards:Card[] = [{
    Id:1,
    Title:"Design <br/> Creative",
    SubTitle:"573+ Courses",
    IconPath: "../../../assets/categories-icons/design-creative.svg"
  },
  {
    Id:2,
    Title:"Sales <br/> Marketing",
    SubTitle:"565+ Courses",
    IconPath: "../../../assets/categories-icons/sales-marketing.svg"
  },
  {
    Id:3,
    Title:"Development <br/> IT",
    SubTitle:"126+ Courses",
    IconPath: "../../../assets/categories-icons/development-it.svg"
  },
  {
    Id:4,
    Title:"Engineering <br/> Architecture",
    SubTitle:"35+ Courses",
    IconPath: "../../../assets/categories-icons/engineering-architecture.svg"
  },
  {
    Id:5,
    Title:"Personal <br/> Development",
    SubTitle:"908+ Courses",
    IconPath: "../../../assets/categories-icons/personal-development.svg"
  },
  {
    Id:6,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:7,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:8,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:9,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:10,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:11,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:12,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:13,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  },
  {
    Id:14,
    Title:"Finance <br/> Accounting",
    SubTitle:"129+ Courses",
    IconPath: "../../../assets/categories-icons/finance-accounting.svg"
  }];
}
