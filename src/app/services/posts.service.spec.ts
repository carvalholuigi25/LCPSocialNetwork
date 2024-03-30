import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PostsService } from './posts.service';
import { environment } from '@environments/environment';
import { Post, User } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('PostsService', () => {
  let service: PostsService;
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
        PostsService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(PostsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if post is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if post is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all posts', () => {
      const mockPosts: Post[] = [{ PostId: 1, Title: "", Description: "", ImgUrl: "", Status: "", DatePostCreated: "", DatePostUpdated: "", DatePostDeleted: "", TypeTxtPost: "", IsFeatured: true, UserId: 1, CommentId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 }];
      
      service.getAll().subscribe(posts => {
        expect(posts).toEqual(mockPosts);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/post`);
      expect(req.request.method).toBe('GET');
      req.flush(mockPosts);
    });
  });

  describe('getAllWithUsers', () => {
    it('should make GET request to fetch all posts with users', () => {
      const userId: number = 1;
      const pid = userId !== -1 ? `/user/${userId}` : userId;
      const mockPosts: Post[] = [{ PostId: 1, Title: "", Description: "", ImgUrl: "", Status: "", DatePostCreated: "", DatePostUpdated: "", DatePostDeleted: "", TypeTxtPost: "", IsFeatured: true, UserId: 1, CommentId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 }];
      const mockUsers: User[] = [{ UserId: 1, Username: 'admin', Password: "admin2024" }];
      
      service.getAllWithUsers(userId).subscribe((r) => {
        expect(r[0]).toEqual(mockPosts);
        expect(r[1]).toEqual(mockUsers);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/post${pid}`);
      expect(req.request.method).toBe('GET');

      const req2 = httpMock.expectOne(`${environment.apiUrl}/user`);
      expect(req2.request.method).toBe('GET');
      req.flush([mockPosts, mockUsers]);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific post by id', () => {
      const postId = 1;
      const mockPost: Post = { PostId: 1, Title: "", Description: "", ImgUrl: "", Status: "", DatePostCreated: "", DatePostUpdated: "", DatePostDeleted: "", TypeTxtPost: "", IsFeatured: true, UserId: 1, CommentId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };
      
      service.getAllById(postId).subscribe(post => {
        expect(post).toEqual(mockPost);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/post/${postId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockPost);
    });
  });

  describe('getAllByUsersId', () => {
    it('should make GET request to fetch a specific post by user id', () => {
      const userId = 1;
      const mockPost: Post = { PostId: 1, Title: "", Description: "", ImgUrl: "", Status: "", DatePostCreated: "", DatePostUpdated: "", DatePostDeleted: "", TypeTxtPost: "", IsFeatured: true, UserId: 1, CommentId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };
      
      service.getAllByUsersId(userId).subscribe(post => {
        expect(post).toEqual(mockPost);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/post/user/${userId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockPost);
    });
  });

  describe('createPosts', () => {
    it('should make POST request to create a new post', () => {
      const newPost: Post = { PostId: 1, Title: "", Description: "", ImgUrl: "", Status: "", DatePostCreated: "", DatePostUpdated: "", DatePostDeleted: "", TypeTxtPost: "", IsFeatured: true, UserId: 1, CommentId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };
      const createdPost: Post = { ...newPost };
      
      service.createPosts(newPost).subscribe(post => {
        expect(post).toEqual(createdPost);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/post`);
      expect(req.request.method).toBe('POST');
      req.flush(createdPost);
    });
  });

  describe('updatePosts', () => {
    it('should make PUT request to update an existing post', () => {
      const postId = 1;
      const updatedPost: Post = { PostId: 1, Title: "", Description: "", ImgUrl: "", Status: "", DatePostCreated: "", DatePostUpdated: "", DatePostDeleted: "", TypeTxtPost: "", IsFeatured: true, UserId: 1, CommentId: 1, ReplyId: 1, ShareId: 1, ReactionId: 1, AttachmentId: 1 };
      
      service.updatePosts(postId, updatedPost).subscribe(post => {
        expect(post).toEqual(updatedPost);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/post/${postId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedPost);
    });
  });

  describe('deletePosts', () => {
    it('should make DELETE request to delete a post by id', () => {
      const postId = 1;
      
      service.deletePosts(postId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/post/${postId}`);
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

  it('should return an observable with a post-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
