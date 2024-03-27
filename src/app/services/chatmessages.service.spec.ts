
import { TestBed } from '@angular/core/testing';
import { ChatMessage } from '@app/models';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { ChatMessagesService } from './chatmessages.service';

describe('ChatMessagesService', () => {
    let service: ChatMessagesService;
    let httpMock: HttpTestingController;
    let authSrv: AuthService;
    let id: number = 1;
    let chatMessage: ChatMessage;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(ChatMessagesService);
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

            service.getAll().subscribe((res: ChatMessage[]) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/chatmessage');
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAll).toBeTruthy();
    });

    it('getAllById should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.getAllById(id).subscribe((res: ChatMessage) => {
                expect(res).toEqual(mydata);
            });
    
            const req = httpMock.expectOne('/api/chatmessage/'+id);
            expect(req.request.method).toEqual("GET");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.getAllById).toBeTruthy();
    });

    it('createChatMessages should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.createChatMessages(chatMessage).subscribe((res: ChatMessage) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/chatmessage');
            expect(req.request.method).toEqual("POST");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.createChatMessages).toBeTruthy();
    });

    it('updateChatMessages should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.updateChatMessages(id, chatMessage).subscribe((res: ChatMessage) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/chatmessage/'+id);
            expect(req.request.method).toEqual("PUT");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.updateChatMessages).toBeTruthy();
    });

    it('deleteChatMessages should...', () => {
        authSrv.login({ Username: "admin", Password: "admin2024" }).subscribe((rlog) => {
            let mydata = rlog;

            service.deleteChatMessages(id).subscribe((res: ChatMessage) => {
                expect(res).toEqual(mydata);
            });

            const req = httpMock.expectOne('/api/chatmessage/'+id);
            expect(req.request.method).toEqual("DELETE");
            req.flush(mydata);

            httpMock.verify();
        });

        expect(service.deleteChatMessages).toBeTruthy();
    });
});
      