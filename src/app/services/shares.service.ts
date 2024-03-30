import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Share } from '../models';
import { DOCUMENT } from '@angular/common';
import { catchError, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SharesService {
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
        return this.http.get<Share[]>(`${environment.apiUrl}/share`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllById(id: number) {
        return this.http.get<Share>(`${environment.apiUrl}/share/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    createShares(Shares: Share) {
        return this.http.post<Share>(`${environment.apiUrl}/share`, Shares, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    updateShares(id: number, Shares: Share) {
        return this.http.put<Share>(`${environment.apiUrl}/share/${id}`, Shares, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    deleteShares(id: number) {
        return this.http.delete<Share>(`${environment.apiUrl}/share/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
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