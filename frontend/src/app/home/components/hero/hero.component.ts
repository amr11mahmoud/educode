import { Component } from '@angular/core';
import {faBookOpenReader, faGraduationCap, faPlayCircle} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent {
  faGraduationCap = faGraduationCap;
  faPlayCircle =faPlayCircle;
  faBookOpenReader = faBookOpenReader;
}
