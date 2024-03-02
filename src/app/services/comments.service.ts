import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Comments } from '../models';

@Injectable({ providedIn: 'root' })
export class CommentsService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Comments[]>(`${environment.apiUrl}/comments`);
    }

    getAllById(id: number) {
        return this.http.get<Comments>(`${environment.apiUrl}/comments/${id}`);
    }

    createComment(comments: Comments) {
        return this.http.post<Comments>(`${environment.apiUrl}/comments`, comments);
    }

    updateComment(id: number, comments: Comments) {
        return this.http.put<Comments>(`${environment.apiUrl}/comments/${id}`, comments);
    }

    deleteComment(id: number) {
        return this.http.delete<Comments>(`${environment.apiUrl}/comments/${id}`);
    }
}