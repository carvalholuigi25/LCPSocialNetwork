import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SharesDialogComponent } from './sharesdialog.component';

describe('SharesDialogComponent', () => {
  let component: SharesDialogComponent;
  let fixture: ComponentFixture<SharesDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SharesDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SharesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
