import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FeedbackService } from './feedbacks.service';
import { environment } from '@environments/environment';
import { Feedback, FeedbackStatusEnum, FeedbackTypeEnum } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('FeedbackService', () => {
  let service: FeedbackService;
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
        FeedbackService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(FeedbackService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if feedback is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if feedback is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all feedbacks', () => {
      const mockFeedbacks: Feedback[] = [{ FeedbackId: 1, Title: "", Description: "", IsLocked: false, IsFeatured: false, Type: FeedbackTypeEnum.pending, Status: FeedbackStatusEnum.public, DateFeedbackCreated: "", DateFeedbackUpdated: "", DateFeedbackDeleted: "", UpvoteCounter: 0, DownvoteCounter: 0, UserId: 1 }];
      
      service.getAll().subscribe(feedbacks => {
        expect(feedbacks).toEqual(mockFeedbacks);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/feedback`);
      expect(req.request.method).toBe('GET');
      req.flush(mockFeedbacks);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific feedback by id', () => {
      const feedbackId = 1;
      const mockFeedback: Feedback = { FeedbackId: 1, Title: "", Description: "", IsLocked: false, IsFeatured: false, Type: FeedbackTypeEnum.pending, Status: FeedbackStatusEnum.public, DateFeedbackCreated: "", DateFeedbackUpdated: "", DateFeedbackDeleted: "", UpvoteCounter: 0, DownvoteCounter: 0, UserId: 1 };
      
      service.getAllById(feedbackId).subscribe(feedback => {
        expect(feedback).toEqual(mockFeedback);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/feedback/${feedbackId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockFeedback);
    });
  });

  describe('createFeedbacks', () => {
    it('should make POST request to create a new feedback', () => {
      const newFeedback: Feedback = { FeedbackId: 2, Title: "", Description: "", IsLocked: false, IsFeatured: false, Type: FeedbackTypeEnum.pending, Status: FeedbackStatusEnum.public, DateFeedbackCreated: "", DateFeedbackUpdated: "", DateFeedbackDeleted: "", UpvoteCounter: 0, DownvoteCounter: 0, UserId: 1 };
      const createdFeedback: Feedback = { ...newFeedback };
      
      service.createFeedbacks(newFeedback).subscribe(feedback => {
        expect(feedback).toEqual(createdFeedback);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/feedback`);
      expect(req.request.method).toBe('POST');
      req.flush(createdFeedback);
    });
  });

  describe('updateFeedbacks', () => {
    it('should make PUT request to update an existing feedback', () => {
      const feedbackId = 1;
      const updatedFeedback: Feedback = { FeedbackId: feedbackId, Title: "feedback1", Description: "", IsLocked: false, IsFeatured: false, Type: FeedbackTypeEnum.pending, Status: FeedbackStatusEnum.public, DateFeedbackCreated: "", DateFeedbackUpdated: "", DateFeedbackDeleted: "", UpvoteCounter: 0, DownvoteCounter: 0, UserId: 1 };
      
      service.updateFeedbacks(feedbackId, updatedFeedback).subscribe(feedback => {
        expect(feedback).toEqual(updatedFeedback);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/feedback/${feedbackId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedFeedback);
    });
  });

  describe('deleteFeedbacks', () => {
    it('should make DELETE request to delete a feedback by id', () => {
      const feedbackId = 1;
      
      service.deleteFeedbacks(feedbackId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/feedback/${feedbackId}`);
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

  it('should return an observable with a feedback-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
