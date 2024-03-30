import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CommentService } from './comments.service';
import { environment } from '@environments/environment';
import { Comment } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('CommentService', () => {
  let service: CommentService;
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
        CommentService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(CommentService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if comment is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if comment is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all comments', () => {
      const mockComments: Comment[] = [{ commentId: 1, title: "", description: "", status: "", imgUrl: "", dateCommentCreated: "", dateCommentDeleted: "", dateCommentUpdated: "", isFeatured: true, userId: 1, postId: 1, replyId: 1, reactionId: 1, shareId: 1, attachmentId: 1 }];
      
      service.getAll().subscribe(comments => {
        expect(comments).toEqual(mockComments);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/comment`);
      expect(req.request.method).toBe('GET');
      req.flush(mockComments);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific comment by id', () => {
      const commentId = 1;
      const mockComment: Comment = { commentId: 1, title: "", description: "", status: "", imgUrl: "", dateCommentCreated: "", dateCommentDeleted: "", dateCommentUpdated: "", isFeatured: true, userId: 1, postId: 1, replyId: 1, reactionId: 1, shareId: 1, attachmentId: 1 };
      
      service.getAllById(commentId).subscribe(comment => {
        expect(comment).toEqual(mockComment);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/comment/${commentId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockComment);
    });
  });

  describe('createComments', () => {
    it('should make POST request to create a new comment', () => {
      const newComment: Comment = { commentId: 1, title: "", description: "", status: "", imgUrl: "", dateCommentCreated: "", dateCommentDeleted: "", dateCommentUpdated: "", isFeatured: true, userId: 1, postId: 1, replyId: 1, reactionId: 1, shareId: 1, attachmentId: 1 };
      const createdComment: Comment = { ...newComment };
      
      service.createComments(newComment).subscribe(comment => {
        expect(comment).toEqual(createdComment);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/comment`);
      expect(req.request.method).toBe('POST');
      req.flush(createdComment);
    });
  });

  describe('updateComments', () => {
    it('should make PUT request to update an existing comment', () => {
      const commentId = 1;
      const updatedComment: Comment = { commentId: 1, title: "", description: "", status: "", imgUrl: "", dateCommentCreated: "", dateCommentDeleted: "", dateCommentUpdated: "", isFeatured: true, userId: 1, postId: 1, replyId: 1, reactionId: 1, shareId: 1, attachmentId: 1 };
      
      service.updateComments(commentId, updatedComment).subscribe(comment => {
        expect(comment).toEqual(updatedComment);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/comment/${commentId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedComment);
    });
  });

  describe('deleteComments', () => {
    it('should make DELETE request to delete a comment by id', () => {
      const commentId = 1;
      
      service.deleteComments(commentId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/comment/${commentId}`);
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

  it('should return an observable with a comment-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
