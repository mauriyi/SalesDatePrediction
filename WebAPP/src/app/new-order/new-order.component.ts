import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from '../material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';

import { Customer } from '../models/customer';
import { Employee } from '../models/employee';
import { Product } from '../models/product';
import { Shipper } from '../models/shipper';
import { NewOrderDto } from '../models/newOrderDto';

import { EmployeeService } from '../services/employee/employee.service';
import { ProductService } from '../services/product/product.service';
import { ShipperService } from '../services/shipper/shipper.service';
import { OrderService } from '../services/order/order.service';
import { DialogMessageComponent } from '../dialog-message/dialog-message.component';

@Component({
  selector: 'app-new-order',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    CommonModule,
    HttpClientModule,
  ],
  providers: [
    EmployeeService,
    ProductService,
    ShipperService,
    OrderService
  ],
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.scss']
})
export class NewOrderComponent implements OnInit {

  customer!: any;

  employees: Employee[] = [];
  products: Product[] = [];
  shippers: Shipper[] = [];

  orderForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<NewOrderComponent>,
    private employeeService: EmployeeService,
    private productService: ProductService,
    private shipperService: ShipperService,
    private orderService: OrderService,
    private dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: Customer
  ) {
    this.customer = data;
  }

  ngOnInit(): void {
    console.log('Customer in ngOnInit:', this.customer); // Agrega este log

    this.orderForm = this.fb.group({
      order: this.fb.group({
        employee: ['', Validators.required],
        shipper: ['', Validators.required],
        shipName: ['', Validators.required],
        shipAddress: ['', Validators.required],
        shipCity: ['', Validators.required],
        shipCountry: ['', Validators.required],
        orderDate: ['', Validators.required],
        requiredDate: [''],
        shippedDate: [''],
        freight: [0, Validators.min(0)],
      }),
      orderDetails: this.fb.group({
        product: ['', Validators.required],
        unitPrice: [0, [Validators.required, Validators.min(0)]],
        quantity: [1, [Validators.required, Validators.min(1)]],
        discount: [0, [Validators.min(0)]],
      })
    });

    // Observa los cambios en los valores del formulario
  this.orderForm.valueChanges.subscribe(values => {
    console.log('Current form values:', values);
  });

  // Observa el estado del formulario
  this.orderForm.statusChanges.subscribe(status => {
    console.log('Form status:', status);
  });

      // Cargar las listas al iniciar
      this.loadEmployees();
      this.loadProducts();
      this.loadShipperss();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe(
      (data: Employee[]) => {
        this.employees = data; // Almacena la lista de empleados
      },
      (error) => {
        console.error('Error fetching employees:', error);
      }
    );
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(
      (data: Product[]) => {
        this.products = data; // Almacena la lista de empleados
      },
      (error) => {
        console.error('Error fetching products:', error);
      }
    );
  }

  loadShipperss(): void {
    this.shipperService.getShippers().subscribe(
      (data: Shipper[]) => {
        this.shippers = data; // Almacena la lista de empleados
      },
      (error) => {
        console.error('Error fetching shippers:', error);
      }
    );
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const orderData: NewOrderDto = {
        empId: this.orderForm.value.order.employee,
        custId: this.customer.data.custId,
        shipperId: this.orderForm.value.order.shipper,
        shipName: this.orderForm.value.order.shipName,
        shipAddress: this.orderForm.value.order.shipAddress,
        shipCity: this.orderForm.value.order.shipCity,
        orderdate: this.orderForm.value.order.orderDate,
        requiredDate: this.orderForm.value.order.requiredDate,
        shippedDate: this.orderForm.value.order.shippedDate,
        freight: this.orderForm.value.order.freight,
        shipCountry: this.orderForm.value.order.shipCountry,
        orderDetails: {
          productId: this.orderForm.value.orderDetails.product,
          unitPrice: this.orderForm.value.orderDetails.unitPrice,
          qty: this.orderForm.value.orderDetails.quantity,
          discount: this.orderForm.value.orderDetails.discount
        }
      };

      this.orderService.addNewOrder(orderData).subscribe(
        (response) => {
            // Abre el diálogo de éxito
          this.dialog.open(DialogMessageComponent, {
            data: {
              title: 'Success',
            message: 'Order created successfully!'
          }
        });
          this.dialogRef.close();
        },
        (error) => {
          // Abre el diálogo de error
          this.dialog.open(DialogMessageComponent, {
            data: {
              title: 'Error',
              message: 'Error creating order. Please try again.'
            }
          });
        }
      );
    }
  }

  onClose(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    this.dialogRef.close();
  }
}
