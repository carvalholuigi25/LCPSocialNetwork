import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HTTP_INTERCEPTORS, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { ErrorInterceptor } from './error.interceptor';
import { AuthService } from '../services/auth.service';
import { throwError } from 'rxjs';

describe('ErrorInterceptor', () => {
  let interceptor: ErrorInterceptor;
  let authService: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AuthService,
        ErrorInterceptor,
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
      ]
    });

    interceptor = TestBed.inject(ErrorInterceptor);
    authService = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(interceptor).toBeTruthy();
  });

  it('should handle 401 Unauthorized error', () => {
    const logoutSpy = jest.spyOn(authService, 'logout').mockImplementation(() => {});

    interceptor.intercept(
      new HttpRequest('GET', '/user'),
      {
        handle: () => throwError(new HttpErrorResponse({ status: 401 })),
      }
    ).subscribe({
      next: () => {},
      error: (error) => {
        expect(error).toBe('Unauthorized');
        expect(logoutSpy).toHaveBeenCalled();
      }
    });
  });

  it('should handle 403 Forbidden error', () => {
    const logoutSpy = jest.spyOn(authService, 'logout').mockImplementation(() => {});

    interceptor.intercept(
      new HttpRequest('GET', '/user'),
      {
        handle: () => throwError(new HttpErrorResponse({ status: 403 })),
      }
    ).subscribe({
        next: () => {},
        error: (error) => {
            expect(error).toBe('Forbidden');
            expect(logoutSpy).toHaveBeenCalled();
        }
    });
  });

  it('should handle other errors', () => {
    interceptor.intercept(
      new HttpRequest('GET', '/user'),
      {
        handle: () => throwError(new HttpErrorResponse({ status: 500, error: { message: 'Internal Server Error' } })),
      }
    ).subscribe({
        next: () => {},
        error: (error) => {
            expect(error).toBe('Internal Server Error');
        }
    });
  });
});
