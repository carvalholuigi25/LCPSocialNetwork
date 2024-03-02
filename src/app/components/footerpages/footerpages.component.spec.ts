import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FooterPagesComponent } from './footerpages.component';

describe('FooterPagesComponent', () => {
  let component: FooterPagesComponent;
  let fixture: ComponentFixture<FooterPagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FooterPagesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FooterPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
