import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ReactionsService } from './reactions.service';
import { DOCUMENT } from '@angular/common';
import { Reaction, ReactionTypeEnum } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('ReactionsService', () => {
  let service: ReactionsService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        ReactionsService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(ReactionsService);
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

  it('#getAll should retrieve reactions', () => {
    const mockReactions: Reaction[] = [
        { reactionId: 1, reactionType: ReactionTypeEnum.laugh, dateReacted: "2024-03-28T13:00:00", reactionCounter: 1, userId: 1, postId: 1, replyId: 1, commentId: 1, attachmentId: 1 }
    ];

    service.getAll().subscribe(reactions => {
      expect(reactions.length).toBe(1);
      expect(reactions).toEqual(mockReactions);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reaction`);
    expect(req.request.method).toBe('GET');
    req.flush(mockReactions);
  });

  it('#getAllById should retrieve reaction', () => {
    const mockReactions: Reaction = { reactionId: 1, reactionType: ReactionTypeEnum.laugh, dateReacted: "2024-03-28T13:00:00", reactionCounter: 1, userId: 1, postId: 1, replyId: 1, commentId: 1, attachmentId: 1 };

    service.getAllById(1).subscribe(reactions => {
      expect(reactions).toEqual(mockReactions);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reaction/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockReactions);
  });

  it('#createReactions should add a new reaction', () => {
    const newReaction: Reaction = { reactionId: 2, reactionType: ReactionTypeEnum.like, dateReacted: "2024-03-28T12:00:00", reactionCounter: 1, userId: 1, postId: 1, replyId: 1, commentId: 1, attachmentId: 1 };

    service.createReactions(newReaction).subscribe(reaction => {
      expect(reaction).toEqual(newReaction);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reaction`);
    expect(req.request.method).toBe('POST');
    req.flush(newReaction);
  });

  it('#updateReactions should update the current reaction', () => {
    const newReaction: Reaction = { reactionId: 2, reactionType: ReactionTypeEnum.love, dateReacted: "2024-03-28T13:00:00", reactionCounter: 1, userId: 1, postId: 1, replyId: 1, commentId: 1, attachmentId: 1 };

    service.updateReactions(2, newReaction).subscribe(reaction => {
      expect(reaction).toEqual(newReaction);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reaction/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newReaction);
  });

  it('#deleteReactions should delete the current reaction', () => {
    const newReaction: Reaction = { reactionId: 2, reactionType: ReactionTypeEnum.love, dateReacted: "2024-03-28T13:00:00", reactionCounter: 1, userId: 1, postId: 1, replyId: 1, commentId: 1, attachmentId: 1 };

    service.deleteReactions(2).subscribe(reaction => {
      expect(reaction).toEqual(newReaction);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/reaction/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newReaction);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not reactions'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});