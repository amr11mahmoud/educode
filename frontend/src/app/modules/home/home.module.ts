import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HeroComponent } from './components/hero/hero.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PartnersComponent } from './components/partners/partners.component';
import { CarouselComponent } from 'src/app/components/carousel/carousel.component';
import { SharedModule } from '../shared/shared.module';
// import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    HomeComponent,
    HeroComponent,
    PartnersComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    SharedModule
  ]
})
export class HomeModule { }
