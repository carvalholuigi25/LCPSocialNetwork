
import { TestBed } from '@angular/core/testing';
import { UsersService } from './users.service';
import { User } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';

describe('UsersService', () => {
    let service: UsersService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let users: User;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(UsersService);
        authSrv = TestBed.inject(AuthService);
        httpMock = TestBed.inject(HttpTestingController);
    });
    

    it('setHeadersObj should...', () => {
        service.setHeadersObj();
        expect(service.setHeadersObj).toBeTruthy();
    });

    it('getAll should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAll().subscribe((res: User[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/user');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: User) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/user/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createUser should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createUser(users).subscribe((res: User) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/user');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createUser).toBeTruthy();
    });

    it('updateUser should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateUser(id, users).subscribe((res: User) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/user/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateUser).toBeTruthy();
    });

    it('deleteUser should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteUser(id).subscribe((res: User) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/user/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteUser).toBeTruthy();
    });
});
      