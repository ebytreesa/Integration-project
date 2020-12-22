import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EconProductsComponent } from './econ-products.component';

describe('EconProductsComponent', () => {
  let component: EconProductsComponent;
  let fixture: ComponentFixture<EconProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EconProductsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EconProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
