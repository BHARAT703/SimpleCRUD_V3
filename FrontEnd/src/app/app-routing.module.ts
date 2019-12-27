import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import { UsersComponent } from '../app/users/presentational/user-list/user-list.component';


const routes: Routes = [
  //{ path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  //{ path: 'dashboard', component: DashboardComponent },
  //{ path: 'detail/:id', component: HeroDetailComponent },
  //{ path: 'users', component: UsersComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
