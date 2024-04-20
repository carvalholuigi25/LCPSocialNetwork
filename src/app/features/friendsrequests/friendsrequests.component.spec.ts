import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FriendsrequestsComponent } from './friendsrequests.component';

describe('FriendsrequestsComponent', () => {
  let component: FriendsrequestsComponent;
  let fixture: ComponentFixture<FriendsrequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FriendsrequestsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FriendsrequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
