import { Component,ElementRef } from '@angular/core';
import {faBookOpenReader, faGraduationCap, faPlayCircle, faSuitcase, faStar, faMedal} from '@fortawesome/free-solid-svg-icons';
import { fromEvent } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})

export class HeroComponent {

  faGraduationCap = faGraduationCap;
  faPlayCircle =faPlayCircle;
  faBookOpenReader = faBookOpenReader;
  faSuitcase = faSuitcase;
  faStar = faStar;
  faMedal = faMedal;

  mouseMoveSubscription;

  hero_image_style:string = '';

  // TODO search about fromEvent observer and debounceTime opreator
  constructor(elementRef: ElementRef) {
    this.mouseMoveSubscription = fromEvent(
      elementRef.nativeElement,
      'mousemove'
    ).pipe()
    .subscribe(({x, y}:any) => {
      this.hero_image_style = `--x: ${x/20}px; --y: ${y/20}px`;
    });
  }
}
