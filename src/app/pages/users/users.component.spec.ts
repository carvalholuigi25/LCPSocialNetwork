
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from "@angular/core/testing";
import { UsersComponent } from "./users.component";
import { UsersService } from '@app/services/users.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { createTranslateLoader } from '@app/app.config';
import { TranslateService, TranslateStore, TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { RouterTestingModule } from '@angular/router/testing';
import {
    HttpClientTestingModule,
    HttpTestingController
} from '@angular/common/http/testing';
import { User } from '@app/models';
import { AuthService } from '@app/services';

describe("UsersComponent", () => {
    let component: UsersComponent;
    let fixture: ComponentFixture<UsersComponent>;
    let httpMock: HttpTestingController;
    let usersSrv: UsersService;
    let authSrv: AuthService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [],
            schemas: [CUSTOM_ELEMENTS_SCHEMA],
            providers: [UsersService, TranslateService, TranslateStore],
            imports: [RouterTestingModule, HttpClientTestingModule, TranslateModule.forRoot({
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
        fixture = TestBed.createComponent(UsersComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        authSrv = TestBed.inject(AuthService);
        usersSrv = TestBed.inject(UsersService);
        httpMock = TestBed.inject(HttpTestingController);
    });

    it('ngOnInit should...', () => {
        // Arrange
        // Act
        component.ngOnInit();

        let mydata: any;

        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            mydata = rlog;

            usersSrv.getAll().subscribe((res: User[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/user');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        // Assert
        // Add your assertions here
    });

    
})
        