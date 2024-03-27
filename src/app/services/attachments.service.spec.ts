
import { TestBed } from '@angular/core/testing';
import { Attachment } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { AttachmentsService } from './attachments.service';

describe('AttachmentsService', () => {
    let service: AttachmentsService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let attachment: Attachment;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(AttachmentsService);
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

            service.getAll().subscribe((res: Attachment[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/attachment');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: Attachment) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/attachment/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createAttachment should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createAttachment(attachment).subscribe((res: Attachment) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/attachment');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createAttachment).toBeTruthy();
    });

    it('updateAttachment should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateAttachment(id, attachment).subscribe((res: Attachment) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/attachment/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateAttachment).toBeTruthy();
    });

    it('deleteAttachment should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteAttachment(id).subscribe((res: Attachment) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/attachment/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteAttachment).toBeTruthy();
    });
});
      