import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Posts, Users } from '../models';
import { DOCUMENT } from '@angular/common';
import { forkJoin } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PostsService {
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
        return this.http.get<Posts[]>(`${environment.apiUrl}/post`, { headers: this.setHeadersObj() });
    }

    getAllWithUsers() {
        let posts = this.http.get<Posts[]>(`${environment.apiUrl}/post`, { headers: this.setHeadersObj() });
        let users = this.http.get<Users[]>(`${environment.apiUrl}/user`, { headers: this.setHeadersObj() });
        return forkJoin([posts, users]);
    }

    getAllById(id: number) {
        return this.http.get<Posts>(`${environment.apiUrl}/post/${id}`, { headers: this.setHeadersObj() });
    }

    createPost(posts: Posts) {
        return this.http.post<Posts>(`${environment.apiUrl}/post`, posts, { headers: this.setHeadersObj() });
    }

    updatePost(id: number, posts: Posts) {
        return this.http.put<Posts>(`${environment.apiUrl}/post/${id}`, posts, { headers: this.setHeadersObj() });
    }

    deletePost(id: number) {
        return this.http.delete<Posts>(`${environment.apiUrl}/post/${id}`, { headers: this.setHeadersObj() });
    }
}