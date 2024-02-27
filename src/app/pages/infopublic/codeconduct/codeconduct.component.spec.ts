import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodeConductComponent } from './codeconduct.component';

describe('CodeConductComponent', () => {
  let component: CodeConductComponent;
  let fixture: ComponentFixture<CodeConductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CodeConductComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CodeConductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
