import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Attachments } from '../models';
import { DOCUMENT } from '@angular/common';

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
            "Authorization": this.ls ? `Bearer ${JSON.parse(this.ls!.getItem('user')!).token}` : ""
        });
    }

    getAll() {
        return this.http.get<Attachments[]>(`${environment.apiUrl}/attachments`, { headers: this.setHeadersObj() });
    }

    getAllById(id: number) {
        return this.http.get<Attachments>(`${environment.apiUrl}/attachments/${id}`, { headers: this.setHeadersObj() });
    }

    createAttachment(attachments: Attachments) {
        return this.http.post<Attachments>(`${environment.apiUrl}/attachments`, attachments, { headers: this.setHeadersObj() });
    }

    updateAttachment(id: number, attachments: Attachments) {
        return this.http.put<Attachments>(`${environment.apiUrl}/attachments/${id}`, attachments, { headers: this.setHeadersObj() });
    }

    deleteAttachment(id: number) {
        return this.http.delete<Attachments>(`${environment.apiUrl}/attachments/${id}`, { headers: this.setHeadersObj() });
    }
}