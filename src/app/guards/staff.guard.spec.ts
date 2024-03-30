import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { StaffGuard } from './staff.guard';
import { AuthService } from '../services/auth.service';

describe('StaffGuard', () => {
  let guard: StaffGuard;
  let authService: AuthService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      providers: [
        StaffGuard,
        AuthService
      ]
    });
    guard = TestBed.inject(StaffGuard);
    authService = TestBed.inject(AuthService);
    router = TestBed.inject(Router);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('should allow access if user is logged in as admin', () => {
    const user = { Role: 'Administrator' };
    jest.spyOn(authService, 'userValue', 'get').mockReturnValue(user);

    const canActivate = guard.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    expect(canActivate).toBe(true);
  });

  it('should redirect to login page if user is not logged in', () => {
    jest.spyOn(authService, 'userValue', 'get').mockReturnValue(null);
    const navigateSpy = jest.spyOn(router, 'navigate').mockReturnValue(Promise.resolve(true));

    const canActivate = guard.canActivate({} as ActivatedRouteSnapshot, { url: '/test' } as RouterStateSnapshot);

    expect(canActivate).toBe(false);
    expect(navigateSpy).toHaveBeenCalledWith(['/auth/login'], { queryParams: { returnUrl: '/test' } });
  });

  it('should redirect to login page if user is logged in but not as admin', () => {
    const user = { Role: 'User' };
    jest.spyOn(authService, 'userValue', 'get').mockReturnValue(user);
    const navigateSpy = jest.spyOn(router, 'navigate').mockReturnValue(Promise.resolve(true));

    const canActivate = guard.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    expect(canActivate).toBe(false);
    expect(navigateSpy).toHaveBeenCalledWith(['/auth/login'], { queryParams: { returnUrl: undefined } });
  });
});
