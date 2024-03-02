import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Attachments } from '../models';

@Injectable({ providedIn: 'root' })
export class AttachmentsService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Attachments[]>(`${environment.apiUrl}/attachments`);
    }

    getAllById(id: number) {
        return this.http.get<Attachments>(`${environment.apiUrl}/attachments/${id}`);
    }

    createAttachment(attachments: Attachments) {
        return this.http.post<Attachments>(`${environment.apiUrl}/attachments`, attachments);
    }

    updateAttachment(id: number, attachments: Attachments) {
        return this.http.put<Attachments>(`${environment.apiUrl}/attachments/${id}`, attachments);
    }

    deleteAttachment(id: number) {
        return this.http.delete<Attachments>(`${environment.apiUrl}/attachments/${id}`);
    }
}