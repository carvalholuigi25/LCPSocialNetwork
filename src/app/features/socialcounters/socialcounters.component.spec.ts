import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SocialcountersComponent } from './socialcounters.component';

describe('SocialcountersComponent', () => {
  let component: SocialcountersComponent;
  let fixture: ComponentFixture<SocialcountersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SocialcountersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SocialcountersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
