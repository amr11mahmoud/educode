import { NgModule } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { CarouselComponent } from 'src/app/components/carousel/carousel.component';
import { FooterComponent } from 'src/app/components/footer/footer.component';
import { HeaderComponent } from 'src/app/components/header/header.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CardComponent } from 'src/app/components/card/card.component';
import { CourseCardComponent } from 'src/app/components/course-card/course-card.component';
import { HttpClientModule } from '@angular/common/http';
import { RatingStarComponent } from 'src/app/components/rating-star/rating-star.component';
import { TestimonialCardComponent } from 'src/app/components/testimonial-card/testimonial-card.component';

@NgModule({
  declarations: [
    CarouselComponent,
    HeaderComponent,
    FooterComponent,
    CardComponent,
    CourseCardComponent,
    RatingStarComponent,
    TestimonialCardComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    HttpClientModule,
  ],
  exports:[
    CarouselComponent,
    HeaderComponent,
    FooterComponent,
    CardComponent,
    CourseCardComponent,
    TestimonialCardComponent
  ]
  
})
export class SharedModule { }
