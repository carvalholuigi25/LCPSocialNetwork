import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Post, QueryParams, User } from '../models';
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

    getAllWithUsers(userId: number = -1, postId: number = -1) {
        let uid = userId !== -1 ? `/${userId}` : '';
        let pid = postId !== -1 ? `/user/${postId}`: '';
        let user = this.http.get<User[]>(`${environment.apiUrl}/user${uid}`, { headers: this.setHeadersObj() });
        let post = this.http.get<Post[]>(`${environment.apiUrl}/post${pid}`, { headers: this.setHeadersObj() });
        return forkJoin([user, post]);
    }

    getAllById(id: number) {
        return this.http.get<Post>(`${environment.apiUrl}/post/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllByUsersId(userId: number) {
        return this.http.get<Post>(`${environment.apiUrl}/post/user/${userId}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getLastId() {
        return this.http.get<Post>(`${environment.apiUrl}/post/lastid`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
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

    searchPosts(qryp: QueryParams) {
        let qparams = new HttpParams().appendAll({
            "Page": qryp.page,
            "PageSize": qryp.pageSize,
            "SortOrder": qryp.sortOrder,
            "SortBy": qryp.sortBy,
            "Search": qryp.search,
            "Operator": qryp.operator
        });

        return this.http.get<Post[]>(`${environment.apiUrl}/post/filter`, { headers: this.setHeadersObj(), params: qparams }).pipe(catchError(this.handleError));
    }

    /* istanbul ignore next */
    handleError(error: HttpErrorResponse) {
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