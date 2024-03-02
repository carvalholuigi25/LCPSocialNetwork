import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Friends } from '../models';

@Injectable({ providedIn: 'root' })
export class FriendsService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Friends[]>(`${environment.apiUrl}/friends`);
    }

    getAllById(id: number) {
        return this.http.get<Friends>(`${environment.apiUrl}/friends/${id}`);
    }

    createFriend(friends: Friends) {
        return this.http.post<Friends>(`${environment.apiUrl}/friends`, friends);
    }

    updateFriend(id: number, friends: Friends) {
        return this.http.put<Friends>(`${environment.apiUrl}/friends/${id}`, friends);
    }

    deleteFriend(id: number) {
        return this.http.delete<Friends>(`${environment.apiUrl}/friends/${id}`);
    }
}