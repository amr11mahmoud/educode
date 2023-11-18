import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Card } from 'src/app/core/models/card';
import { Category } from 'src/app/core/models/category';
import { testimonial } from 'src/app/core/models/testimonial';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchTopCategories();
  }

  carouselIndex: number[] = [1, 2, 3, 4, 5, 6];
  carouselCards: Card[] = [];
  testmonials: testimonial[] = [];

  public fetchTopCategories() {
    this.http.get<Category[]>('http://localhost:3000/topCategories').subscribe({
      next: (categories) => {
        categories.map((val) => {
          this.carouselCards.push(
            new Card(
              val.id,
              val.title,
              `${val.courses}+ Courses`,
              `../../../assets/categories-icons/${val.iconPath}`
            )
          );
        });
      },
      error: (err) => console.log(err),
    });
  }
}
