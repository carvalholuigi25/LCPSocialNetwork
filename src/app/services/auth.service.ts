import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { User, UserAuth } from '../models';
import { DOCUMENT } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private ls: Storage | undefined;
    private userSubject: BehaviorSubject<User | any | null>;
    public user: Observable<User | null>;

    constructor(
        private router: Router,
        private http: HttpClient,
        @Inject(DOCUMENT) private document: Document
    ) {
        this.ls = this.document.defaultView?.localStorage;
        this.userSubject = new BehaviorSubject(this.ls ? JSON.parse(this.ls.getItem('user')!) : null);
        this.user = this.userSubject.asObservable();
    }

    public get userValue() {
        return this.userSubject.value;
    }

    login(users: UserAuth) {
        return this.http.post<any>(`${environment.apiUrl}/auth/login`, { Username: users.Username, Password: users.Password })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
                this.userSubject.next(user);
                return user;
            }), catchError(this.handleError));
    }

    register(users: User) {
        return this.http.post<any>(`${environment.apiUrl}/users`, users).pipe(catchError(this.handleError));
    }

    logout() {
        // remove user from local storage to log user out
        if(this.ls) {
            this.ls.removeItem('user');
        }

        this.userSubject.next(null);
        this.router.navigate(['/']);
    }

    private handleError(error: HttpErrorResponse) {
        if (error.status === 0) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong.
          console.error(
            `Backend returned code ${error.status}, body was: `, error.error);
        }
        // Return an observable with a user-facing error message.
        return throwError(() => new Error('Something bad happened; please try again later.'));
    }
}