import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguageswitchComponent } from './languageswitch.component';

describe('LanguageswitchComponent', () => {
  let component: LanguageswitchComponent;
  let fixture: ComponentFixture<LanguageswitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LanguageswitchComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LanguageswitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
