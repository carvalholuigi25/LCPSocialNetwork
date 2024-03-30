import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { LanguageswitchComponent } from './languageswitch.component';
import { LanguagesService } from '@app/services';
import { TranslateService } from '@ngx-translate/core';
import { of, throwError } from 'rxjs';

describe('LanguageswitchComponent', () => {
  let component: LanguageswitchComponent;
  let fixture: ComponentFixture<LanguageswitchComponent>;
  let languagesService: LanguagesService;
  let translateService: TranslateService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [LanguageswitchComponent],
      providers: [LanguagesService, TranslateService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LanguageswitchComponent);
    component = fixture.componentInstance;
    languagesService = TestBed.inject(LanguagesService);
    translateService = TestBed.inject(TranslateService);
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should set default language and load languages on initialization', () => {
    spyOn(languagesService, 'getLanguage').and.returnValue('en');
    spyOn(languagesService, 'getListLanguages').and.returnValue(of(['English', 'Portuguese']));
    spyOn(translateService, 'setDefaultLang').and.callThrough();
    spyOn(component, 'switchLanguage').and.callThrough();

    component.ngOnInit();

    expect(translateService.setDefaultLang).toHaveBeenCalledWith('en');
    expect(component.switchLanguage).toHaveBeenCalledWith('en');
    expect(languagesService.getListLanguages).toHaveBeenCalled();
    expect(component.aryLanguages).toEqual(['English', 'Portuguese']);
  });

  it('should switch language properly', () => {
    spyOn(translateService, 'use').and.callThrough();
    spyOn(languagesService, 'setLanguage').and.callThrough();

    component.switchLanguage('pt');

    expect(translateService.use).toHaveBeenCalledWith('pt');
    expect(languagesService.setLanguage).toHaveBeenCalledWith('pt');
    expect(component.selectedLanguage).toEqual('pt');
  });

  it('should handle error when loading languages', () => {
    spyOn(languagesService, 'getLanguage').and.returnValue('en');
    spyOn(languagesService, 'getListLanguages').and.returnValue(throwError({ message: 'Error message' }));
    spyOn(console, 'error'); // Spy on console.error

    component.ngOnInit();

    expect(console.error).toHaveBeenCalledWith(jasmine.any(Error));
    // You can add more expectations here based on how you handle errors in your component
  });

  it('should switch language to default when no language value is provided', () => {
    spyOn(component, 'switchLanguage').and.callThrough();
    spyOn(languagesService, 'getLanguage').and.returnValue(null); // Simulate no language value

    component.ngOnInit();

    expect(component.switchLanguage).toHaveBeenCalledWith('en');
  });
});
