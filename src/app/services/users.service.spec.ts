import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UsersService } from './users.service';
import { environment } from '@environments/environment';
import { User } from '../models';
import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

describe('UsersService', () => {
  let service: UsersService;
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
        UsersService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });
    service = TestBed.inject(UsersService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('setHeadersObj', () => {
    it('should return HttpHeaders with Authorization header if user is present in localStorage', () => {
      const mockToken = 'mockToken';
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(JSON.stringify({ token: mockToken }));
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBe(`Bearer ${mockToken}`);
      expect(headers.get('Content-Type')).toBe('application/json');
    });

    it('should return HttpHeaders without Authorization header if user is not present in localStorage', () => {
      documentMock.defaultView.localStorage.getItem.mockReturnValueOnce(null);
      
      const headers = service.setHeadersObj();
      
      expect(headers.get('Authorization')).toBeFalsy();
      expect(headers.get('Content-Type')).toBe('application/json');
    });
  });

  describe('getAll', () => {
    it('should make GET request to fetch all users', () => {
      const mockUsers: User[] = [{ UserId: 1, Username: 'admin', Password: "admin2024" }];
      
      service.getAll().subscribe(users => {
        expect(users).toEqual(mockUsers);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/user`);
      expect(req.request.method).toBe('GET');
      req.flush(mockUsers);
    });
  });

  describe('getAllById', () => {
    it('should make GET request to fetch a specific user by id', () => {
      const userId = 1;
      const mockUser: User = { UserId: userId, Username: 'admin', Password: "admin2024" };
      
      service.getAllById(userId).subscribe(user => {
        expect(user).toEqual(mockUser);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/user/${userId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockUser);
    });
  });

  describe('createUser', () => {
    it('should make POST request to create a new user', () => {
      const newUser: User = { Username: 'guest', Password: "guest2024" };
      const createdUser: User = { UserId: 2, ...newUser };
      
      service.createUser(newUser).subscribe(user => {
        expect(user).toEqual(createdUser);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/user`);
      expect(req.request.method).toBe('POST');
      req.flush(createdUser);
    });
  });

  describe('updateUser', () => {
    it('should make PUT request to update an existing user', () => {
      const userId = 1;
      const updatedUser: User = { UserId: userId, Username: 'admin', Password: "admin2024" };
      
      service.updateUser(userId, updatedUser).subscribe(user => {
        expect(user).toEqual(updatedUser);
      });
      
      const req = httpMock.expectOne(`${environment.apiUrl}/user/${userId}`);
      expect(req.request.method).toBe('PUT');
      req.flush(updatedUser);
    });
  });

  describe('deleteUser', () => {
    it('should make DELETE request to delete a user by id', () => {
      const userId = 1;
      
      service.deleteUser(userId).subscribe();
      
      const req = httpMock.expectOne(`${environment.apiUrl}/user/${userId}`);
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

  it('should return an observable with a user-facing error message', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found', error: 'Resource Not Found' });
    
    const result = service.handleError(errorResponse);

    expect(result).toEqual(throwError(expect.any(Function)));
  });

});
