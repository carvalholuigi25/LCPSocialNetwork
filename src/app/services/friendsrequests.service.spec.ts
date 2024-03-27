
import { TestBed } from '@angular/core/testing';
import { FriendRequest } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { FriendsRequestsService } from './friendsrequests.service';

describe('FriendsRequestsService', () => {
    let service: FriendsRequestsService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let friendRequest: FriendRequest;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(FriendsRequestsService);
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

            service.getAll().subscribe((res: FriendRequest[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/friendrequest');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: FriendRequest) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/friendrequest/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createFriendsRequests should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createFriendsRequests(friendRequest).subscribe((res: FriendRequest) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/friendrequest');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createFriendsRequests).toBeTruthy();
    });

    it('updateFriendsRequests should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateFriendsRequests(id, friendRequest).subscribe((res: FriendRequest) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/friendrequest/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateFriendsRequests).toBeTruthy();
    });

    it('deleteFriendsRequests should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteFriendsRequests(id).subscribe((res: FriendRequest) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/friendrequest/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteFriendsRequests).toBeTruthy();
    });
});
      