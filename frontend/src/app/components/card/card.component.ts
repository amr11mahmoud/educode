import { Component, Input } from '@angular/core';
import { Card } from 'src/app/core/models/card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})

export class CardComponent {

  @Input() Title:string = "";
  @Input() SubTitle:string = "";
  @Input() IconPath:string = "";
}

