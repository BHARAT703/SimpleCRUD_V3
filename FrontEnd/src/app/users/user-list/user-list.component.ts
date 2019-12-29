import { Component, Input, Output, OnInit, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { IUser } from '../user';
import { UserService } from '../users.service';
import { UserDeleteConfirmation } from './user-list-delete-confirmation';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-users',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css']
})
export class UsersComponent implements OnInit {

    users: IUser[];
    displayedColumns: string[] = ['id', 'name', 'email', 'star'];

    constructor(private userService: UserService, public deleteConfirmation: MatDialog, private _snackBar: MatSnackBar) { }

    ngOnInit() {
        this.getUsers();
    }

    getUsers(): void {
        this.userService.getAll().subscribe(m => this.users = m);
    }

    openDialog(deleteUser: IUser): void {
        const dialogRef = this.deleteConfirmation.open(UserDeleteConfirmation, { width: '500px', data: { id: deleteUser.id } });

        dialogRef.afterClosed().subscribe(m => {
            if (m) {
                this._snackBar.open('User Removed Successfully..', '', {
                    duration: 2000
                })
            }
        });
    }
}
