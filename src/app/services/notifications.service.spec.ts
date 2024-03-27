
import { TestBed } from '@angular/core/testing';
import { Notification } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { NotificationsService } from './notifications.service';

describe('NotificationsService', () => {
    let service: NotificationsService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let notifications: Notification;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(NotificationsService);
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

            service.getAll().subscribe((res: Notification[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/notification');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: Notification) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/notification/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createNotification should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createNotification(notifications).subscribe((res: Notification) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/notification');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createNotification).toBeTruthy();
    });

    it('updateNotification should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateNotification(id, notifications).subscribe((res: Notification) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/notification/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateNotification).toBeTruthy();
    });

    it('deleteNotification should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteNotification(id).subscribe((res: Notification) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/notification/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteNotification).toBeTruthy();
    });
});
      