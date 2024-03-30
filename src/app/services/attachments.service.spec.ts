import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AttachmentsService } from './attachments.service';
import { environment } from '@environments/environment';
import { Attachment } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('AttachmentsService', () => {
  let service: AttachmentsService;
  let httpMock: HttpTestingController;
  let documentMock: any;

  beforeEach(() => {
    documentMock = {
      defaultView: {
        localStorage: {
          getItem: jest.fn()
        }
      }
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AttachmentsService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(AttachmentsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if attachment is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if attachment is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all attachments', () => {
      const mockAttachments: Attachment[] = [{ attachmentId: 1, title: "", description: "", status: "", attachmentType: "", attachmentUrl: "", dateAttachmentDeleted: "", dateAttachmentUpdated: "", dateAttachmentUploaded: "", isFeatured: false, postId: 1, commentId: 1, reactionId: 1, replyId: 1, shareId: 1, userId: 1 }];
      
      service.getAll().subscribe(attachments => {
        expect(attachments).toEqual(mockAttachments);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/attachment`);
      expect(req.request.method).toBe('GET');
      req.flush(mockAttachments);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific attachment by id', () => {
      const attachmentId = 1;
      const mockAttachment: Attachment = { attachmentId: attachmentId, title: "", description: "", status: "", attachmentType: "", attachmentUrl: "", dateAttachmentDeleted: "", dateAttachmentUpdated: "", dateAttachmentUploaded: "", isFeatured: false, postId: 1, commentId: 1, reactionId: 1, replyId: 1, shareId: 1, userId: 1 };
      
      service.getAllById(attachmentId).subscribe(attachment => {
        expect(attachment).toEqual(mockAttachment);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/attachment/${attachmentId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockAttachment);
    });
  });

  describe('createAttachments', () => {
    it('should make POST request to create a new attachment', () => {
      const newAttachment: Attachment = { attachmentId: 1, title: "", description: "", status: "", attachmentType: "", attachmentUrl: "", dateAttachmentDeleted: "", dateAttachmentUpdated: "", dateAttachmentUploaded: "", isFeatured: false, postId: 1, commentId: 1, reactionId: 1, replyId: 1, shareId: 1, userId: 1 };
      const createdAttachment: Attachment = { ...newAttachment };
      
      service.createAttachment(newAttachment).subscribe(attachment => {
        expect(attachment).toEqual(createdAttachment);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/attachment`);
      expect(req.request.method).toBe('POST');
      req.flush(createdAttachment);
    });
  });

  describe('updateAttachments', () => {
    it('should make PUT request to update an existing attachment', () => {
      const attachmentId = 1;
      const updatedAttachment: Attachment = { attachmentId: attachmentId, title: "", description: "", status: "", attachmentType: "", attachmentUrl: "", dateAttachmentDeleted: "", dateAttachmentUpdated: "", dateAttachmentUploaded: "", isFeatured: false, postId: 1, commentId: 1, reactionId: 1, replyId: 1, shareId: 1, userId: 1 };
      
      service.updateAttachment(attachmentId, updatedAttachment).subscribe(attachment => {
        expect(attachment).toEqual(updatedAttachment);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/attachment/${attachmentId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedAttachment);
    });
  });

  describe('deleteAttachments', () => {
    it('should make DELETE request to delete a attachment by id', () => {
      const attachmentId = 1;
      
      service.deleteAttachment(attachmentId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/attachment/${attachmentId}`);
      expect(req.request.method).toBe('DELETE');
      req.flush({});
    });
  });

  it('should handle client-side or network error correctly', () => {
    const errorResponse = new HttpErrorResponse({ status: 0, statusText: 'error' });
    const consoleErrorSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(expect.any(Error));
    expect(consoleErrorSpy).toHaveBeenCalledWith('An error occurred:', errorResponse.error);
    consoleErrorSpy.mockRestore();
  });

  it('should handle backend error correctly', () => {
    const errorResponse = new HttpErrorResponse({ status: 500, statusText: 'Internal Server Error', error: 'Server Error' });
    const consoleErrorSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(expect.any(Error));
    expect(consoleErrorSpy).toHaveBeenCalledWith(`Backend returned code ${errorResponse.status}, body was: `, errorResponse.error);
    consoleErrorSpy.mockRestore();
  });

  it('should return an observable with a attachment-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
