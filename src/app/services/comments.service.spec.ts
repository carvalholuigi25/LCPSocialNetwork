import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CommentService } from './comments.service';
import { DOCUMENT } from '@angular/common';
import { Comment } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('CommentsService', () => {
  let service: CommentService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        CommentService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(CommentService);
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

  it('#getAll should retrieve comments', () => {
    const mockComments: Comment[] = [
      { commentId: 1, title: "Comment 1", description: "", imgUrl: "", status: "", dateCommentCreated: "2024-03-28T12:00:00", dateCommentDeleted: "2024-03-28T12:00:00", dateCommentUpdated: "2024-03-28T12:00:00", isFeatured: true, userId: 1, postId: 1, replyId: 1, shareId: 1, reactionId: 1, attachmentId: 1 }
    ];

    service.getAll().subscribe(comments => {
      expect(comments.length).toBe(1);
      expect(comments).toEqual(mockComments);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/comment`);
    expect(req.request.method).toBe('GET');
    req.flush(mockComments);
  });

  it('#getAllById should retrieve comment', () => {
    const mockComments: Comment = { commentId: 1, title: "Comment 1", description: "", imgUrl: "", status: "", dateCommentCreated: "2024-03-28T12:00:00", dateCommentDeleted: "2024-03-28T12:00:00", dateCommentUpdated: "2024-03-28T12:00:00", isFeatured: true, userId: 1, postId: 1, replyId: 1, shareId: 1, reactionId: 1, attachmentId: 1 };

    service.getAllById(1).subscribe(comments => {
      expect(comments).toEqual(mockComments);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/comment/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockComments);
  });

  it('#createComments should add a new comment', () => {
    const newComment: Comment = { commentId: 2, title: "Comment 2", description: "", imgUrl: "", status: "", dateCommentCreated: "2024-03-28T12:00:00", dateCommentDeleted: "2024-03-28T12:00:00", dateCommentUpdated: "2024-03-28T12:00:00", isFeatured: true, userId: 1, postId: 1, replyId: 1, shareId: 1, reactionId: 1, attachmentId: 1 };

    service.createComments(newComment).subscribe(comment => {
      expect(comment).toEqual(newComment);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/comment`);
    expect(req.request.method).toBe('POST');
    req.flush(newComment);
  });

  it('#updateComments should update the current comment', () => {
    const newComment: Comment = { commentId: 2, title: "Comment 2", description: "", imgUrl: "", status: "", dateCommentCreated: "2024-03-28T13:00:00", dateCommentDeleted: "2024-03-28T13:00:00", dateCommentUpdated: "2024-03-28T13:00:00", isFeatured: true, userId: 1, postId: 1, replyId: 1, shareId: 1, reactionId: 1, attachmentId: 1 };

    service.updateComments(2, newComment).subscribe(comment => {
      expect(comment).toEqual(newComment);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/comment/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newComment);
  });

  it('#deleteComments should delete the current comment', () => {
    const newComment: Comment = { commentId: 2, title: "Comment 2", description: "", imgUrl: "", status: "", dateCommentCreated: "2024-03-28T13:00:00", dateCommentDeleted: "2024-03-28T13:00:00", dateCommentUpdated: "2024-03-28T13:00:00", isFeatured: true, userId: 1, postId: 1, replyId: 1, shareId: 1, reactionId: 1, attachmentId: 1 };

    service.deleteComments(2).subscribe(comment => {
      expect(comment).toEqual(newComment);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/comment/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newComment);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not comments'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});