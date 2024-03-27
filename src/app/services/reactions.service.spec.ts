
import { TestBed } from '@angular/core/testing';
import { Reaction } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { ReactionsService } from './reactions.service';

describe('ReactionsService', () => {
    let service: ReactionsService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let reaction: Reaction;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(ReactionsService);
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

            service.getAll().subscribe((res: Reaction[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/reaction');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: Reaction) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/reaction/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createReactions should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createReactions(reaction).subscribe((res: Reaction) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/reaction');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createReactions).toBeTruthy();
    });

    it('updateReactions should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateReactions(id, reaction).subscribe((res: Reaction) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/reaction/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateReactions).toBeTruthy();
    });

    it('deleteReactions should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteReactions(id).subscribe((res: Reaction) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/reaction/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteReactions).toBeTruthy();
    });
});
      