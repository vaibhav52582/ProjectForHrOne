import { Component } from '@angular/core';
import { SideNavItem } from '../models/models';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent {
  sideNavContent: SideNavItem[]=[
    {
      title: 'View Books',
      link: '/books/library'
    },
    {
      title: 'Manage Book',
      link: '/books/maintenance'
    },
    {
      title: 'view users',
      link: 'users/list'
    }

  ];

}
