import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HeroComponent } from './components/hero/hero.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PartnersComponent } from './components/partners/partners.component';
import { SharedModule } from '../shared/shared.module';
import { PopularCoursesComponent } from './components/popular-courses/popular-courses.component';
import { TestimonialComponent } from './components/testimonial/testimonial.component';
// import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    HomeComponent,
    HeroComponent,
    PartnersComponent,
    PopularCoursesComponent,
    TestimonialComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    SharedModule
  ]
})
export class HomeModule { }
