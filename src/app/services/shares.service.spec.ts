import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SharesService } from './shares.service';
import { DOCUMENT } from '@angular/common';
import { Share } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('SharesService', () => {
  let service: SharesService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        SharesService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(SharesService);
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

  it('#getAll should retrieve shares', () => {
    const mockShares: Share[] = [
      {
        shareId: 1,
        shareCounter: 0,
        dateShared: "2024-03-28T12:26:00",
        attachmentId: 1,
        postId: 1,
        commentId: 1,
        replyId: 1,
        userId: 1
      }
    ];

    service.getAll().subscribe(shares => {
      expect(shares.length).toBe(1);
      expect(shares).toEqual(mockShares);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/share`);
    expect(req.request.method).toBe('GET');
    req.flush(mockShares);
  });

  it('#getAllById should retrieve share', () => {
    const mockShares: Share = {
        shareId: 1,
        shareCounter: 0,
        dateShared: "2024-03-28T12:26:00",
        attachmentId: 1,
        postId: 1,
        commentId: 1,
        replyId: 1,
        userId: 1
    };

    service.getAllById(1).subscribe(shares => {
      expect(shares).toEqual(mockShares);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/share/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockShares);
  });

  it('#createShares should add a new share', () => {
    const newShare: Share = {
        shareId: 2,
        shareCounter: 0,
        dateShared: "2024-03-28T12:26:00",
        attachmentId: 1,
        postId: 1,
        commentId: 1,
        replyId: 1,
        userId: 1
    };

    service.createShares(newShare).subscribe(share => {
      expect(share).toEqual(newShare);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/share`);
    expect(req.request.method).toBe('POST');
    req.flush(newShare);
  });

  it('#updateShares should update the current share', () => {
    const newShare: Share = {
        shareId: 2,
        shareCounter: 1,
        dateShared: "2024-03-28T13:26:00",
        attachmentId: 1,
        postId: 1,
        commentId: 1,
        replyId: 1,
        userId: 1
    }

    service.updateShares(2, newShare).subscribe(share => {
      expect(share).toEqual(newShare);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/share/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newShare);
  });

  it('#deleteShares should delete the current share', () => {
    const newShare: Share = {
        shareId: 2,
        shareCounter: 1,
        dateShared: "2024-03-28T13:26:00",
        attachmentId: 1,
        postId: 1,
        commentId: 1,
        replyId: 1,
        userId: 1
    };

    service.deleteShares(2).subscribe(share => {
      expect(share).toEqual(newShare);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/share/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newShare);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not shares'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});