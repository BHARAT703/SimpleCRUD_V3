import { Component, EventEmitter, Input, Output, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { UserService } from '../users.service';
import * as _ from "lodash";

@Component({
    selector: 'user-form-component',
    templateUrl: 'user-form-component.html',
    styleUrls: ['./user-form-component.css']
})
export class UserFormComponent {

    formGroup: FormGroup;
    titleAlert: string = 'This field is required';
    post: any = '';
    reference = [];

    constructor(
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<UserFormComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private userService: UserService) {
        dialogRef.disableClose = true;
    }

    ngOnInit() {
        this.createForm();

        if (this.data.action == 'update')
            this.setInitialValue();

        this.createReference();
    }

    // Creates a reference of form's initial value
    createReference() {
        for (let prop in this.formGroup.controls) {
            var item = {};
            item[prop.toString()] = this.formGroup.controls[prop].value;
            this.reference.push(item)
        }
    }

    // Returns true if the user has changed the value in the given property of form
    isDifferent(prop: string, value: string) {
        var item = _.find(this.reference, prop);
        if (item && value && (item[prop] != value))
            return true;
        else
            return false;
    }

    getFormState() {
        let hasChanges = false;
        for (let prop in this.formGroup.controls) {
            if (this.isDifferent(prop, this.formGroup.controls[prop].value)) {
                hasChanges = true;
            }
        }

        let isFormValid = this.formGroup.valid;
        let isFormDirty = this.formGroup.dirty;
        return this.data.action == 'add' ? !isFormValid : !(isFormValid && isFormDirty && hasChanges);
    }

    createForm() {
        let emailregex: RegExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        this.formGroup = this.formBuilder.group({
            'name': [null, Validators.required],
            'email': [null, [Validators.required, Validators.pattern(emailregex)]]
        });
    }

    setInitialValue() {
        this.formGroup.controls['name'].setValue(this.data.name);
        this.formGroup.controls['email'].setValue(this.data.email);
    }

    get name() {
        return this.formGroup.get('name') as FormControl
    }

    getErrorEmail() {
        return this.formGroup.get('email').hasError('required') ? 'Field is required' :
            this.formGroup.get('email').hasError('pattern') ? 'Not a valid emailaddress' :
                this.formGroup.get('email').hasError('alreadyInUse') ? 'This emailaddress is already in use' : '';
    }

    onSubmit(data) {
        if (this.data.action == 'add') {
            this.userService.add(data).pipe(tap(() => console.log('User added Successfully.')))
                .subscribe(m => {
                    this.dialogRef.close(m);
                });
        }
        else if (this.data.action == 'update') {
            data.id = this.data.id;
            this.userService.update(data).pipe(tap(() => console.log('User updated Successfully.')))
                .subscribe(m => {
                    this.dialogRef.close(m);
                });
        }
    }

    onNoClick(): void {
        this.dialogRef.close(false);
    }

    onYesClick(): void {
        this.userService.delete(this.data.id).pipe(tap(() => console.log('User Deleted Successfully.')))
            .subscribe(() => {
                this.dialogRef.close(true);
            });
    }
}