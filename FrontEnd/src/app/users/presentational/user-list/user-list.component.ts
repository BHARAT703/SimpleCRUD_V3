import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { IUser } from '../../user';
import { UserService } from '../../users.service';

@Component({
    selector: 'app-users',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css']
})
export class UsersComponent implements OnInit {

    users: IUser[];
    displayedColumns: string[] = ['id', 'name', 'email'];    

    constructor(private userService: UserService) { }

    ngOnInit() {
        this.getUsers();
    }

    getUsers(): void {
        this.userService.getAll().subscribe(m => this.users = m);
    }
}