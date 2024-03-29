import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FriendsRequestsService } from './friendsrequests.service';
import { DOCUMENT } from '@angular/common';
import { FriendRequest, FriendRequestTypeEnum } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('FriendRequestsService', () => {
  let service: FriendsRequestsService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        FriendsRequestsService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(FriendsRequestsService);
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

  it('#getAll should retrieve friendrequests', () => {
    const mockFriendRequests: FriendRequest[] = [
      { 
        friendRequestId: 1, 
        description: "",
        status: "",
        friendRequestType: FriendRequestTypeEnum.unknown,
        isAccepted: false,
        dateFriendRequestCreated: "2024-03-28T12:05:00",
        dateFriendRequestAccepted: "2024-03-28T12:05:00",
        dateFriendRequestDeleted:  "2024-03-28T12:05:00",
        userId: 1 
      }
    ];

    service.getAll().subscribe(friendrequests => {
      expect(friendrequests.length).toBe(1);
      expect(friendrequests).toEqual(mockFriendRequests);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/friendrequest`);
    expect(req.request.method).toBe('GET');
    req.flush(mockFriendRequests);
  });

  it('#getAllById should retrieve friendrequest', () => {
    const mockFriendRequests: FriendRequest = { 
        friendRequestId: 1, 
        description: "",
        status: "",
        friendRequestType: FriendRequestTypeEnum.unknown,
        isAccepted: false,
        dateFriendRequestCreated: "2024-03-28T12:05:00",
        dateFriendRequestAccepted: "2024-03-28T12:05:00",
        dateFriendRequestDeleted:  "2024-03-28T12:05:00",
        userId: 1 
    };

    service.getAllById(1).subscribe(friendrequests => {
      expect(friendrequests).toEqual(mockFriendRequests);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/friendrequest/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockFriendRequests);
  });

  it('#createFriendsRequests should add a new friendrequest', () => {
    const newFriendRequest: FriendRequest =  { 
        friendRequestId: 2, 
        description: "",
        status: "",
        friendRequestType: FriendRequestTypeEnum.unknown,
        isAccepted: false,
        dateFriendRequestCreated: "2024-03-28T12:05:00",
        dateFriendRequestAccepted: "2024-03-28T12:05:00",
        dateFriendRequestDeleted:  "2024-03-28T12:05:00",
        userId: 1 
    };

    service.createFriendsRequests(newFriendRequest).subscribe(friendrequest => {
      expect(friendrequest).toEqual(newFriendRequest);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/friendrequest`);
    expect(req.request.method).toBe('POST');
    req.flush(newFriendRequest);
  });

  it('#updateFriendsRequests should update the current friendrequest', () => {
    const newFriendRequest: FriendRequest = { 
        friendRequestId: 2, 
        description: "",
        status: "",
        friendRequestType: FriendRequestTypeEnum.friend,
        isAccepted: true,
        dateFriendRequestCreated: "2024-03-28T12:05:00",
        dateFriendRequestAccepted: "2024-03-28T12:05:00",
        dateFriendRequestDeleted:  "2024-03-28T12:05:00",
        userId: 1 
    };

    service.updateFriendsRequests(2, newFriendRequest).subscribe(friendrequest => {
      expect(friendrequest).toEqual(newFriendRequest);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/friendrequest/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newFriendRequest);
  });

  it('#deleteFriendsRequests should delete the current friendrequest', () => {
    const newFriendRequest: FriendRequest = { 
        friendRequestId: 2, 
        description: "",
        status: "",
        friendRequestType: FriendRequestTypeEnum.friend,
        isAccepted: true,
        dateFriendRequestCreated: "2024-03-28T12:05:00",
        dateFriendRequestAccepted: "2024-03-28T12:05:00",
        dateFriendRequestDeleted:  "2024-03-28T12:05:00",
        userId: 1 
    };

    service.deleteFriendsRequests(2).subscribe(friendrequest => {
      expect(friendrequest).toEqual(newFriendRequest);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/friendrequest/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newFriendRequest);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not friendrequests'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});