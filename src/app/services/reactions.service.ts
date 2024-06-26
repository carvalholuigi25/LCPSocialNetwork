import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '@environments/environment';
import { FilterOperatorEnum, QueryParams, Reaction, User } from '../models';
import { DOCUMENT } from '@angular/common';
import { catchError, forkJoin, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ReactionsService {
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

    getDataLocal() {
        return this.http.get<any>('../assets/data/reactions.json').pipe(catchError(this.handleError));
    }

    getAll() {
        return this.http.get<Reaction[]>(`${environment.apiUrl}/reaction`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllById(id: number) {
        return this.http.get<Reaction>(`${environment.apiUrl}/reaction/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllWithUsers() {
        let reaction = this.http.get<Reaction[]>(`${environment.apiUrl}/reaction`, { headers: this.setHeadersObj() });
        let user = this.http.get<User[]>(`${environment.apiUrl}/user`, { headers: this.setHeadersObj() });
        return forkJoin([reaction, user]);
    }

    getCount() {
        return this.http.get<number>(`${environment.apiUrl}/reaction/count`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getCountByPostId(postId: number) {
        return this.http.get<number>(`${environment.apiUrl}/reaction/count/${postId}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    createReactions(Reaction: Reaction) {
        return this.http.post<Reaction>(`${environment.apiUrl}/reaction`, Reaction, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    updateReactions(id: number, Reaction: Reaction) {
        return this.http.put<Reaction>(`${environment.apiUrl}/reaction/${id}`, Reaction, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    searchReactions(qryp: QueryParams) {
        let qparams = new HttpParams().appendAll({
            "Page": qryp.page ?? 1,
            "PageSize": qryp.pageSize ?? 30,
            "SortOrder": qryp.sortOrder ?? "asc",
            "SortBy": qryp.sortBy ?? "ReactionType",
            "Search": qryp.search,
            "Operator": qryp.operator ?? FilterOperatorEnum.Equals
        });
        
        return this.http.get<Reaction[]>(`${environment.apiUrl}/reaction/filter`, { headers: this.setHeadersObj(), params: qparams }).pipe(catchError(this.handleError));
    }

    deleteReactions(id: number) {
        return this.http.delete<Reaction>(`${environment.apiUrl}/reaction/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
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