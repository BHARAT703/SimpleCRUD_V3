import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Simple CRUD';

  navItems: any[] = [
    { name: 'Overview', route: 'users' }
  ];
}
