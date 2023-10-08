import {
  Component,
  ElementRef,
  Input,
  ViewChild,
  inject,
} from '@angular/core';

import { Card } from 'src/app/core/models/card';

import {
  faCircle,
  faArrowLeft,
  faArrowRight,
} from '@fortawesome/free-solid-svg-icons';

import { RxState } from '@rx-angular/state';
import { arrowDirections } from 'src/app/core/enums/arrowDirections';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss'],
  providers: [RxState],
})

export class CarouselComponent {
  
  state: RxState<{ activeCarouselPage: number }> = inject(RxState);
  state$ = this.state.select();

  activePage$?:Subscription;

  @ViewChild("carousel") carouselCards?:ElementRef;
  
  @Input() Title: string = '';
  @Input() SubTitle: string = '';
  @Input() Cards: Card[] = [];
  @Input() Indexes:number[] = [];

  faCircle = faCircle;
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  arrowDirections = arrowDirections;

  ngAfterViewInit(){
    this.activePage$ = this.state$.subscribe({
      next:()=> {
        this.carouselCards?.nativeElement.scrollTo({
          left: this.state.get().activeCarouselPage * 200,
          behavior: "smooth"
        })
      },
      error:()=> console.log("error"),
      complete:()=> console.log("activeScrollDots Subscriber Completed")
    });
  }

  onDotClick(id: number) {
    this.state.set({
      ...this.state,
      activeCarouselPage: id,
    });
  }

  onArrowClick(dir: arrowDirections) {
    const currentIndex = this.state.get().activeCarouselPage;

    if (dir === arrowDirections.Left) {
      if (currentIndex === 0) {
        this.state.set({
          ...this.state,
          activeCarouselPage: this.Indexes.length - 1,
        });
      } else {
        this.state.set({
          ...this.state,
          activeCarouselPage: currentIndex - 1,
        });
      }
      return;
    }

    if (dir === arrowDirections.Right) {
      if (currentIndex === this.Indexes.length - 1) {
        this.state.set({
          ...this.state,
          activeCarouselPage: 0,
        });
      } else {
        this.state.set({
          ...this.state,
          activeCarouselPage: currentIndex + 1,
        });
      }
    }
  }

  constructor() {
    this.state.set({
      ...this.state,
      activeCarouselPage: 1,
    }); 
  }

  ngOnDestroy(){
    this.activePage$?.unsubscribe()
  }
  
}
