import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditFeedbacksDialog } from './edit.component';

describe('EditFeedbacksDialog', () => {
  let component: EditFeedbacksDialog;
  let fixture: ComponentFixture<EditFeedbacksDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditFeedbacksDialog]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditFeedbacksDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
