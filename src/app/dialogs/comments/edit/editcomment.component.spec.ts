import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditcommentDialogComponent } from './editcomment.component';

describe('EditcommentDialogComponent', () => {
  let component: EditcommentDialogComponent;
  let fixture: ComponentFixture<EditcommentDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditcommentDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditcommentDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
