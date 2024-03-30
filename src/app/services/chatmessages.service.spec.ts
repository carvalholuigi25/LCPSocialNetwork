import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ChatMessagesService } from './chatmessages.service';
import { environment } from '@environments/environment';
import { ChatMessage } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('ChatMessagesService', () => {
  let service: ChatMessagesService;
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
        ChatMessagesService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(ChatMessagesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if chatmessage is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if chatmessage is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all chatmessages', () => {
      const mockChatMessages: ChatMessage[] = [ { 
        chatMessageId: 1,
        description: "",
        status: "",
        isRead: false,
        dateChatMessageCreated: "",
        dateChatMessageReaded: "",
        dateChatMessageUpdated: "",
        dateChatMessageDeleted: "",
        commentId: 1,
        replyId: 1,
        userId: 1,
        reactionId: 1,
        shareId: 1,
        attachmentId: 1 
      }];
      
      service.getAll().subscribe(chatmessages => {
        expect(chatmessages).toEqual(mockChatMessages);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/chatmessage`);
      expect(req.request.method).toBe('GET');
      req.flush(mockChatMessages);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific chatmessage by id', () => {
      const chatmessageId = 1;
      const mockChatMessage: ChatMessage = { 
        chatMessageId: 1,
        description: "",
        status: "",
        isRead: false,
        dateChatMessageCreated: "",
        dateChatMessageReaded: "",
        dateChatMessageUpdated: "",
        dateChatMessageDeleted: "",
        commentId: 1,
        replyId: 1,
        userId: 1,
        reactionId: 1,
        shareId: 1,
        attachmentId: 1 
      };
      
      service.getAllById(chatmessageId).subscribe(chatmessage => {
        expect(chatmessage).toEqual(mockChatMessage);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/chatmessage/${chatmessageId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockChatMessage);
    });
  });

  describe('createChatMessages', () => {
    it('should make POST request to create a new chatmessage', () => {
      const newChatMessage: ChatMessage = { 
        chatMessageId: 1,
        description: "",
        status: "",
        isRead: false,
        dateChatMessageCreated: "",
        dateChatMessageReaded: "",
        dateChatMessageUpdated: "",
        dateChatMessageDeleted: "",
        commentId: 1,
        replyId: 1,
        userId: 1,
        reactionId: 1,
        shareId: 1,
        attachmentId: 1 
      };
      const createdChatMessage: ChatMessage = { ...newChatMessage };
      
      service.createChatMessages(newChatMessage).subscribe(chatmessage => {
        expect(chatmessage).toEqual(createdChatMessage);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/chatmessage`);
      expect(req.request.method).toBe('POST');
      req.flush(createdChatMessage);
    });
  });

  describe('updateChatMessages', () => {
    it('should make PUT request to update an existing chatmessage', () => {
      const chatmessageId = 1;
      const updatedChatMessage: ChatMessage = { 
        chatMessageId: 1,
        description: "",
        status: "",
        isRead: false,
        dateChatMessageCreated: "",
        dateChatMessageReaded: "",
        dateChatMessageUpdated: "",
        dateChatMessageDeleted: "",
        commentId: 1,
        replyId: 1,
        userId: 1,
        reactionId: 1,
        shareId: 1,
        attachmentId: 1 
      };
      
      service.updateChatMessages(chatmessageId, updatedChatMessage).subscribe(chatmessage => {
        expect(chatmessage).toEqual(updatedChatMessage);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/chatmessage/${chatmessageId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedChatMessage);
    });
  });

  describe('deleteChatMessages', () => {
    it('should make DELETE request to delete a chatmessage by id', () => {
      const chatmessageId = 1;
      
      service.deleteChatMessages(chatmessageId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/chatmessage/${chatmessageId}`);
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

  it('should return an observable with a chatmessage-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
