import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CookieConsentComponent } from './cookieconsent.component';
import { NgcCookieConsentService, NgcInitializingEvent, NgcInitializationErrorEvent } from 'ngx-cookieconsent';
import { TranslateService } from '@ngx-translate/core';
import { LanguagesService } from '@app/services';
import { of, throwError, Subject } from 'rxjs';

describe('CookieConsentComponent', () => {
  let component: CookieConsentComponent;
  let fixture: ComponentFixture<CookieConsentComponent>;
  let ccService: NgcCookieConsentService;
  let translateService: TranslateService;
  let langService: LanguagesService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [CookieConsentComponent],
      providers: [
        NgcCookieConsentService,
        TranslateService,
        LanguagesService
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CookieConsentComponent);
    component = fixture.componentInstance;
    ccService = TestBed.inject(NgcCookieConsentService);
    translateService = TestBed.inject(TranslateService);
    langService = TestBed.inject(LanguagesService);
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load cookie consent and translate service on initialization', () => {
    spyOn(component, 'LoadCookieConsent').and.callThrough();
    spyOn(component, 'LoadTranslateService').and.callThrough();

    component.ngOnInit();

    expect(component.LoadCookieConsent).toHaveBeenCalled();
    expect(component.LoadTranslateService).toHaveBeenCalled();
    // You can add more expectations as needed
  });

  it('should unsubscribe to observables on component destruction', () => {
    spyOn(component.initializingSubscription, 'unsubscribe').and.callThrough();
    spyOn(component.initializedSubscription, 'unsubscribe').and.callThrough();
    spyOn(component.initializationErrorSubscription, 'unsubscribe').and.callThrough();

    component.ngOnDestroy();

    expect(component.initializingSubscription.unsubscribe).toHaveBeenCalled();
    expect(component.initializedSubscription.unsubscribe).toHaveBeenCalled();
    expect(component.initializationErrorSubscription.unsubscribe).toHaveBeenCalled();
  });

  it('should handle error when loading languages', () => {
    spyOn(langService, 'getListLanguages').and.returnValue(throwError({ message: 'Error message' }));
    spyOn(console, 'log');

    component.LoadTranslateService();

    expect(console.log).toHaveBeenCalledWith('Error message');
    // You can add more expectations based on how you handle errors in your component
  });

  it('should subscribe to initializing, initialized, and initializationError events', () => {
    const initializingSubject = new Subject<NgcInitializingEvent>();
    const initializedSubject = new Subject<void>();
    const initializationErrorSubject = new Subject<NgcInitializationErrorEvent>();

    spyOn(ccService, 'initializing$').and.returnValue(initializingSubject.asObservable());
    spyOn(ccService, 'initialized$').and.returnValue(initializedSubject.asObservable());
    spyOn(ccService, 'initializationError$').and.returnValue(initializationErrorSubject.asObservable());

    component.LoadCookieConsent();

    // Emit events to simulate initialization process
    initializingSubject.next({} as NgcInitializingEvent);
    initializedSubject.next();
    initializationErrorSubject.next({} as NgcInitializationErrorEvent);

    expect(console.log).toHaveBeenCalledTimes(2); // Assuming you're logging events in your component
    // You can add more expectations as needed
  });

  it('should properly initialize translate service with languages', () => {
    spyOn(langService, 'getListLanguages').and.returnValue(of(['en', 'pt']));
    spyOn(translateService, 'addLangs').and.callThrough();
    spyOn(translateService, 'setDefaultLang').and.callThrough();
    spyOn(translateService, 'get').and.returnValue(of({
      'cookie.header': 'Cookie Header',
      'cookie.message': 'Cookie Message',
      'cookie.dismiss': 'Dismiss',
      'cookie.allow': 'Allow cookies',
      'cookie.deny': 'Deny cookies',
      'cookie.link': 'Cookie Policy',
      'cookie.policy': 'Privacy Policy'
    }));
    spyOn(ccService, 'destroy').and.callThrough();
    spyOn(ccService, 'init').and.callThrough();

    component.LoadTranslateService();

    expect(translateService.addLangs).toHaveBeenCalledWith(['en', 'pt']);
    expect(translateService.setDefaultLang).toHaveBeenCalledWith('en');
    expect(translateService.get).toHaveBeenCalled();
    expect(ccService.destroy).toHaveBeenCalled();
    expect(ccService.init).toHaveBeenCalled();
  });

  it('should handle error when loading languages', () => {
    spyOn(langService, 'getListLanguages').and.returnValue(throwError({ message: 'Error message' }));
    spyOn(console, 'log');

    component.LoadTranslateService();

    expect(console.log).toHaveBeenCalledWith('Error message');
    // You can add more expectations based on how you handle errors in your component
  });
});
