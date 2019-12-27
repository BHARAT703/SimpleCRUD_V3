import { Routes } from '@angular/router';
import { UsersComponent } from './presentational/user-list/user-list.component';

export const UserRoutes: Routes = [
    { path: '', redirectTo: '/users', pathMatch: 'full' },
    //{ path: 'dashboard', component: DashboardComponent },
    //{ path: 'detail/:id', component: HeroDetailComponent },
    { path: 'users', component: UsersComponent }
];