
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { RegisterComponent } from "./register.component";

import { ActivatedRoute, Router } from '@angular/router';
import { AlertsService, LanguagesService, UsersService } from '@app/services';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { createTranslateLoader } from '@app/app.config';
import { TranslateService, TranslateStore, TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe("RegisterComponent", () => {
    let component: RegisterComponent;
    let fixture: ComponentFixture<RegisterComponent>;
    //let myService: MyService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [Router,UsersService,AlertsService,LanguagesService,TranslateService, TranslateStore],
            imports: [BrowserAnimationsModule, HttpClientModule, RouterTestingModule, TranslateModule.forRoot({
                defaultLanguage: 'en',
                loader: {
                    provide: TranslateLoader,
                    useFactory: (createTranslateLoader),
                    deps: [HttpClient]
                }
            })],
            teardown: {destroyAfterEach: false}
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(RegisterComponent);
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

    it('onClear should...', () => {
        // Arrange
        // Act
        component.onClear();
        // Assert
        // Add your assertions here
    });

    it('onSubmit should...', () => {
        // Arrange
        // Act
        component.onSubmit();
        // Assert
        // Add your assertions here
    });

    
})
        