import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { DOCUMENT } from '@angular/common';
import { LanguagesService } from './languages.service';
import { HttpErrorResponse } from '@angular/common/http';
import { environment } from '@environments/environment';

describe('LanguagesService', () => {
  let service: LanguagesService;
  let httpMock: HttpTestingController;
  let documentMock: Partial<Document>;

  beforeEach(() => {
    documentMock = {
      defaultView: {
        localStorage: {
          getItem: jest.fn(),
          setItem: jest.fn(),
          removeItem: jest.fn()
        }
      } as any
    };

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        LanguagesService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });

    service = TestBed.inject(LanguagesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get list of languages', () => {
    const mockResponse = [{ id: 1, name: 'English', countrycodeiso: "en" }];
    service.getListLanguages().subscribe(languages => {
      expect(languages).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${environment.apiUrl}/language`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should get language from local storage', () => {
    const localStorageValue = 'en';
    documentMock.defaultView!.localStorage.getItem = jest.fn().mockReturnValue(localStorageValue);

    const language = service.getLanguage();

    expect(language).toEqual(localStorageValue);
    expect(documentMock.defaultView!.localStorage.getItem).toHaveBeenCalledWith('mylanguage');
  });

  it('should set language in local storage', () => {
    const language = 'en';
    service.setLanguage(language);

    expect(documentMock.defaultView!.localStorage.setItem).toHaveBeenCalledWith('mylanguage', language);
  });

  it('should remove language from local storage', () => {
    service.removeLanguage();

    expect(documentMock.defaultView!.localStorage.removeItem).toHaveBeenCalledWith('mylanguage');
  });

  it('should handle HTTP errors', () => {
    const errorResponse = new HttpErrorResponse({ status: 404, statusText: 'Not Found' });
    const consoleErrorSpy = jest.spyOn(console, 'error').mockImplementation(() => {});

    service.handleError(errorResponse);

    expect(consoleErrorSpy).toHaveBeenCalledWith(`Backend returned code ${errorResponse.status}, body was: `, errorResponse.error);
    consoleErrorSpy.mockRestore();
  });
});
