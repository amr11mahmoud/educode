import { HttpClient } from '@angular/common/http';
import {
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { RxState } from '@rx-angular/state';
import { Subscription } from 'rxjs';
import { arrowDirections } from 'src/app/core/enums/arrowDirections';
import { testimonial } from 'src/app/core/models/testimonial';

@Component({
  selector: 'app-testimonial',
  templateUrl: './testimonial.component.html',
  styleUrls: ['./testimonial.component.scss'],
  providers: [RxState],
})
export class TestimonialComponent implements OnInit {
  @Input() title: string = '';
  @Input() subtitle: string = '';
  testimonials: testimonial[] = [];
  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;
  arrowDirections = arrowDirections;

  state: RxState<{ activePage: number }> = inject(RxState);
  state$ = this.state.select();

  activePage$?: Subscription;

  @ViewChild('testimonials_list') testimonials_list?: ElementRef;

  /**
   *
   */
  constructor(private http: HttpClient) {
    this.state.set({
      ...this.state,
      activePage: 0,
    });
  }

  ngOnInit(): void {
    this.fetchTestimonials();
  }
  ngAfterViewInit() {
    this.activePage$ = this.state$.subscribe({
      next: () => {
        this.testimonials_list?.nativeElement.scrollTo({
          left: this.state.get().activePage * 320,
          behavior: 'smooth',
        });
      },
      error: () => console.log('error'),
      complete: () => console.log('activeScrollDots Subscriber Completed'),
    });
  }

  public fetchTestimonials() {
    this.http
      .get<testimonial[]>('http://localhost:3000/testimonials')
      .subscribe({
        next: (testimonials) => {
          this.testimonials = testimonials;
        },
        error: (err) => console.error(err),
        complete: () => {
          console.log('finish testimonials GET');
        },
      });
  }

  public onArrowClick(dir: arrowDirections) {
    const currentIndex = this.state.get().activePage;
    console.log(currentIndex);
    if (dir === arrowDirections.Left) {
      if (currentIndex === 0) {
        this.state.set({
          ...this.state,
          activePage: this.testimonials.length - 2,
        });
      } else {
        this.state.set({
          ...this.state,
          activePage: currentIndex - 1,
        });
      }
      return;
    }

    if (dir === arrowDirections.Right) {
      if (currentIndex === this.testimonials.length - 2) {
        this.state.set({
          ...this.state,
          activePage: 0,
        });
      } else {
        this.state.set({
          ...this.state,
          activePage: currentIndex + 1,
        });
      }
    }

    console.log(this.state.get().activePage)
  }
}
