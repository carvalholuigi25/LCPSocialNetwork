import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ChatMessagesService } from './chatmessages.service';
import { DOCUMENT } from '@angular/common';
import { ChatMessage } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('ChatMessagesService', () => {
  let service: ChatMessagesService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        ChatMessagesService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(ChatMessagesService);
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

  it('#getAll should retrieve chatmessages', () => {
    const mockChatMessages: ChatMessage[] = [
      { chatMessageId: 1, description: "", status: "online", isRead: true, dateChatMessageCreated: "2024-03-28T11:52:00", dateChatMessageDeleted: "2024-03-28T11:52:00", dateChatMessageReaded: "2024-03-28T11:52:00", dateChatMessageUpdated: "2024-03-28T11:52:00", commentId: 1, replyId: 1, userId: 1, reactionId: 1, shareId: 1, attachmentId: 1 }
    ];

    service.getAll().subscribe(chatmessages => {
      expect(chatmessages.length).toBe(1);
      expect(chatmessages).toEqual(mockChatMessages);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/chatmessage`);
    expect(req.request.method).toBe('GET');
    req.flush(mockChatMessages);
  });

  it('#getAllById should retrieve chatmessage', () => {
    const mockChatMessages: ChatMessage = { chatMessageId: 1, description: "", status: "online", isRead: true, dateChatMessageCreated: "2024-03-28T11:52:00", dateChatMessageDeleted: "2024-03-28T11:52:00", dateChatMessageReaded: "2024-03-28T11:52:00", dateChatMessageUpdated: "2024-03-28T11:52:00", commentId: 1, replyId: 1, userId: 1, reactionId: 1, shareId: 1, attachmentId: 1 };

    service.getAllById(1).subscribe(chatmessages => {
      expect(chatmessages).toEqual(mockChatMessages);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/chatmessage/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockChatMessages);
  });

  it('#createChatMessages should add a new chatmessage', () => {
    const newChatMessage: ChatMessage = { chatMessageId: 2, description: "", status: "online", isRead: true, dateChatMessageCreated: "2024-03-28T12:52:00", dateChatMessageDeleted: "2024-03-28T12:52:00", dateChatMessageReaded: "2024-03-28T12:52:00", dateChatMessageUpdated: "2024-03-28T12:52:00", commentId: 1, replyId: 1, userId: 1, reactionId: 1, shareId: 1, attachmentId: 1 };

    service.createChatMessages(newChatMessage).subscribe(chatmessage => {
      expect(chatmessage).toEqual(newChatMessage);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/chatmessage`);
    expect(req.request.method).toBe('POST');
    req.flush(newChatMessage);
  });

  it('#updateChatMessages should update the current chatmessage', () => {
    const newChatMessage: ChatMessage = { chatMessageId: 2, description: "", status: "offline", isRead: true, dateChatMessageCreated: "2024-03-28T11:52:00", dateChatMessageDeleted: "2024-03-28T11:52:00", dateChatMessageReaded: "2024-03-28T11:52:00", dateChatMessageUpdated: "2024-03-28T11:52:00", commentId: 1, replyId: 1, userId: 1, reactionId: 1, shareId: 1, attachmentId: 1 };

    service.updateChatMessages(2, newChatMessage).subscribe(chatmessage => {
      expect(chatmessage).toEqual(newChatMessage);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/chatmessage/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newChatMessage);
  });

  it('#deleteChatMessages should delete the current chatmessage', () => {
    const newChatMessage: ChatMessage = { chatMessageId: 2, description: "", status: "offline", isRead: true, dateChatMessageCreated: "2024-03-28T11:52:00", dateChatMessageDeleted: "2024-03-28T11:52:00", dateChatMessageReaded: "2024-03-28T11:52:00", dateChatMessageUpdated: "2024-03-28T11:52:00", commentId: 1, replyId: 1, userId: 1, reactionId: 1, shareId: 1, attachmentId: 1 };

    service.deleteChatMessages(2).subscribe(chatmessage => {
      expect(chatmessage).toEqual(newChatMessage);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/chatmessage/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newChatMessage);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not chatmessages'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});