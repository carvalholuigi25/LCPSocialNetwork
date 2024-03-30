import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HTTP_INTERCEPTORS, HttpErrorResponse, HttpHeaders, HttpRequest } from '@angular/common/http';
import { FakeBackendInterceptor } from './fake-backend';
import { throwError } from 'rxjs';

describe('FakeBackendInterceptor', () => {
  let interceptor: FakeBackendInterceptor;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        FakeBackendInterceptor,
        { provide: HTTP_INTERCEPTORS, useClass: FakeBackendInterceptor, multi: true }
      ]
    });

    interceptor = TestBed.inject(FakeBackendInterceptor);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(interceptor).toBeTruthy();
  });

  it('should auth user', () => {
    const username = 'admin';
    const password = 'admin2024';

    interceptor.intercept(
      new HttpRequest('POST', '/auth/login', { username, password }),
      {
        handle: () => throwError(new HttpErrorResponse({ status: 404 })),
      }
    ).subscribe();

    const req = httpMock.expectOne('/auth/login');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ username, password });
    req.flush({ UserId: 1, Username: username, CurrentToken: 'fake-jwt-token' });
  });

  it('should get user', () => {
    const token = 'Bearer fake-jwt-token';
    const headers = new HttpHeaders().set('Authorization', token);

    interceptor.intercept(
      new HttpRequest('GET', '/user', null, { headers }),
      {
        handle: () => throwError(new HttpErrorResponse({ status: 404 })),
      }
    ).subscribe();

    const req = httpMock.expectOne('/user');
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBe(token);
    req.flush([{ UserId: 1, Username: 'admin' }]);
  });

  it('should handle unauthorized access', () => {
    interceptor.intercept(
      new HttpRequest('GET', '/user', null),
      {
        handle: () => throwError(new HttpErrorResponse({ status: 401 })),
      }
    ).subscribe();

    const req = httpMock.expectOne('/user');
    expect(req.request.method).toBe('GET');
    req.error(new ErrorEvent('error'), { status: 401, statusText: 'Unauthorized' });
  });
});
