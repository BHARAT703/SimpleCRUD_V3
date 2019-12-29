import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { HttpBaseService } from '../shared/http-base.service';
import { IUser, User } from './user';

@Injectable({ providedIn: 'root' })
export class UserService {
    private backendUrl = 'http://localhost:54203/api/users';
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };

    constructor(private readonly httpBase: HttpBaseService) { }

    getAll(): Observable<IUser[]> {
        return this.httpBase.get<IUser[]>(this.backendUrl).pipe(tap(_ => this.log(`fetched users`)), catchError(this.handleError<IUser[]>('')));
    }

    delete(user: IUser | number): Observable<IUser> {
        const id = typeof user === 'number' ? user : user.id;
        const url = `${this.backendUrl}/SoftDelete?id=${id}`;
    
        return this.httpBase.delete(url);
      }

    /**
* Handle Http operation that failed.
* Let the app continue.
* @param operation - name of the operation that failed
* @param result - optional value to return as the observable result
*/
    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

    private log(message: string) {
        console.log(`UserService: ${message}`);
    }
}