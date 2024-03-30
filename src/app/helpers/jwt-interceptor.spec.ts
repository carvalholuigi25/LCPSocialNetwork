import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HTTP_INTERCEPTORS, HttpRequest } from '@angular/common/http';
import { JwtInterceptor } from './jwt-interceptor';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';
import { environment } from '@environments/environment';

describe('JwtInterceptor', () => {
  let interceptor: JwtInterceptor;
  let authService: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        AuthService,
        JwtInterceptor,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
      ]
    });

    interceptor = TestBed.inject(JwtInterceptor);
    authService = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(interceptor).toBeTruthy();
  });

  it('should add Authorization header if user is logged in and request is to API URL', () => {
    const user = { CurrentToken: 'mockToken' };
    spyOn(authService, 'userValue').and.returnValue(user);

    const apiUrl = environment.apiUrl;
    const request = new HttpRequest('GET', apiUrl);
    
    interceptor.intercept(request, { handle: () => new Observable<any>() }).subscribe();

    const httpRequest = httpMock.expectOne(apiUrl);
    expect(httpRequest.request.headers.has('Authorization')).toBe(true);
    expect(httpRequest.request.headers.get('Authorization')).toBe(`Bearer ${user.CurrentToken}`);
  });

  it('should not add Authorization header if user is not logged in', () => {
    spyOn(authService, 'userValue').and.returnValue(null);

    const apiUrl = environment.apiUrl;
    const request = new HttpRequest('GET', apiUrl);
    
    interceptor.intercept(request, { handle: () => new Observable<any>() }).subscribe();

    const httpRequest = httpMock.expectOne(apiUrl);
    expect(httpRequest.request.headers.has('Authorization')).toBe(false);
  });

  it('should not add Authorization header if request is not to API URL', () => {
    const user = { CurrentToken: 'mockToken' };
    spyOn(authService, 'userValue').and.returnValue(user);

    const nonApiUrl = environment.apiUrl;
    const request = new HttpRequest('GET', nonApiUrl);
    
    interceptor.intercept(request, { handle: () => new Observable<any>() }).subscribe();

    const httpRequest = httpMock.expectOne(nonApiUrl);
    expect(httpRequest.request.headers.has('Authorization')).toBe(false);
  });
});
