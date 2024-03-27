
import { TestBed } from '@angular/core/testing';
import { Comment } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { CommentService } from './comments.service';

describe('CommentService', () => {
    let service: CommentService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let comment: Comment;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(CommentService);
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

            service.getAll().subscribe((res: Comment[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/comment');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: Comment) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/comment/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createComments should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createComments(comment).subscribe((res: Comment) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/comment');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createComments).toBeTruthy();
    });

    it('updateComments should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateComments(id, comment).subscribe((res: Comment) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/comment/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateComments).toBeTruthy();
    });

    it('deleteComments should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteComments(id).subscribe((res: Comment) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/comment/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteComments).toBeTruthy();
    });
});
      