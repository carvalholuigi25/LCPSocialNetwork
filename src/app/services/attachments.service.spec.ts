import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AttachmentsService } from './attachments.service';
import { DOCUMENT } from '@angular/common';
import { Attachment } from '../models';
import { environment } from '@environments/environment';
import { HttpErrorResponse } from '@angular/common/http';

describe('AttachmentsService', () => {
  let service: AttachmentsService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AttachmentsService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(AttachmentsService);
    httpTestingController = TestBed.inject(HttpTestingController);

    // Mock local storage
    let store: any = {};
    jest.spyOn(localStorage, 'getItem').mockImplementation((key: string) => store[key]);
    jest.spyOn(localStorage, 'setItem').mockImplementation((key: string, value: string) => store[key] = `${value}`);
    // Set a mock user token in local storage for authorization
    localStorage.setItem('user', JSON.stringify({ token: 'mockToken' }));
  });

  afterEach(() => {
    httpTestingController.verify(); // Verifies that no requests are outstanding.
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('#getAll should retrieve attachments', () => {
    const mockAttachments: Attachment[] = [
      { attachmentId: 1, attachmentUrl: 'http://example.com/1', attachmentType: 'image/png', title: 'Attach1', description: "", status: 'online', dateAttachmentUploaded: '2024-03-28T08:44:00', dateAttachmentDeleted: '2024-03-28T08:44:00', dateAttachmentUpdated: '2024-03-28T08:44:00', isFeatured: true, postId: 1, commentId: 1, replyId: 1, reactionId: 1, shareId: 1, userId: 1 }
    ];

    service.getAll().subscribe(attachments => {
      expect(attachments.length).toBe(1);
      expect(attachments).toEqual(mockAttachments);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/attachment`);
    expect(req.request.method).toBe('GET');
    req.flush(mockAttachments);
  });

  it('#getAllById should retrieve attachment', () => {
    const mockAttachments: Attachment = { attachmentId: 1, attachmentUrl: 'http://example.com/1', attachmentType: 'image/png', title: 'Attach1', description: "", status: 'online', dateAttachmentUploaded: '2024-03-28T08:44:00', dateAttachmentDeleted: '2024-03-28T08:44:00', dateAttachmentUpdated: '2024-03-28T08:44:00', isFeatured: true, postId: 1, commentId: 1, replyId: 1, reactionId: 1, shareId: 1, userId: 1 };

    service.getAllById(1).subscribe(attachments => {
      expect(attachments).toEqual(mockAttachments);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/attachment/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockAttachments);
  });

  it('#createAttachment should add a new attachment', () => {
    const newAttachment: Attachment = { attachmentId: 2, attachmentUrl: 'http://example.com/2', attachmentType: 'image/png', title: 'Attach2', description: "", status: 'online', dateAttachmentUploaded: '2024-03-28T09:44:00', dateAttachmentDeleted: '2024-03-28T08:44:00', dateAttachmentUpdated: '2024-03-28T08:44:00', isFeatured: true, postId: 1, commentId: 1, replyId: 1, reactionId: 1, shareId: 1, userId: 1 };

    service.createAttachment(newAttachment).subscribe(attachment => {
      expect(attachment).toEqual(newAttachment);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/attachment`);
    expect(req.request.method).toBe('POST');
    req.flush(newAttachment);
  });

  it('#updateAttachment should update the current attachment', () => {
    const newAttachment: Attachment = { attachmentId: 2, attachmentUrl: 'http://example.com/2', attachmentType: 'image/png', title: 'Attach2', description: "", status: 'online', dateAttachmentUploaded: '2024-03-28T09:44:00', dateAttachmentDeleted: '2024-03-28T08:44:00', dateAttachmentUpdated: '2024-03-28T08:44:00', isFeatured: false, postId: 1, commentId: 1, replyId: 1, reactionId: 1, shareId: 1, userId: 1 };

    service.updateAttachment(2, newAttachment).subscribe(attachment => {
      expect(attachment).toEqual(newAttachment);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/attachment/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newAttachment);
  });

  it('#deleteAttachment should delete the current attachment', () => {
    const newAttachment: Attachment = { attachmentId: 2, attachmentUrl: 'http://example.com/2', attachmentType: 'image/png', title: 'Attach2', description: "", status: 'online', dateAttachmentUploaded: '2024-03-28T09:44:00', dateAttachmentDeleted: '2024-03-28T08:44:00', dateAttachmentUpdated: '2024-03-28T08:44:00', isFeatured: false, postId: 1, commentId: 1, replyId: 1, reactionId: 1, shareId: 1, userId: 1 };

    service.deleteAttachment(2).subscribe(attachment => {
      expect(attachment).toEqual(newAttachment);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/attachment/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newAttachment);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not attachments'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});