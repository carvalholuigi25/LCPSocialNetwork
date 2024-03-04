import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Users } from '../models';
import { DOCUMENT } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class UsersService {
    private ls: Storage | undefined;

    constructor(
        private http: HttpClient,
        @Inject(DOCUMENT) private document: Document
    ) { 
        this.ls = this.document.defaultView?.localStorage;
    }

    setHeadersObj() {
        return new HttpHeaders({
            "Content-Type": "application/json",
            "Authorization": this.ls ? `Bearer ${JSON.parse(this.ls!.getItem('user')!).token}` : ""
        });
    }

    getAll() {
        return this.http.get<Users[]>(`${environment.apiUrl}/user`, { headers: this.setHeadersObj() });
    }

    getAllById(id: number) {
        return this.http.get<Users>(`${environment.apiUrl}/user/${id}`, { headers: this.setHeadersObj() });
    }

    createUser(users: Users) {
        return this.http.post<Users>(`${environment.apiUrl}/user`, users, { headers: this.setHeadersObj() });
    }

    updateUser(id: number, users: Users) {
        return this.http.put<Users>(`${environment.apiUrl}/user/${id}`, users, { headers: this.setHeadersObj() });
    }

    deleteUser(id: number) {
        return this.http.delete<Users>(`${environment.apiUrl}/user/${id}`, { headers: this.setHeadersObj() });
    }
}