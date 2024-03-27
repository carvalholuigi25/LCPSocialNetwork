
import { TestBed } from '@angular/core/testing';
import { LanguagesService } from './languages.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('LanguagesService', () => {
    let service: LanguagesService;
    let currentLanguage: string = "en";

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                Document,
            ],
        });
        
        service = TestBed.inject(LanguagesService);
    });

    it('getListLanguages should...', () => {
        service.getListLanguages();
        expect(service.getListLanguages).toBeTruthy();
    });

    it('getLanguage should...', () => {
        service.getLanguage();
        expect(service.getLanguage).toBeTruthy();
    });

    it('setLanguage should...', () => {
        service.setLanguage(currentLanguage);
        expect(service.setLanguage).toBeTruthy();
    });

    it('removeLanguage should...', () => {
        service.removeLanguage();
        expect(service.removeLanguage).toBeTruthy();
    });
});
      