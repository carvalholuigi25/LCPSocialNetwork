
import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';

import { Router } from '@angular/router';
import { User, UserAuth } from '@app/models';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AuthService', () => {
    let service: AuthService;
    let usersAuth: UserAuth = {
        Username: "admin",
        Password: "admin2024"
    };
    let users: User;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Router,Document,
            ],
        });
        
        service = TestBed.inject(AuthService);
    });

    it('login should...', () => {
        service.login(usersAuth);
        expect(service.login).toBeTruthy();
    });

    it('register should...', () => {
        service.register(users);
        expect(service.register).toBeTruthy();
    });

    it('logout should...', () => {
       service.logout();
       expect(service.logout).toBeTruthy();
    });
});
      