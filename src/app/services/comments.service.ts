import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Comments } from '../models';
import { DOCUMENT } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class CommentsService {
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
        return this.http.get<Comments[]>(`${environment.apiUrl}/comments`, { headers: this.setHeadersObj() });
    }

    getAllById(id: number) {
        return this.http.get<Comments>(`${environment.apiUrl}/comments/${id}`, { headers: this.setHeadersObj() });
    }

    createComment(comments: Comments) {
        return this.http.post<Comments>(`${environment.apiUrl}/comments`, comments, { headers: this.setHeadersObj() });
    }

    updateComment(id: number, comments: Comments) {
        return this.http.put<Comments>(`${environment.apiUrl}/comments/${id}`, comments, { headers: this.setHeadersObj() });
    }

    deleteComment(id: number) {
        return this.http.delete<Comments>(`${environment.apiUrl}/comments/${id}`, { headers: this.setHeadersObj() });
    }
}