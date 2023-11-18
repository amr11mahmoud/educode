import { AfterContentInit, Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Course } from 'src/app/core/models/course';
import { courseLevel } from 'src/app/core/enums/courseLevel';
import { Category } from 'src/app/core/models/category';

@Component({
  selector: 'app-popular-courses',
  templateUrl: './popular-courses.component.html',
  styleUrls: ['./popular-courses.component.scss'],
})
export class PopularCoursesComponent implements OnInit  {
  categories: Category[] = [];
  activeCategoryId: number = 0;
  allCourses: Course[] = [];
  activeCourses: Course[] = [];
  /**
   *
   */
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchCategories();
    this.fetchCourses();
  }

  fetchCategories() {
    this.http
      .get<Category[]>(
        'http://localhost:3000/categories'
      )
      .subscribe({
        next: (categories) => {
          this.categories = categories;
        },
        error: err=> console.error(err),
        complete: ()=> {
          console.log("finish categories GET")
          this.setActiveCategory(this.categories[0].id)
        }
      });
  }

  fetchCourses(){
    this.http
    .get<Course[]>(
      'http://localhost:3000/courses'
    )
    .subscribe({
      next:(courses) => {
        this.allCourses = courses;
      },
      error: err=> console.error(err),
      complete : ()=> {
        console.log("finish courses GET");
        this.setActiveCourses(this.activeCategoryId);
      }
    });
  }

  setActiveCategory(categoryId: number) {
    this.activeCategoryId = categoryId;
    this.setActiveCourses(this.activeCategoryId);
  }

  setActiveCourses(categoryId: number){
    this.activeCourses = this.allCourses.slice(0, categoryId)
  }
}
