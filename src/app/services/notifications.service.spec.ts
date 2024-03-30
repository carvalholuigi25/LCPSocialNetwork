import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NotificationsService } from './notifications.service';
import { environment } from '@environments/environment';
import { Notification } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('NotificationsService', () => {
  let service: NotificationsService;
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
        NotificationsService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(NotificationsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if notification is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if notification is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all notifications', () => {
      const mockNotifications: Notification[] = [{ notificationId: 1, description: "", status: "", isMarkRead: false, isPinned: false, dateUserNotificationCreated: "", dateUserNotificationUpdated: "", dateUserNotificationDeleted: "", dateUserNotificationMarked: "", postId: 1, reactionId: 1, replyId: 1, commentId: 1, attachmentId: 1, userId: 1 }];
      
      service.getAll().subscribe(notifications => {
        expect(notifications).toEqual(mockNotifications);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/notification`);
      expect(req.request.method).toBe('GET');
      req.flush(mockNotifications);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific notification by id', () => {
      const notificationId = 1;
      const mockNotification: Notification = { notificationId: 1, description: "", status: "", isMarkRead: false, isPinned: false, dateUserNotificationCreated: "", dateUserNotificationUpdated: "", dateUserNotificationDeleted: "", dateUserNotificationMarked: "", postId: 1, reactionId: 1, replyId: 1, commentId: 1, attachmentId: 1, userId: 1 };
      
      service.getAllById(notificationId).subscribe(notification => {
        expect(notification).toEqual(mockNotification);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/notification/${notificationId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockNotification);
    });
  });

  describe('createNotifications', () => {
    it('should make POST request to create a new notification', () => {
      const newNotification: Notification = { notificationId: 1, description: "", status: "", isMarkRead: false, isPinned: false, dateUserNotificationCreated: "", dateUserNotificationUpdated: "", dateUserNotificationDeleted: "", dateUserNotificationMarked: "", postId: 1, reactionId: 1, replyId: 1, commentId: 1, attachmentId: 1, userId: 1 };
      const createdNotification: Notification = { ...newNotification };
      
      service.createNotification(newNotification).subscribe(notification => {
        expect(notification).toEqual(createdNotification);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/notification`);
      expect(req.request.method).toBe('POST');
      req.flush(createdNotification);
    });
  });

  describe('updateNotifications', () => {
    it('should make PUT request to update an existing notification', () => {
      const notificationId = 1;
      const updatedNotification: Notification = { notificationId: 1, description: "", status: "", isMarkRead: false, isPinned: false, dateUserNotificationCreated: "", dateUserNotificationUpdated: "", dateUserNotificationDeleted: "", dateUserNotificationMarked: "", postId: 1, reactionId: 1, replyId: 1, commentId: 1, attachmentId: 1, userId: 1 };
      
      service.updateNotification(notificationId, updatedNotification).subscribe(notification => {
        expect(notification).toEqual(updatedNotification);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/notification/${notificationId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedNotification);
    });
  });

  describe('deleteNotifications', () => {
    it('should make DELETE request to delete a notification by id', () => {
      const notificationId = 1;
      
      service.deleteNotification(notificationId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/notification/${notificationId}`);
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

  it('should return an observable with a notification-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
