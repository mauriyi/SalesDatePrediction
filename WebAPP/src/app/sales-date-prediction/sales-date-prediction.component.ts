import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

//Import all material modules
import { MaterialModule } from '../material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Customer } from '../models/customer';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-sales-date-prediction',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    CommonModule,
    HttpClientModule,
  ],
  providers: [CustomerService],
  templateUrl: './sales-date-prediction.component.html',
  styleUrl: './sales-date-prediction.component.scss'
})
export class SalesDatePredictionComponent implements OnInit {

  customers: Customer[] = [];

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.customerService.getSalesDatePrediction().subscribe((data: Customer[]) => {
      this.customers = data;
    });
  }
}
