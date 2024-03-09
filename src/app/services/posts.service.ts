import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Posts, Users } from '../models';
import { DOCUMENT } from '@angular/common';
import { catchError, forkJoin, throwError } from 'rxjs';

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
            "Authorization": this.ls ? `Bearer ${this.ls!.getItem('user')! ? JSON.parse(this.ls!.getItem('user')!).token : null}` : ""
        });
    }

    getAll() {
        return this.http.get<Posts[]>(`${environment.apiUrl}/post`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllWithUsers() {
        let posts = this.http.get<Posts[]>(`${environment.apiUrl}/post`, { headers: this.setHeadersObj() });
        let users = this.http.get<Users[]>(`${environment.apiUrl}/user`, { headers: this.setHeadersObj() });
        return forkJoin([posts, users]);
    }

    getAllById(id: number) {
        return this.http.get<Posts>(`${environment.apiUrl}/post/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    createPost(posts: Posts) {
        return this.http.post<Posts>(`${environment.apiUrl}/post`, posts, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    updatePost(id: number, posts: Posts) {
        return this.http.put<Posts>(`${environment.apiUrl}/post/${id}`, posts, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    deletePost(id: number) {
        return this.http.delete<Posts>(`${environment.apiUrl}/post/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    private handleError(error: HttpErrorResponse) {
        if (error.status === 0) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong.
          console.error(
            `Backend returned code ${error.status}, body was: `, error.error);
        }
        // Return an observable with a user-facing error message.
        return throwError(() => new Error('Something bad happened; please try again later.'));
    }
}