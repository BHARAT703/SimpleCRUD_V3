import { Component, Input, Output, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, Validators } from '@angular/forms';
import * as _ from "lodash";

import { IUser } from '../user';
import { UserService } from '../users.service';
import { UserFormComponent } from '../user-form/user-form-component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';


import { range } from 'rxjs';
import { map, filter, scan } from 'rxjs/operators'

@Component({
    selector: 'app-users',
    templateUrl: './user-list.component.html',
    styleUrls: ['./user-list.component.css']
})
export class UsersComponent implements OnInit {

    users: IUser[];
    dataSource = new MatTableDataSource<IUser>([]);
    displayedColumns: string[] = ['id', 'name', 'email', 'star'];

    constructor(private userService: UserService, public deleteConfirmation: MatDialog, private _snackBar: MatSnackBar, private changeDetectorRefs: ChangeDetectorRef) { }

    ngOnInit() {
        // const source$ = range(0, 10);

        // source$.pipe(
        //     filter(x => x % 2 === 0), //filter out non-even numbers and returns 0, 2, 4, 6, 8          
        //     map(x => x + x),
        //     scan((acc, x) => acc + x, 0)
        // )
        //     .subscribe(x => console.log(x))

        this.getUsers();
    }

    getUsers(): void {
        this.userService.getAll().subscribe(m => {
            this.dataSource.data = m;
            this.users = m;
        });
    }

    openDialog(user: IUser, action: string): void {
        const data = {
            action: action,
            id: user && user.id ? user.id : 0,
            name: user && user.name ? user.name : '',
            email: user && user.email ? user.email : ''
        }

        if (action === 'remove') {
            data.id = user.id;
        }

        const dialogRef = this.deleteConfirmation.open(UserFormComponent, { width: '500px', data: data });

        dialogRef.afterClosed().subscribe(m => {
            if (m && action === 'remove') {
                _.remove(this.users, m => m.id === data.id);
                this.dataSource.data = this.users;
                this._snackBar.open('User Removed Successfully..', '', {
                    duration: 2000
                })
            }
            else if (action === 'add' && m.id) {
                this.users.push(m);
                this.dataSource.data = this.users;
                this._snackBar.open('User Added Successfully..', '', {
                    duration: 2000
                })
            }
            else if (action === 'update' && m.id) {
                var index = _.findIndex(this.users, m.id);
                this.users.splice(index, 1, m);
                this.dataSource.data = this.users;
                this._snackBar.open('User Updated Successfully..', '', {
                    duration: 2000
                })
            }
        });
    }
}
