import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NinjaProductsComponent } from './ninja-products.component';

describe('NinjaProductsComponent', () => {
  let component: NinjaProductsComponent;
  let fixture: ComponentFixture<NinjaProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NinjaProductsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NinjaProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
