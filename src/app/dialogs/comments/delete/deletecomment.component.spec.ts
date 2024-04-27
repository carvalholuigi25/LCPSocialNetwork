import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeletecommentDialogComponent } from './deletecomment.component';

describe('DeletecommentDialogComponent', () => {
  let component: DeletecommentDialogComponent;
  let fixture: ComponentFixture<DeletecommentDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeletecommentDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DeletecommentDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
