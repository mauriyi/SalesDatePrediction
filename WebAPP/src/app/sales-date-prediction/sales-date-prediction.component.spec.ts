import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesDatePredictionComponent } from './sales-date-prediction.component';

describe('SalesDatePredictionComponent', () => {
  let component: SalesDatePredictionComponent;
  let fixture: ComponentFixture<SalesDatePredictionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SalesDatePredictionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SalesDatePredictionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
