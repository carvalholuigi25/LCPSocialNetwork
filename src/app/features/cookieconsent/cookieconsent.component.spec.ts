
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { CookieConsentComponent } from "./cookieconsent.component";

import { TranslateLoader, TranslateModule, TranslateService, TranslateStore } from '@ngx-translate/core';
import { NgcCookieConsentService, NgcInitializingEvent, NgcInitializationErrorEvent, WindowService, NgcCookieConsentConfig } from 'ngx-cookieconsent';
import { LanguagesService } from '@app/services';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { createTranslateLoader } from '@app/app.config';
import { RouterTestingModule } from '@angular/router/testing';

describe("CookieConsentComponent", () => {
    let component: CookieConsentComponent;
    let fixture: ComponentFixture<CookieConsentComponent>;
    //let myService: MyService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [NgcCookieConsentService,TranslateService,LanguagesService,TranslateService, TranslateStore, WindowService, NgcCookieConsentService, NgcCookieConsentConfig],
            imports: [HttpClientModule, RouterTestingModule, TranslateModule.forRoot({
                defaultLanguage: 'en',
                loader: {
                    provide: TranslateLoader,
                    useFactory: (createTranslateLoader),
                    deps: [HttpClient]
                }
            })]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(CookieConsentComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        //myService = TestBed.inject(MyService);
    });

    it('ngOnInit should...', () => {
        // Arrange
        // Act
        component.ngOnInit();
        // Assert
        // Add your assertions here
    });

    it('ngOnDestroy should...', () => {
        // Arrange
        // Act
        component.ngOnDestroy();
        // Assert
        // Add your assertions here
    });

    it('LoadCookieConsent should...', () => {
        // Arrange
        // Act
        component.LoadCookieConsent();
        // Assert
        // Add your assertions here
    });

    it('LoadTranslateService should...', () => {
        // Arrange
        // Act
        component.LoadTranslateService();
        // Assert
        // Add your assertions here
    });

    it('UnloadCookieConsent should...', () => {
        // Arrange
        // Act
        component.UnloadCookieConsent();
        // Assert
        // Add your assertions here
    });

    
})
        