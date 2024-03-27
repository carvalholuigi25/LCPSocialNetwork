import { TestBed } from "@angular/core/testing";
import { DomSanitizer, BrowserModule } from '@angular/platform-browser';
import { SafePipe } from "./safe.pipe";

describe('SafePipe', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
              BrowserModule,
            ],
            declarations: [],
            providers: [
                SafePipe,
                { provide: DomSanitizer, useValue: MockDomSanitizer }
            ]
          }).compileComponents();
    })

    it('should create an instance', () => {
        let pipe = TestBed.inject(SafePipe);
        expect(pipe).toBeTruthy();
    });
});

export class MockDomSanitizer {
    bypassSecurityTrustHtml() {}
    bypassSecurityTrustStyle() {}
    bypassSecurityTrustScript() {}
    bypassSecurityTrustUrl() {}
    bypassSecurityTrustResourceUrl() {}
    marked() {}
}