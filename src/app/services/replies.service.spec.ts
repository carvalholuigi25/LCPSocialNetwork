
import { TestBed } from '@angular/core/testing';
import { Reply } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { RepliesService } from './replies.service';

describe('RepliesService', () => {
    let service: RepliesService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let reply: Reply;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(RepliesService);
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

            service.getAll().subscribe((res: Reply[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/reply');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: Reply) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/reply/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createReplies should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createReplies(reply).subscribe((res: Reply) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/reply');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createReplies).toBeTruthy();
    });

    it('updateReplies should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateReplies(id, reply).subscribe((res: Reply) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/reply/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateReplies).toBeTruthy();
    });

    it('deleteReplies should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteReplies(id).subscribe((res: Reply) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/reply/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteReplies).toBeTruthy();
    });
});
      