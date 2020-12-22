import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EconCustomerComponent } from './econ-customer.component';

describe('EconCustomerComponent', () => {
  let component: EconCustomerComponent;
  let fixture: ComponentFixture<EconCustomerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EconCustomerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EconCustomerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
