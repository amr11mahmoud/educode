import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  // encapsulation:ViewEncapsulation.None,
  // encapsulation:ViewEncapsulation.Emulated,
  // encapsulation:ViewEncapsulation.ShadowDom,
})
export class AppComponent {
  title = 'frontend';
}
