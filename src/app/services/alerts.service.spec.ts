
import { TestBed } from '@angular/core/testing';
import { AlertsService } from './alerts.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('AlertsService', () => {
    let service: AlertsService;
    let message: string;
    let seconds: number;
    let clStatus: string;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [BrowserAnimationsModule],
            providers: [
                MatSnackBar,
            ],
        });
        
        service = TestBed.inject(AlertsService);
    });
    
    it('openAlert should...', () => {
        service.openAlert(message, seconds, clStatus);
        expect(service.openAlert).toBeTruthy();
    });

    it('closeAlert should...', () => {
        service.closeAlert();
        expect(service.closeAlert).toBeTruthy();
    });
});
      