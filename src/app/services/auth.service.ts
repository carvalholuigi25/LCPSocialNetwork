import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Users, UsersAuth } from '../models';
import { DOCUMENT } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private ls: Storage | undefined;
    private userSubject: BehaviorSubject<Users | any | null>;
    public user: Observable<Users | null>;

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

    login(users: UsersAuth) {
        return this.http.post<any>(`${environment.apiUrl}/auth/login`, { Username: users.Username, Password: users.Password })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
                this.userSubject.next(user);
                return user;
            }));
    }

    register(users: Users) {
        return this.http.post<any>(`${environment.apiUrl}/users`, users);
    }

    logout() {
        // remove user from local storage to log user out
        if(this.ls) {
            this.ls.removeItem('user');
        }

        this.userSubject.next(null);
        this.router.navigate(['/']);
    }
}