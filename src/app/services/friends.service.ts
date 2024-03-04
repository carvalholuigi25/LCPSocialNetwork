import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Friends } from '../models';
import { DOCUMENT } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class FriendsService {
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
        return this.http.get<Friends[]>(`${environment.apiUrl}/friends`, { headers: this.setHeadersObj() });
    }

    getAllById(id: number) {
        return this.http.get<Friends>(`${environment.apiUrl}/friends/${id}`, { headers: this.setHeadersObj() });
    }

    createFriend(friends: Friends) {
        return this.http.post<Friends>(`${environment.apiUrl}/friends`, friends, { headers: this.setHeadersObj() });
    }

    updateFriend(id: number, friends: Friends) {
        return this.http.put<Friends>(`${environment.apiUrl}/friends/${id}`, friends, { headers: this.setHeadersObj() });
    }

    deleteFriend(id: number) {
        return this.http.delete<Friends>(`${environment.apiUrl}/friends/${id}`, { headers: this.setHeadersObj() });
    }
}