import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MaterialModule } from '../material.module';
import { CommonModule } from '@angular/common';

import { Order } from '../models/order';
import { OrderService } from '../services/order/order.service';
import { Customer } from '../models/customer';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    CommonModule,
  ],
  providers: [
    OrderService
  ],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.scss'
})
export class OrdersComponent implements OnInit {

  customer!: any;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  displayedColumns: string[] = [
    'orderId',
    'requiredDate',
    'shippedDate',
    'shipName',
    'shipAddress',
    'shipCity'
    ];

    orders: Order[] = [];
    dataSource = new MatTableDataSource<Order>(this.orders);

  constructor(
    private orderService: OrderService,
    public dialogRef: MatDialogRef<OrdersComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Customer
  ) {
    this.customer = data;
  }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders() {
    // Llama a tu servicio para obtener las órdenes del cliente
    this.orderService.getOrders(this.customer.data.custId).subscribe((data: Order[]) => {
      this.orders = data;
      this.dataSource.data = this.orders; // Set data for the data source
      this.dataSource.paginator = this.paginator; // Assign paginator
      this.dataSource.sort = this.sort; // Assign sort
    });
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
