import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { ChatMessage } from '../models';
import { DOCUMENT } from '@angular/common';
import { catchError, throwError } from 'rxjs';
import * as signalR from '@microsoft/signalr';

@Injectable({ providedIn: 'root' })
export class ChatMessagesService {
    private hubConnection!: signalR.HubConnection;

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
        return this.http.get<ChatMessage[]>(`${environment.apiUrl}/chatmessage`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getAllById(id: number) {
        return this.http.get<ChatMessage>(`${environment.apiUrl}/chatmessage/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    getCount() {
        return this.http.get<number>(`${environment.apiUrl}/chatmessage/count`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    createChatMessages(ChatMessages: ChatMessage) {
        return this.http.post<ChatMessage>(`${environment.apiUrl}/chatmessage`, ChatMessages, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    updateChatMessages(id: number, ChatMessages: ChatMessage) {
        return this.http.put<ChatMessage>(`${environment.apiUrl}/chatmessage/${id}`, ChatMessages, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    deleteChatMessages(id: number) {
        return this.http.delete<ChatMessage>(`${environment.apiUrl}/chatmessage/${id}`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    deleteAllChatMessages() {
        return this.http.delete<ChatMessage>(`${environment.apiUrl}/chatmessage/all`, { headers: this.setHeadersObj() }).pipe(catchError(this.handleError));
    }

    loadSignalRStuff() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .configureLogging(environment.production ? signalR.LogLevel.Information : signalR.LogLevel.Debug)
            .withUrl('http://localhost:5001/chathub')
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start().then(function () {  
            console.log('ChatHub Connected!');
        }).catch(function (err) {  
            return console.error(err.toString());  
        });

        this.hubConnection.on("SendMessage", () => {
            this.getAll();
        });

        this.hubConnection.onreconnected(() => {
            this.getAll();
        });
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