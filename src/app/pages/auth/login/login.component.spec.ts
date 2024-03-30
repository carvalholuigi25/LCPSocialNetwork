
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { LoginComponent } from "./login.component";

import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@app/services/auth.service';
import { AlertsService } from '@app/services';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { createTranslateLoader } from '@app/app.config';
import { TranslateService, TranslateStore, TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe("LoginComponent", () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;
    //let myService: MyService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [Router,AuthService,AlertsService,TranslateService, TranslateStore],
            imports: [NoopAnimationsModule, HttpClientModule, RouterTestingModule, TranslateModule.forRoot({
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
        fixture = TestBed.createComponent(LoginComponent);
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

    it('onSubmit should...', () => {
        // Arrange
        // Act
        component.onSubmit();
        expect(component.onSubmit).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    
})
        