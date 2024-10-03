import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from '../material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { Customer } from '../models/customer';
import { CustomerService } from '../services/customer/customer.service';

import { NewOrderComponent } from '../new-order/new-order.component';
import { MatDialog } from '@angular/material/dialog';
import { OrdersComponent } from '../orders/orders.component';

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
  styleUrls: ['./sales-date-prediction.component.scss'],
})
export class SalesDatePredictionComponent implements OnInit {
  customers: Customer[] = [];
  dataSource = new MatTableDataSource<Customer>(this.customers);
  displayedColumns: string[] = [
    'customerName',
    'lastOrderDate',
    'nextPredictedOrder',
    'action',
  ];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private customerService: CustomerService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadCustomers(); // Cargar clientes inicialmente
  }

  loadCustomers(searchTerm: string = ''): void {
    this.customerService.getSalesDatePrediction(searchTerm).subscribe((data: Customer[]) => {
      this.customers = data;
      this.dataSource.data = this.customers; // Set data for the data source
      this.dataSource.paginator = this.paginator; // Assign paginator
      this.dataSource.sort = this.sort; // Assign sort
    });
  }

  applyFilter(efilterValue: string): void {
    this.loadCustomers(efilterValue.trim().toLowerCase()); // Cargar clientes filtrados
  }

  openDialog(action: string, obj: any): void {
    // Implementation for opening a dialog
  }

  viewOrders(customer: any): void {
    const dialogRef = this.dialog.open(OrdersComponent, {
      data: { data: customer },
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      console.log(`Dialog result: ${result}`);
    });
  }

  newOrder(customer: any): void {
    const dialogRef = this.dialog.open(NewOrderComponent, {
      data: { data: customer },
    });

    dialogRef.afterClosed().subscribe((result: any) => {
      console.log(`Dialog result: ${result}`);
    });
  }
}
