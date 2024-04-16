import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteFeedbacksDialog } from './delete.component';

describe('DeleteFeedbacksDialog', () => {
  let component: DeleteFeedbacksDialog;
  let fixture: ComponentFixture<DeleteFeedbacksDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteFeedbacksDialog]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeleteFeedbacksDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
