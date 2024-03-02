import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Posts } from '../models';

@Injectable({ providedIn: 'root' })
export class PostsService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Posts[]>(`${environment.apiUrl}/posts`);
    }

    getAllById(id: number) {
        return this.http.get<Posts>(`${environment.apiUrl}/posts/${id}`);
    }

    createPost(posts: Posts) {
        return this.http.post<Posts>(`${environment.apiUrl}/posts`, posts);
    }

    updatePost(id: number, posts: Posts) {
        return this.http.put<Posts>(`${environment.apiUrl}/posts/${id}`, posts);
    }

    deletePost(id: number) {
        return this.http.delete<Posts>(`${environment.apiUrl}/posts/${id}`);
    }
}