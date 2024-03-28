import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NotificationsService } from './notifications.service';
import { DOCUMENT } from '@angular/common';
import { Notification } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('NotificationsService', () => {
  let service: NotificationsService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        NotificationsService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(NotificationsService);
    httpTestingController = TestBed.inject(HttpTestingController);

    // Mock local storage
    let store: any = {};
    spyOn(localStorage, 'getItem').and.callFake((key: string) => store[key]);
    spyOn(localStorage, 'setItem').and.callFake((key: string, value: string) => store[key] = `${value}`);
    // Set a mock user token in local storage for authorization
    localStorage.setItem('user', JSON.stringify({ token: 'mockToken' }));
  });

  afterEach(() => {
    httpTestingController.verify(); // Verifies that no requests are outstanding.
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('#getAll should retrieve notifications', () => {
    const mockNotifications: Notification[] = [
      { notificationId: 1, description: "Notification 1", status: "", isMarkRead: true, isPinned: false, dateUserNotificationCreated: "2024-03-28T12:00:00", dateUserNotificationDeleted: "2024-03-28T12:00:00", dateUserNotificationUpdated: "2024-03-28T12:00:00", dateUserNotificationMarked: "2024-03-28T12:00:00", userId: 1, postId: 1, replyId: 1, commentId: 1, reactionId: 1, attachmentId: 1 }
    ];

    service.getAll().subscribe(notifications => {
      expect(notifications.length).toBe(1);
      expect(notifications).toEqual(mockNotifications);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/notification`);
    expect(req.request.method).toBe('GET');
    req.flush(mockNotifications);
  });

  it('#getAllById should retrieve notification', () => {
    const mockNotifications: Notification = { notificationId: 1, description: "Notification 1", status: "", isMarkRead: true, isPinned: false, dateUserNotificationCreated: "2024-03-28T12:00:00", dateUserNotificationDeleted: "2024-03-28T12:00:00", dateUserNotificationUpdated: "2024-03-28T12:00:00", dateUserNotificationMarked: "2024-03-28T12:00:00", userId: 1, postId: 1, replyId: 1, commentId: 1, reactionId: 1, attachmentId: 1 };

    service.getAllById(1).subscribe(notifications => {
      expect(notifications).toEqual(mockNotifications);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/notification/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockNotifications);
  });

  it('#createNotifications should add a new notification', () => {
    const newNotification: Notification = { notificationId: 2, description: "Notification 2", status: "", isMarkRead: true, isPinned: false, dateUserNotificationCreated: "2024-03-28T12:00:00", dateUserNotificationDeleted: "2024-03-28T12:00:00", dateUserNotificationUpdated: "2024-03-28T12:00:00", dateUserNotificationMarked: "2024-03-28T12:00:00", userId: 1, postId: 1, replyId: 1, commentId: 1, reactionId: 1, attachmentId: 1 };

    service.createNotification(newNotification).subscribe(notification => {
      expect(notification).toEqual(newNotification);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/notification`);
    expect(req.request.method).toBe('POST');
    req.flush(newNotification);
  });

  it('#updateNotifications should update the current notification', () => {
    const newNotification: Notification = { notificationId: 2, description: "Notification 2", status: "", isMarkRead: true, isPinned: false, dateUserNotificationCreated: "2024-03-28T13:00:00", dateUserNotificationDeleted: "2024-03-28T13:00:00", dateUserNotificationUpdated: "2024-03-28T13:00:00", dateUserNotificationMarked: "2024-03-28T13:00:00", userId: 1, postId: 1, replyId: 1, commentId: 1, reactionId: 1, attachmentId: 1 };

    service.updateNotification(2, newNotification).subscribe(notification => {
      expect(notification).toEqual(newNotification);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/notification/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newNotification);
  });

  it('#deleteNotifications should delete the current notification', () => {
    const newNotification: Notification = { notificationId: 2, description: "Notification 2", status: "", isMarkRead: true, isPinned: false, dateUserNotificationCreated: "2024-03-28T13:00:00", dateUserNotificationDeleted: "2024-03-28T13:00:00", dateUserNotificationUpdated: "2024-03-28T13:00:00", dateUserNotificationMarked: "2024-03-28T13:00:00", userId: 1, postId: 1, replyId: 1, commentId: 1, reactionId: 1, attachmentId: 1 };

    service.deleteNotification(2).subscribe(notification => {
      expect(notification).toEqual(newNotification);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/notification/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newNotification);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not notifications'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});