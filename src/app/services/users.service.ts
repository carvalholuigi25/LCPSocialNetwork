import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Users } from '../models';

@Injectable({ providedIn: 'root' })
export class UsersService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Users[]>(`${environment.apiUrl}/users`);
    }

    getAllById(id: number) {
        return this.http.get<Users>(`${environment.apiUrl}/users/${id}`);
    }

    createUser(users: Users) {
        return this.http.post<Users>(`${environment.apiUrl}/users`, users);
    }

    updateUser(id: number, users: Users) {
        return this.http.put<Users>(`${environment.apiUrl}/users/${id}`, users);
    }

    deleteUser(id: number) {
        return this.http.delete<Users>(`${environment.apiUrl}/users/${id}`);
    }
}