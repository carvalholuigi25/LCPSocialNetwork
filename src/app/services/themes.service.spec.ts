
import { TestBed } from '@angular/core/testing';
import { ThemesService } from './themes.service';

describe('ThemesService', () => {
    let service: ThemesService;
    let currentTheme: string = "default";

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(ThemesService);
    });

    it('getTheme should...', () => {
        service.getTheme();
        expect(service.getTheme).toBeTruthy();
    });

    it('setTheme should...', () => {
        service.setTheme(currentTheme);
        expect(service.setTheme).toBeTruthy();
    });

    it('removeTheme should...', () => {
        service.removeTheme(currentTheme);
        expect(service.removeTheme).toBeTruthy();
    });

    it('removeAllThemes should...', () => {
        service.removeAllThemes();
        expect(service.removeAllThemes).toBeTruthy();
    });
});
      