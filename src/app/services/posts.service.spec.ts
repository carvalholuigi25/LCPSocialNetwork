import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PostsService } from './posts.service';
import { DOCUMENT } from '@angular/common';
import { Post } from '../models';
import { environment } from '@environments/environment';
import { AuthService } from './auth.service';
// import { HttpErrorResponse } from '@angular/common/http';

describe('PostsService', () => {
  let service: PostsService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;
  let userId: number = 1;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        PostsService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });

    service = TestBed.inject(PostsService);
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

  it('#getAll should retrieve posts', () => {
    const mockPosts: Post[] = [
        { PostId: 1, Title: "Post 1", Description: "", ImgUrl: "", Status: "", DatePostCreated: "2024-03-28T12:00:00", DatePostDeleted: "2024-03-28T12:00:00", DatePostUpdated: "2024-03-28T12:00:00", IsFeatured: true, UserId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 }
    ];

    service.getAll().subscribe(posts => {
      expect(posts.length).toBe(1);
      expect(posts).toEqual(mockPosts);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/post`);
    expect(req.request.method).toBe('GET');
    req.flush(mockPosts);
  });

  it('#getAllWithUsers should retrieve posts', () => {
    service.getAllByUsersId(userId);
    expect(service.getAllByUsersId).toBeTruthy();
  });

  it('#getAllById should retrieve post', () => {
    const mockPosts: Post = { PostId: 1, Title: "Post 1", Description: "", ImgUrl: "", Status: "", DatePostCreated: "2024-03-28T12:00:00", DatePostDeleted: "2024-03-28T12:00:00", DatePostUpdated: "2024-03-28T12:00:00", IsFeatured: true, UserId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };

    service.getAllById(1).subscribe(posts => {
      expect(posts).toEqual(mockPosts);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/post/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockPosts);
  });

  it('#getAllByUsersId should retrieve post', () => {
    const mockPosts: Post = { PostId: 1, Title: "Post 1", Description: "", ImgUrl: "", Status: "", DatePostCreated: "2024-03-28T12:00:00", DatePostDeleted: "2024-03-28T12:00:00", DatePostUpdated: "2024-03-28T12:00:00", IsFeatured: true, UserId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };

    service.getAllByUsersId(1).subscribe(posts => {
      expect(posts).toEqual(mockPosts);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/post/user/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockPosts);
  });

  it('#createPosts should add a new post', () => {
    const newPost: Post = { PostId: 2, Title: "Post 2", Description: "", ImgUrl: "", Status: "", DatePostCreated: "2024-03-28T12:00:00", DatePostDeleted: "2024-03-28T12:00:00", DatePostUpdated: "2024-03-28T12:00:00", IsFeatured: true, UserId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };

    service.createPosts(newPost).subscribe(post => {
      expect(post).toEqual(newPost);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/post`);
    expect(req.request.method).toBe('POST');
    req.flush(newPost);
  });

  it('#updatePosts should update the current post', () => {
    const newPost: Post = { PostId: 2, Title: "Post 2", Description: "", ImgUrl: "", Status: "", DatePostCreated: "2024-03-28T13:00:00", DatePostDeleted: "2024-03-28T13:00:00", DatePostUpdated: "2024-03-28T13:00:00", IsFeatured: true, UserId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };

    service.updatePosts(2, newPost).subscribe(post => {
      expect(post).toEqual(newPost);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/post/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newPost);
  });

  it('#deletePosts should delete the current post', () => {
    const newPost: Post = { PostId: 2, Title: "Post 2", Description: "", ImgUrl: "", Status: "", DatePostCreated: "2024-03-28T13:00:00", DatePostDeleted: "2024-03-28T13:00:00", DatePostUpdated: "2024-03-28T13:00:00", IsFeatured: true, UserId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };

    service.deletePosts(2).subscribe(post => {
      expect(post).toEqual(newPost);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/post/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newPost);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not posts'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});