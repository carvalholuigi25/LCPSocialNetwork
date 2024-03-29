
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { AppComponent } from "./app.component";

import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';
import { BreakpointObserver } from '@angular/cdk/layout';
import { AlertsService, ThemesService } from './services';
import { LanguagesService } from '@app/services';
import { TranslateLoader, TranslateModule, TranslateService, TranslateStore } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from './modules';
import { HttpClient } from '@angular/common/http';
import { createTranslateLoader } from './app.config';
import { NgcCookieConsentConfig, NgcCookieConsentService, WindowService } from 'ngx-cookieconsent';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


describe("AppComponent", () => {
    let component: AppComponent;
    let fixture: ComponentFixture<AppComponent>;
    let $event: any;

    //let myService: MyService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [AuthService, BreakpointObserver, Router, AlertsService, ThemesService, LanguagesService, TranslateService, TranslateStore, NgcCookieConsentService, WindowService, NgcCookieConsentConfig],
            imports: [
                RouterTestingModule, 
                BrowserAnimationsModule,
                RouterOutlet, 
                SharedModule, 
                TranslateModule.forRoot({
                    defaultLanguage: 'en',
                    loader: {
                        provide: TranslateLoader,
                        useFactory: (createTranslateLoader),
                        deps: [HttpClient]
                    }
                })
            ]
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AppComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        //myService = TestBed.inject(MyService);
    });

    it('ngOnInit should...', () => {
        // Arrange
        // Act
        component.ngOnInit();
        expect(component.ngOnInit).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    it('logout should...', () => {
        // Arrange
        // Act
        component.logout();
        expect(component.logout).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    it('DoPreventMenuCloseAfterClicked should...', () => {
        // Arrange
        // Act
        component.DoPreventMenuCloseAfterClicked($event);
        expect(component.DoPreventMenuCloseAfterClicked).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    it('toggleMenu should...', () => {
        // Arrange
        // Act
        component.toggleMenu();
        expect(component.toggleMenu).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    it('LoadMediaObserver should...', () => {
        // Arrange
        // Act
        component.LoadMediaObserver();
        expect(component.LoadMediaObserver).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    it('DoRouterStuff should...', () => {
        // Arrange
        // Act
        component.DoRouterStuff();
        expect(component.DoRouterStuff).toBeTruthy();
        // Assert
        // Add your assertions here
    });


})
