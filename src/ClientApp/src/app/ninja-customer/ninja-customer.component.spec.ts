import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NinjaCustomerComponent } from './ninja-customer.component';

describe('NinjaCustomerComponent', () => {
  let component: NinjaCustomerComponent;
  let fixture: ComponentFixture<NinjaCustomerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NinjaCustomerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NinjaCustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
