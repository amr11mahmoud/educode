import { Component } from '@angular/core';
import { faChevronDown, faSearch, faShoppingCart, faBarsStaggered } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  faChevronDown = faChevronDown;
  faSearch = faSearch;
  faShoppingCart = faShoppingCart;
  faBarsStaggered = faBarsStaggered;
}
