import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FriendsRequestsService } from './friendsrequests.service';
import { environment } from '@environments/environment';
import { FriendRequest, FriendRequestTypeEnum } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('FriendRequestsService', () => {
  let service: FriendsRequestsService;
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
        FriendsRequestsService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(FriendsRequestsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if friend request is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if friend request is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all friend requests', () => {
      const mockFriendrequests: FriendRequest[] = [{ friendRequestId: 1, description: "", status: "", friendRequestType: FriendRequestTypeEnum.friend, dateFriendRequestAccepted: "", dateFriendRequestCreated: "", dateFriendRequestDeleted: "", isAccepted: false, userId: 1 }];
      
      service.getAll().subscribe(friendrequests => {
        expect(friendrequests).toEqual(mockFriendrequests);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/friendrequest`);
      expect(req.request.method).toBe('GET');
      req.flush(mockFriendrequests);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific friend request by id', () => {
      const friendrequestId = 1;
      const mockFriendrequest: FriendRequest = { friendRequestId: 1, description: "", status: "", friendRequestType: FriendRequestTypeEnum.friend, dateFriendRequestAccepted: "", dateFriendRequestCreated: "", dateFriendRequestDeleted: "", isAccepted: false, userId: 1 };
      
      service.getAllById(friendrequestId).subscribe(friendrequest => {
        expect(friendrequest).toEqual(mockFriendrequest);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/friendrequest/${friendrequestId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockFriendrequest);
    });
  });

  describe('createFriendrequests', () => {
    it('should make POST request to create a new friend request', () => {
      const newFriendrequest: FriendRequest = { friendRequestId: 1, description: "", status: "", friendRequestType: FriendRequestTypeEnum.friend, dateFriendRequestAccepted: "", dateFriendRequestCreated: "", dateFriendRequestDeleted: "", isAccepted: false, userId: 1 };
      const createdFriendrequest: FriendRequest = { ...newFriendrequest };
      
      service.createFriendsRequests(newFriendrequest).subscribe(friendrequest => {
        expect(friendrequest).toEqual(createdFriendrequest);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/friendrequest`);
      expect(req.request.method).toBe('POST');
      req.flush(createdFriendrequest);
    });
  });

  describe('updateFriendrequests', () => {
    it('should make PUT request to update an existing friend request', () => {
      const friendrequestId = 1;
      const updatedFriendrequest: FriendRequest = { friendRequestId: 1, description: "", status: "", friendRequestType: FriendRequestTypeEnum.friend, dateFriendRequestAccepted: "", dateFriendRequestCreated: "", dateFriendRequestDeleted: "", isAccepted: false, userId: 1 };
      
      service.updateFriendsRequests(friendrequestId, updatedFriendrequest).subscribe(friendrequest => {
        expect(friendrequest).toEqual(updatedFriendrequest);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/friendrequest/${friendrequestId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedFriendrequest);
    });
  });

  describe('deleteFriendrequests', () => {
    it('should make DELETE request to delete a friend request by id', () => {
      const friendrequestId = 1;
      
      service.deleteFriendsRequests(friendrequestId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/friendrequest/${friendrequestId}`);
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

  it('should return an observable with a friendrequest-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
