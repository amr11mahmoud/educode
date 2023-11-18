import { Component, Input } from '@angular/core';
import { testimonial } from 'src/app/core/models/testimonial';

@Component({
  selector: 'app-testimonial-card',
  templateUrl: './testimonial-card.component.html',
  styleUrls: ['./testimonial-card.component.scss']
})
export class TestimonialCardComponent {

  @Input() testimonial!:testimonial;
}
