import { Component, EventEmitter, Input, Output, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { UserService } from '../users.service';
import { IUser } from '../user';

@Component({
    selector: 'user-list-delete-confirmation',
    templateUrl: 'user-list-delete-confirmation.html',
})
export class UserDeleteConfirmation {

    constructor(
        public dialogRef: MatDialogRef<UserDeleteConfirmation>,
        @Inject(MAT_DIALOG_DATA) public id: number,
        private userService: UserService) {        
    }

    onNoClick(): void {
        this.dialogRef.close(false);
    }

    onYesClick(): void {
        this.userService.delete(this.id).pipe(tap(() => console.log('User Deleted Successfully.')))
        .subscribe(() => {
            this.dialogRef.close(true);
        });        
    }
}