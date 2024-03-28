import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UsersService } from './users.service';
import { DOCUMENT } from '@angular/common';
import { User } from '../models';
import { environment } from '@environments/environment';
// import { HttpErrorResponse } from '@angular/common/http';

describe('UsersService', () => {
  let service: UsersService;
  let httpTestingController: HttpTestingController;
  let mockDocument: Document;

  beforeEach(() => {
    mockDocument = document;
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        UsersService,
        { provide: DOCUMENT, useValue: mockDocument }
      ]
    });
    service = TestBed.inject(UsersService);
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

  it('#getAll should retrieve users', () => {
    const mockUsers: User[] = [
      {
        UserId: 1,
        Username: "admin",
        Password: "admin2024",
        FirstName: "Luis",
        LastName: "Carvalho",
        Email: "luiscarvalho239@gmail.com",
        Role: "Administrator",
        Status: "public",
        Biography: "Hello, I'm Luis Carvalho!",
        AvatarUrl: "images/users/avatars/luis.jpg",
        CoverUrl: "images/users/covers/luis_cover.jpg",
        PhoneNumber: "1234567890",
        DateBirthday: "1996-06-04T00:00:00",
        DateAccountCreated: "2024-01-02T00:00:00",
        CurrentToken: "",
        RefreshToken: "",
        RefreshTokenExpiryTime: ""
      }
    ];

    service.getAll().subscribe(users => {
      expect(users.length).toBe(1);
      expect(users).toEqual(mockUsers);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/user`);
    expect(req.request.method).toBe('GET');
    req.flush(mockUsers);
  });

  it('#getAllById should retrieve user', () => {
    const mockUsers: User = {
      UserId: 1,
      Username: "admin",
      Password: "admin2024",
      FirstName: "Luis",
      LastName: "Carvalho",
      Email: "luiscarvalho239@gmail.com",
      Role: "Administrator",
      Status: "public",
      Biography: "Hello, I'm Luis Carvalho!",
      AvatarUrl: "images/users/avatars/luis.jpg",
      CoverUrl: "images/users/covers/luis_cover.jpg",
      PhoneNumber: "1234567890",
      DateBirthday: "1996-06-04T00:00:00",
      DateAccountCreated: "2024-01-02T00:00:00",
      CurrentToken: "",
      RefreshToken: "",
      RefreshTokenExpiryTime: ""
    };

    service.getAllById(1).subscribe(users => {
      expect(users).toEqual(mockUsers);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/user/1`);
    expect(req.request.method).toBe('GET');
    req.flush(mockUsers);
  });

  it('#createUsers should add a new user', () => {
    const newUser: User = {
        UserId: 2,
        Username: "guest",
        Password: "guest2024",
        FirstName: "Guest",
        LastName: "",
        Email: "guest@localhost.loc",
        Role: "User",
        Status: "public",
        Biography: "Hello, I'm guest!",
        AvatarUrl: "images/users/avatars/guest.png",
        CoverUrl: "images/users/covers/guest_cover.jpeg",
        PhoneNumber: "1234567890",
        DateBirthday: "1996-04-02T00:00:00",
        DateAccountCreated: "2024-01-03T00:00:00",
        CurrentToken: "",
        RefreshToken: "",
        RefreshTokenExpiryTime: ""
    };

    service.createUser(newUser).subscribe(user => {
      expect(user).toEqual(newUser);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/user`);
    expect(req.request.method).toBe('POST');
    req.flush(newUser);
  });

  it('#updateUsers should update the current user', () => {
    const newUser: User = {
        UserId: 2,
        Username: "guest",
        Password: "guest2025",
        FirstName: "Guest",
        LastName: "",
        Email: "guest@localhost.loc",
        Role: "User",
        Status: "public",
        Biography: "Hello, I'm guest!",
        AvatarUrl: "images/users/avatars/guest.png",
        CoverUrl: "images/users/covers/guest_cover.jpeg",
        PhoneNumber: "1234567890",
        DateBirthday: "1996-04-02T00:00:00",
        DateAccountCreated: "2024-01-03T00:00:00",
        CurrentToken: "",
        RefreshToken: "",
        RefreshTokenExpiryTime: ""
    };

    service.updateUser(2, newUser).subscribe(user => {
      expect(user).toEqual(newUser);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/user/2`);
    expect(req.request.method).toBe('PUT');
    req.flush(newUser);
  });

  it('#deleteUsers should delete the current user', () => {
    const newUser: User = {
        UserId: 2,
        Username: "guest",
        Password: "guest2025",
        FirstName: "Guest",
        LastName: "",
        Email: "guest@localhost.loc",
        Role: "User",
        Status: "public",
        Biography: "Hello, I'm guest!",
        AvatarUrl: "images/users/avatars/guest.png",
        CoverUrl: "images/users/covers/guest_cover.jpeg",
        PhoneNumber: "1234567890",
        DateBirthday: "1996-04-02T00:00:00",
        DateAccountCreated: "2024-01-03T00:00:00",
        CurrentToken: "",
        RefreshToken: "",
        RefreshTokenExpiryTime: ""
    };

    service.deleteUser(2).subscribe(user => {
      expect(user).toEqual(newUser);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/user/2`);
    expect(req.request.method).toBe('DELETE');
    req.flush(newUser);
  });

//   it('#handleError should return an error message', () => {
//     const errorResponse = new HttpErrorResponse({
//       error: 'test error',
//       status: 404,
//       statusText: 'Not Found',
//     });

//     service.handleError(errorResponse).subscribe({
//       next: () => fail('expected an error, not users'),
//       error: (error: Error) => {
//         expect(error.message).toContain('Something bad happened; please try again later.');
//       }
//     });
//   });
});