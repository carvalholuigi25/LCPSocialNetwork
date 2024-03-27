
import { TestBed } from '@angular/core/testing';
import { Share } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { SharesService } from './shares.service';

describe('SharesService', () => {
    let service: SharesService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let share: Share;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(SharesService);
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

            service.getAll().subscribe((res: Share[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/share');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: Share) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/share/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createShares should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createShares(share).subscribe((res: Share) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/share');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createShares).toBeTruthy();
    });

    it('updateShares should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateShares(id, share).subscribe((res: Share) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/share/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateShares).toBeTruthy();
    });

    it('deleteShares should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteShares(id).subscribe((res: Share) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/share/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteShares).toBeTruthy();
    });
});
      