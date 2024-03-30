import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ReactionsService } from './reactions.service';
import { environment } from '@environments/environment';
import { Reaction, ReactionTypeEnum } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('ReactionsService', () => {
  let service: ReactionsService;
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
        ReactionsService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(ReactionsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if reaction is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if reaction is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all reactions', () => {
      const mockReactions: Reaction[] = [{ reactionId: 1, reactionType: ReactionTypeEnum.like, reactionCounter: 0, dateReacted: "2024-03-30T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 }];
      
      service.getAll().subscribe(reactions => {
        expect(reactions).toEqual(mockReactions);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reaction`);
      expect(req.request.method).toBe('GET');
      req.flush(mockReactions);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific reaction by id', () => {
      const reactionId = 1;
      const mockReaction: Reaction = { reactionId: 1, reactionType: ReactionTypeEnum.like, reactionCounter: 0, dateReacted: "2024-03-30T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 };
      
      service.getAllById(reactionId).subscribe(reaction => {
        expect(reaction).toEqual(mockReaction);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reaction/${reactionId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockReaction);
    });
  });

  describe('createReactions', () => {
    it('should make POST request to create a new reaction', () => {
      const newReaction: Reaction = { reactionId: 1, reactionType: ReactionTypeEnum.like, reactionCounter: 0, dateReacted: "2024-03-30T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 };
      const createdReaction: Reaction = { ...newReaction };
      
      service.createReactions(newReaction).subscribe(reaction => {
        expect(reaction).toEqual(createdReaction);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reaction`);
      expect(req.request.method).toBe('POST');
      req.flush(createdReaction);
    });
  });

  describe('updateReactions', () => {
    it('should make PUT request to update an existing reaction', () => {
      const reactionId = 1;
      const updatedReaction: Reaction = { reactionId: reactionId, reactionType: ReactionTypeEnum.like, reactionCounter: 0, dateReacted: "2024-03-30T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 };
      
      service.updateReactions(reactionId, updatedReaction).subscribe(reaction => {
        expect(reaction).toEqual(updatedReaction);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reaction/${reactionId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedReaction);
    });
  });

  describe('deleteReactions', () => {
    it('should make DELETE request to delete a reaction by id', () => {
      const reactionId = 1;
      
      service.deleteReactions(reactionId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/reaction/${reactionId}`);
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

  it('should return an observable with a reaction-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
