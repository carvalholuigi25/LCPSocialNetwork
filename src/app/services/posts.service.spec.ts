
import { TestBed } from '@angular/core/testing';
import { PostsService } from './posts.service';
import { Post, User } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';

describe('PostsService', () => {
    let service: PostsService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let userId: number = 1;
    let posts: Post;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(PostsService);
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

            service.getAll().subscribe((res: Post[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/post');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: Post) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/post/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('getAllWithUsers should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllWithUsers(userId).subscribe((res) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/post/'+(id !== -1 ? '/user/' + id : id));
            expect(req.request.method).toEqual("GET");

            const req2 = httpMock.expectOne('/api/user/'+id);
            expect(req2.request.method).toEqual("GET");

            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllWithUsers).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(userId).subscribe((res: Post) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/post/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('getAllByUsersId should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllByUsersId(userId).subscribe((res: Post) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/post/user/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllByUsersId).toBeTruthy();
    });

    it('createPosts should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createPosts(posts).subscribe((res: Post) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/post');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createPosts).toBeTruthy();
    });

    it('updatePosts should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updatePosts(id, posts).subscribe((res: Post) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/post/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updatePosts).toBeTruthy();
    });

    it('deletePosts should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deletePosts(id).subscribe((res: Post) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/post/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deletePosts).toBeTruthy();
    });
});
      