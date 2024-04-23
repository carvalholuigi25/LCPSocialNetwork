import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReactionsDialogComponent } from './reactionsdialog.component';

describe('ReactionsDialogComponent', () => {
  let component: ReactionsDialogComponent;
  let fixture: ComponentFixture<ReactionsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReactionsDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReactionsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
