import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { allPresentationalComponents } from './presentational';
import { UserRoutes } from './users.routing';

@NgModule({
    imports: [
        HttpClientModule,
        CommonModule,
        SharedModule,
        ReactiveFormsModule,
        FormsModule,
        RouterModule.forChild(UserRoutes),
    ],
    declarations: [...allPresentationalComponents],
})
export class UsersModule { }