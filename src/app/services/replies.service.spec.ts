import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RepliesService } from './replies.service';
import { DOCUMENT } from '@angular/common';
import { Reply } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('RepliesService', () => {
  let service: RepliesService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        RepliesService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(RepliesService);
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

  it('#getAll should retrieve replys', () => {
    const mockReplys: Reply[] = [
        { 
            replyId: 1,
            title: "",
            description: "",
            imgUrl: "",
            status: "public",
            dateReplyCreated: "2024-03-28T12:22:00",
            dateReplyUpdated: "2024-03-28T12:22:00",
            dateReplyDeleted: "2024-03-28T12:22:00",
            isFeatured: true,
            reactionId: 1,
            shareId: 1,
            userId: 1,
            commentId: 1,
            postId: 1,
            attachmentId: 1
        }
    ];

    service.getAll().subscribe(replys => {
      expect(replys.length).toBe(1);
      expect(replys).toEqual(mockReplys);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reply`);
    expect(req.request.method).toBe('GET');
    req.flush(mockReplys);
  });

  it('#getAllById should retrieve reply', () => {
    const mockReplys: Reply = { 
        replyId: 1,
        title: "",
        description: "",
        imgUrl: "",
        status: "public",
        dateReplyCreated: "2024-03-28T12:22:00",
        dateReplyUpdated: "2024-03-28T12:22:00",
        dateReplyDeleted: "2024-03-28T12:22:00",
        isFeatured: true,
        reactionId: 1,
        shareId: 1,
        userId: 1,
        commentId: 1,
        postId: 1,
        attachmentId: 1
    };

    service.getAllById(1).subscribe(replys => {
      expect(replys).toEqual(mockReplys);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reply/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockReplys);
  });

  it('#createReplies should add a new reply', () => {
    const newReply: Reply = { 
        replyId: 2,
        title: "",
        description: "",
        imgUrl: "",
        status: "public",
        dateReplyCreated: "2024-03-28T12:22:00",
        dateReplyUpdated: "2024-03-28T12:22:00",
        dateReplyDeleted: "2024-03-28T12:22:00",
        isFeatured: true,
        reactionId: 1,
        shareId: 1,
        userId: 1,
        commentId: 1,
        postId: 1,
        attachmentId: 1
    };

    service.createReplies(newReply).subscribe(reply => {
      expect(reply).toEqual(newReply);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reply`);
    expect(req.request.method).toBe('POST');
    req.flush(newReply);
  });

  it('#updateReplies should update the current reply', () => {
    const newReply: Reply = { 
        replyId: 2,
        title: "",
        description: "",
        imgUrl: "",
        status: "public",
        dateReplyCreated: "2024-03-28T13:22:00",
        dateReplyUpdated: "2024-03-28T13:22:00",
        dateReplyDeleted: "2024-03-28T13:22:00",
        isFeatured: true,
        reactionId: 1,
        shareId: 1,
        userId: 1,
        commentId: 1,
        postId: 1,
        attachmentId: 1
    };

    service.updateReplies(2, newReply).subscribe(reply => {
      expect(reply).toEqual(newReply);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reply/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newReply);
  });

  it('#deleteReplies should delete the current reply', () => {
    const newReply: Reply = { 
        replyId: 2,
        title: "",
        description: "",
        imgUrl: "",
        status: "public",
        dateReplyCreated: "2024-03-28T13:22:00",
        dateReplyUpdated: "2024-03-28T13:22:00",
        dateReplyDeleted: "2024-03-28T13:22:00",
        isFeatured: true,
        reactionId: 1,
        shareId: 1,
        userId: 1,
        commentId: 1,
        postId: 1,
        attachmentId: 1
    };

    service.deleteReplies(2).subscribe(reply => {
      expect(reply).toEqual(newReply);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reply/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newReply);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not replys'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});