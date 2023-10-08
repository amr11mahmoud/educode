import { NgModule } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { CarouselComponent } from 'src/app/components/carousel/carousel.component';
import { FooterComponent } from 'src/app/components/footer/footer.component';
import { HeaderComponent } from 'src/app/components/header/header.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CardComponent } from 'src/app/components/card/card.component';


@NgModule({
  declarations: [
    CarouselComponent,
    HeaderComponent,
    FooterComponent,
    CardComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
  ],
  exports:[
    CarouselComponent,
    HeaderComponent,
    FooterComponent,
    CardComponent
  ]
  
})
export class SharedModule { }
