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
}