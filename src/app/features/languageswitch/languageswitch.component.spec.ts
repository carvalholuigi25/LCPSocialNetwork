
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { LanguageswitchComponent } from "./languageswitch.component";

import { AlertsService, LanguagesService } from '@app/services';
import { TranslateLoader, TranslateModule, TranslateService, TranslateStore } from '@ngx-translate/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { createTranslateLoader } from '@app/app.config';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe("LanguageswitchComponent", () => {
    let component: LanguageswitchComponent;
    let fixture: ComponentFixture<LanguageswitchComponent>;
    //let myService: MyService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [LanguagesService,AlertsService,TranslateService,TranslateService, TranslateStore],
            imports: [BrowserAnimationsModule, HttpClientModule, RouterTestingModule, TranslateModule.forRoot({
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
        fixture = TestBed.createComponent(LanguageswitchComponent);
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

    it('switchLanguage should...', () => {
        // Arrange
        // Act
        const languageval = "en";
        component.switchLanguage(languageval);
        // Assert
        // Add your assertions here
    });

    it('loadLanguages should...', () => {
        // Arrange
        // Act
        component.loadLanguages();
        // Assert
        // Add your assertions here
    });

    
})
        