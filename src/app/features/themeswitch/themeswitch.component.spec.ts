
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { ThemeswitchComponent } from "./themeswitch.component";

import { ThemesService } from '@app/services';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { createTranslateLoader } from '@app/app.config';
import { TranslateService, TranslateStore, TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe("ThemeswitchComponent", () => {
    let component: ThemeswitchComponent;
    let fixture: ComponentFixture<ThemeswitchComponent>;
    let themeval: string = "default";
    //let myService: MyService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [ThemesService,TranslateService, TranslateStore],
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
        fixture = TestBed.createComponent(ThemeswitchComponent);
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

    it('switchTheme should...', () => {
        // Arrange
        // Act
        component.switchTheme(themeval);
        expect(component.switchTheme).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    it('loadThemes should...', () => {
        // Arrange
        // Act
        component.loadThemes();
        expect(component.loadThemes).toBeTruthy();
        // Assert
        // Add your assertions here
    });

    
})
        