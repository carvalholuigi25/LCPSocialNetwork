import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Attachment } from '../models';
import { DOCUMENT } from '@angular/common';
import { catchError, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AttachmentsService {
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
        return this.http.get<Attachment[]>(`${environment.apiUrl}/attachment`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllById(id: number) {
        return this.http.get<Attachment>(`${environment.apiUrl}/attachment/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    createAttachment(attachment: Attachment) {
        return this.http.post<Attachment>(`${environment.apiUrl}/attachment`, attachment, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    updateAttachment(id: number, attachment: Attachment) {
        return this.http.put<Attachment>(`${environment.apiUrl}/attachment/${id}`, attachment, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    deleteAttachment(id: number) {
        return this.http.delete<Attachment>(`${environment.apiUrl}/attachment/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
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