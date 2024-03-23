import { DOCUMENT } from '@angular/common';
import { HttpHeaders, HttpErrorResponse, HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Languages } from '@app/models';
import { environment } from '@environments/environment';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguagesService {
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

  getListLanguages() {
    return this.http.get<Languages[]>(`${environment.apiUrl}/language`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
  }

  getLanguage() {
    return !!this.ls ? this.ls!.getItem("mylanguage") : "en";
  }

  setLanguage(currentLanguage: string = "en") {
    if(!!this.ls) {
      const languageval = currentLanguage;
      this.ls!.setItem("mylanguage", languageval);
    }
  }

  removeLanguage() {
    if(!!this.ls) {
      this.ls!.removeItem("mylanguage");
    }
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
