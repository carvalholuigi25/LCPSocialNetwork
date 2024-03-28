import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Post, User } from '../models';
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
        return this.http.get<Post[]>(`${environment.apiUrl}/post`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllWithUsers(userId: number) {
        let pid = userId !== -1 ? `/user/${userId}`: '';
        let post = this.http.get<Post[]>(`${environment.apiUrl}/post${pid}`, { headers: this.setHeadersObj() });
        let user = this.http.get<User[]>(`${environment.apiUrl}/user`, { headers: this.setHeadersObj() });
        return forkJoin([post, user]);
    }

    getAllById(id: number) {
        return this.http.get<Post>(`${environment.apiUrl}/post/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllByUsersId(userId: number) {
        return this.http.get<Post>(`${environment.apiUrl}/post/user/${userId}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    createPosts(post: Post) {
        return this.http.post<Post>(`${environment.apiUrl}/post`, post, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    updatePosts(id: number, post: Post) {
        return this.http.put<Post>(`${environment.apiUrl}/post/${id}`, post, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    deletePosts(id: number) {
        return this.http.delete<Post>(`${environment.apiUrl}/post/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    /* istanbul ignore next */
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