import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RepliesService } from './replies.service';
import { environment } from '@environments/environment';
import { Reply } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('RepliesService', () => {
  let service: RepliesService;
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
        RepliesService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(RepliesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if replie is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if replie is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all replies', () => {
      const mockReplies: Reply[] = [{ replyId: 1, title: "", description: "", imgUrl: "", status: "public", dateReplyCreated: "2024-03-30T10:41:00", dateReplyDeleted: "2024-03-30T10:42:00", dateReplyUpdated: "2024-03-30T10:42:00", isFeatured: true, reactionId: 1, shareId: 1, userId: 1, commentId: 1, postId: 1, attachmentId: 1 }];
      
      service.getAll().subscribe(replies => {
        expect(replies).toEqual(mockReplies);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reply`);
      expect(req.request.method).toBe('GET');
      req.flush(mockReplies);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific reply by id', () => {
      const replyId = 1;
      const mockReplie: Reply = { replyId: 1, title: "", description: "", imgUrl: "", status: "public", dateReplyCreated: "2024-03-30T10:41:00", dateReplyDeleted: "2024-03-30T10:42:00", dateReplyUpdated: "2024-03-30T10:42:00", isFeatured: true, reactionId: 1, shareId: 1, userId: 1, commentId: 1, postId: 1, attachmentId: 1 };
      
      service.getAllById(replyId).subscribe(replie => {
        expect(replie).toEqual(mockReplie);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reply/${replyId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockReplie);
    });
  });

  describe('createReplies', () => {
    it('should make POST request to create a new reply', () => {
      const newReply: Reply = { replyId: 1, title: "", description: "", imgUrl: "", status: "public", dateReplyCreated: "2024-03-30T10:41:00", dateReplyDeleted: "2024-03-30T10:42:00", dateReplyUpdated: "2024-03-30T10:42:00", isFeatured: true, reactionId: 1, shareId: 1, userId: 1, commentId: 1, postId: 1, attachmentId: 1 };
      const createdReply: Reply = { ...newReply };
      
      service.createReplies(newReply).subscribe(reply => {
        expect(reply).toEqual(createdReply);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reply`);
      expect(req.request.method).toBe('POST');
      req.flush(createdReply);
    });
  });

  describe('updateReplies', () => {
    it('should make PUT request to update an existing reply', () => {
      const replyId = 1;
      const updatedReply: Reply = { replyId: replyId, title: "", description: "", imgUrl: "", status: "public", dateReplyCreated: "2024-03-30T10:41:00", dateReplyDeleted: "2024-03-30T10:42:00", dateReplyUpdated: "2024-03-30T10:42:00", isFeatured: true, reactionId: 1, shareId: 1, userId: 1, commentId: 1, postId: 1, attachmentId: 1 };
      
      service.updateReplies(replyId, updatedReply).subscribe(replie => {
        expect(replie).toEqual(updatedReply);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reply/${replyId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedReply);
    });
  });

  describe('deleteReplies', () => {
    it('should make DELETE request to delete a reply by id', () => {
      const replyId = 1;
      
      service.deleteReplies(replyId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reply/${replyId}`);
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

  it('should return an observable with a replie-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
