import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchComponentDialog } from './search.component';

describe('SearchComponentDialog', () => {
  let component: SearchComponentDialog;
  let fixture: ComponentFixture<SearchComponentDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SearchComponentDialog]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchComponentDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
