import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SharesService } from './shares.service';
import { environment } from '@environments/environment';
import { Share } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('SharesService', () => {
  let service: SharesService;
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
        SharesService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(SharesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if share is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if share is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all shares', () => {
      const mockShares: Share[] = [{ shareId: 1, shareCounter: 0, dateShared: "2024-03-30T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 }];
      
      service.getAll().subscribe(shares => {
        expect(shares).toEqual(mockShares);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/share`);
      expect(req.request.method).toBe('GET');
      req.flush(mockShares);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific share by id', () => {
      const shareId = 1;
      const mockShare: Share = { shareId: shareId, shareCounter: 0, dateShared: "2024-03-30T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 };
      
      service.getAllById(shareId).subscribe(share => {
        expect(share).toEqual(mockShare);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/share/${shareId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockShare);
    });
  });

  describe('createShare', () => {
    it('should make POST request to create a new share', () => {
      const newShare: Share = { shareId: 1, shareCounter: 0, dateShared: "2024-03-30T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 };
      const createdShare: Share = { ...newShare };
      
      service.createShares(newShare).subscribe(share => {
        expect(share).toEqual(createdShare);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/share`);
      expect(req.request.method).toBe('POST');
      req.flush(createdShare);
    });
  });

  describe('updateShare', () => {
    it('should make PUT request to update an existing share', () => {
      const shareId = 1;
      const updatedShare: Share = { shareId: shareId, shareCounter: 1, dateShared: "2024-03-31T10:35:00", attachmentId: 1, postId: 1, commentId: 1, userId: 1, replyId: 1 };
      
      service.updateShares(shareId, updatedShare).subscribe(share => {
        expect(share).toEqual(updatedShare);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/share/${shareId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedShare);
    });
  });

  describe('deleteShare', () => {
    it('should make DELETE request to delete a share by id', () => {
      const shareId = 1;
      
      service.deleteShares(shareId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/share/${shareId}`);
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

  it('should return an observable with a share-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
