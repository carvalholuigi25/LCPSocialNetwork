import { TestBed } from '@angular/core/testing';
import { ThemesService } from './themes.service';
import { DOCUMENT } from '@angular/common';

declare global {
  interface Window {
    body: bodyObj;
  }

  interface bodyObj {
    classList: bodyObjCl;
  }

  interface bodyObjCl {
    add: any;
    remove: any;
    contains: any;
  }
}

describe('ThemesService', () => {
  let service: ThemesService;
  let documentMock: Partial<Document>;

  beforeEach(() => {
    documentMock = {
      defaultView: {
        localStorage: {
          getItem: jest.fn(),
          setItem: jest.fn(),
          removeItem: jest.fn()
        },
        body: {
          classList: {
            add: jest.fn(),
            remove: jest.fn(),
            contains: jest.fn().mockReturnValue(false)
          }
        }
      } as any
    };

    TestBed.configureTestingModule({
      providers: [
        ThemesService,
        { provide: DOCUMENT, useValue: documentMock }
      ]
    });

    service = TestBed.inject(ThemesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get theme from local storage', () => {
    const localStorageValue = 'default';
    documentMock.defaultView!.localStorage.getItem = jest.fn().mockReturnValue(localStorageValue);

    const theme = service.getTheme();

    expect(theme).toEqual(localStorageValue);
    expect(documentMock.defaultView!.localStorage.getItem).toHaveBeenCalledWith('mytheme');
  });

  it('should set theme in local storage and add to body classList', () => {
    const theme = 'dark';
    const themeClass = 'mytheme-dark';
    service.setTheme(theme);

    expect(documentMock.defaultView!.localStorage.setItem).toHaveBeenCalledWith('mytheme', themeClass);
    expect(documentMock.defaultView!.body.classList.add).toHaveBeenCalledWith(themeClass);
  });

  it('should remove theme from local storage and body classList', () => {
    const theme = 'dark';
    const themeClass = 'mytheme-dark';
    documentMock.defaultView!.localStorage.getItem = jest.fn().mockReturnValue(themeClass);

    service.removeTheme(theme);

    expect(documentMock.defaultView!.localStorage.removeItem).toHaveBeenCalledWith('mytheme-' + theme);
    expect(documentMock.defaultView!.body.classList.remove).toHaveBeenCalledWith(themeClass);
  });

  it('should remove all themes from body classList', () => {
    const themesToRemove = ['default', 'glass', 'dark', 'light'];
    documentMock.defaultView!.body.classList.contains = jest.fn().mockReturnValue(true);

    service.removeAllThemes();

    themesToRemove.forEach(theme => {
      expect(documentMock.defaultView!.body.classList.remove).toHaveBeenCalledWith('mytheme-' + theme);
    });
  });
});
